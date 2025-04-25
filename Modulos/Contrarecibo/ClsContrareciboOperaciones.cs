using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace Reportes.Modulos.Contrarecibo
{
	public class ClsContrareciboOperaciones
	{
		private readonly MySqlConnection _con;
		private readonly MySqlCommand _cmd;

		public ClsContrareciboOperaciones(string con)
		{
			_con = new MySqlConnection(con);
			_cmd = _con.CreateCommand();
		}

		public async Task<string[]> ObtenerDatosContrarecibo(int id)
		{
			try
			{
				await _con.OpenAsync();

				_cmd.CommandType = CommandType.StoredProcedure;

				_cmd.Parameters.AddWithValue("@Folio", id);

				_cmd.CommandText = "contrarecibo.ObtenerContrareciboEspecifico";

				var obj = await _cmd.ExecuteReaderAsync();

				string[] result = null;

				if (obj.Read())
				{
					result = new string[] { obj.GetString(0), obj.GetString(1), obj.GetString(2), obj.GetString(3) };
				}

				obj.Close();
				_cmd.CommandType = CommandType.Text;

				_cmd.Parameters.Clear();

				return result;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await _con.CloseAsync();
			}

			return new string[] { };
		}

		public async Task AplicarComoPagado(int folio)
		{
			try
			{
				await _con.OpenAsync();

				_cmd.CommandType = CommandType.StoredProcedure;

				_cmd.Parameters.AddWithValue("@folio", folio);

				_cmd.CommandText = "contrarecibo.AplicarComoPagado";

				await _cmd.ExecuteNonQueryAsync();

				_cmd.CommandType = CommandType.Text;

				_cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await _con.CloseAsync();
			}
		}

		public async Task<DataTable> ObtenerContrarecibos(DateTime fechaA, DateTime fechaB, string filtro)
		{
			DataTable resultado = new DataTable();

			try
			{
				await _con.OpenAsync();

				_cmd.CommandType = CommandType.StoredProcedure;

				_cmd.CommandText = "contrarecibo.ContrarecibosConFiltros";
				_cmd.Parameters.AddWithValue("@fecha_inicio", fechaA);
				_cmd.Parameters.AddWithValue("@fecha_fin", fechaB);
				_cmd.Parameters.AddWithValue("@filtro", filtro);

				MySqlDataAdapter ad = new MySqlDataAdapter(_cmd);

				await ad.FillAsync(resultado);

				_cmd.CommandType = CommandType.Text;
				_cmd.Parameters.Clear();
			}
			catch (MySqlException ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await _con.CloseAsync();
			}

			return resultado;
		}

		public async Task<DataTable> ObtenerContrarecibos(DateTime fechaA, DateTime fechaB)
		{
			DataTable resultado = new DataTable();

			try
			{
				await _con.OpenAsync();
				_cmd.CommandType = CommandType.StoredProcedure;

				_cmd.CommandText = "contrarecibo.ObtenerContrarecibos";
				_cmd.Parameters.AddWithValue("@fecha_inicio", fechaA);
				_cmd.Parameters.AddWithValue("@fecha_fin", fechaB);

				MySqlDataAdapter ad = new MySqlDataAdapter(_cmd);

				await ad.FillAsync(resultado);

				_cmd.CommandType = CommandType.Text;
				_cmd.Parameters.Clear();

			}
			catch (MySqlException ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await _con.CloseAsync();
			}
			return resultado;
		}


		public async Task<bool> GenerarContrarecibo(List<string> folios, DateTime fecha, string CodProv, decimal Monto)
		{
			MySqlTransaction transaction = null;
			try
			{
				await _con.OpenAsync();
				transaction = _con.BeginTransaction();


				_cmd.CommandText = "insert into contrarecibo.contrareciboheader " +
					"(codProveedor, MontoTotal, FechaDePago, estatus) " +
					$"values ('{CodProv}', {Monto}, '{fecha:yyyy/MM/dd}', 1)";

				_cmd.ExecuteNonQuery();

				_cmd.CommandText = "SELECT LAST_INSERT_ID();";

				var result = await _cmd.ExecuteScalarAsync();

				string id = result.ToString();

				foreach (string folio in folios)
				{
					_cmd.CommandText = $"insert into contrarecibo.contrarecibodetail values ({id}, '{folio}');" +
						$"delete from contrarecibo.folios where fol='{folio}' ";
					_cmd.ExecuteNonQuery();
				}

				await GenerarPdfContrarecibo(id, CodProv, fecha.ToString("dd/MM/yy"));

				//Confirmamos los cambios a la base de datos
				transaction.Commit();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				transaction.Rollback();
				return false;
			}
			finally
			{
				await _con.CloseAsync();
			}

			return true;
		}

		public async Task GenerarPdfContrarecibo(string id, string proveedor, string fechaPago)
		{
			bool conexionIinterna = false;

			string pdfPath = "contrarecibo.pdf";
			Document doc = new Document(PageSize.A4, 10, 10, 100, 50);

			using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				PdfWriter writer = PdfWriter.GetInstance(doc, fs);

				ClsHeaderContrarecibo pageEventHelper = new ClsHeaderContrarecibo("Imagenes/LOGO_EMPRESA-removebg-preview.png", "PRUEBA");
				writer.PageEvent = pageEventHelper;

				doc.Open();

				iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

				_cmd.CommandText = $"select nom_prov from tblcatproveedor where cod_prov='{proveedor}'";

				if (_con.State == ConnectionState.Closed)
				{
					await _con.OpenAsync();
					conexionIinterna = true;
				}

				var result = await _cmd.ExecuteScalarAsync();

				doc.Add(new Paragraph($"FOLIO: {id}   FECHA: {DateTime.Now:dd/MM/yy}  PROVEEDOR: {result} ", headerFont));
				doc.Add(new Paragraph("CONTRARECIBO", headerFont) { Alignment = Element.ALIGN_CENTER, PaddingTop = 15f, SpacingAfter = 20f });

				DataTable partidas = new DataTable();

				_cmd.CommandText = $@"SELECT ren.cod1_art, art.des1_art, sum(ren.can_art), sum(ren.importe)
										FROM tblcomprasren ren
										inner join tblcatarticulos art on art.cod1_art=ren.cod1_art
										inner join tblcomprasenc enc on enc.FOL_DOC=ren.FOL_DOC
										inner join contrarecibo.contrarecibodetail det on det.FolioCompra=enc.fol_doc
										where det.IDContrarecibo={id}
										group by art.cod1_art";

				MySqlDataAdapter ad = new MySqlDataAdapter(_cmd);

				await ad.FillAsync(partidas);

				PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
				float[] columnWidths = new float[] { 3f, 3f, 2f, 2f }; // Ajusta estos valores según sea necesario
				table.SetWidths(columnWidths);

				headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
				PdfPCell headerCell;

				// Añadir encabezados
				headerCell = new PdfPCell(new Phrase("CODIGO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
				table.AddCell(headerCell);
				headerCell = new PdfPCell(new Phrase("DESCRIPCION", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
				table.AddCell(headerCell);
				headerCell = new PdfPCell(new Phrase("PIEZAS", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
				table.AddCell(headerCell);
				headerCell = new PdfPCell(new Phrase("IMPORTE", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
				table.AddCell(headerCell);

				iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
				PdfPCell dataCell;

				double total = 0;

				foreach (DataRow row in partidas.Rows)
				{
					string codigo = row[0].ToString();
					string descripcion = row[1].ToString();
					string piezas = row[2].ToString();
					string importe = row[3].ToString();

					total += double.Parse(importe);

					dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(dataCell);
					dataCell = new PdfPCell(new Phrase(descripcion, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(dataCell);
					dataCell = new PdfPCell(new Phrase(piezas, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(dataCell);
					dataCell = new PdfPCell(new Phrase(importe, dataFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(dataCell);
				}

				dataCell = new PdfPCell(new Phrase()) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.NO_BORDER, PaddingBottom = 10f };
				table.AddCell(dataCell);
				table.AddCell(dataCell);
				table.AddCell(dataCell);

				dataFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
				dataCell = new PdfPCell(new Phrase($"Total: {total}", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
				table.AddCell(dataCell);

				// Añadir la tabla al documento
				doc.Add(table);

				doc.Add(new Paragraph($"Fecha de pago: {fechaPago}", new Font(Font.FontFamily.HELVETICA, 16)) { SpacingAfter = 15f });

				_cmd.CommandText = $"SELECT GROUP_CONCAT(FolioCompra SEPARATOR ' | ') AS Folios " +
					$"FROM contrarecibo.contrarecibodetail where idcontrarecibo={id};";

				result = await _cmd.ExecuteScalarAsync();

				doc.Add(new Paragraph($"Folios relacionados: {result}") { SpacingAfter = 20f });

				// Línea en blanco antes de las firmas
				doc.Add(new Paragraph("\n\n\n"));

				PdfPTable firmas = new PdfPTable(2);
				firmas.WidthPercentage = 100;
				firmas.SetWidths(new float[] { 1f, 1f }); // Columnas iguales

				// Línea de firma del proveedor
				PdfPCell firmaProveedor = new PdfPCell(new Phrase("____________________", dataFont))
				{
					Border = PdfPCell.NO_BORDER,
					HorizontalAlignment = Element.ALIGN_CENTER
				};
				firmas.AddCell(firmaProveedor);

				// Línea de firma de la empresa
				PdfPCell firmaEmpresa = new PdfPCell(new Phrase("________________________", dataFont))
				{
					Border = PdfPCell.NO_BORDER,
					HorizontalAlignment = Element.ALIGN_CENTER
				};
				firmas.AddCell(firmaEmpresa);

				// Texto debajo de la firma
				firmaProveedor = new PdfPCell(new Phrase("Proveedor", dataFont))
				{
					Border = PdfPCell.NO_BORDER,
					HorizontalAlignment = Element.ALIGN_CENTER
				};
				firmas.AddCell(firmaProveedor);

				firmaEmpresa = new PdfPCell(new Phrase("Empresa", dataFont))
				{
					Border = PdfPCell.NO_BORDER,
					HorizontalAlignment = Element.ALIGN_CENTER
				};
				firmas.AddCell(firmaEmpresa);

				// Agregar al documento
				doc.Add(firmas);

				if (conexionIinterna == true) await _con.CloseAsync();

				doc.Close();
			}

			Process.Start(pdfPath);
		}
	}
}
