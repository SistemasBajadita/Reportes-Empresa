using Aspose.Cells;
using Aspose.Cells.Charts;
using Aspose.Cells.Tables;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec.wmf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Reportes
{
	public class ClsConnection
	{
		public Action<DataTable> sendReport;
		public Action<DataSet> sendTables;
		readonly string con;
		private DataTable ReporteActivo;

		public string GetRowQuery(string query)
		{
			MySqlConnection _con = new MySqlConnection(con);
			try
			{
				_con.Open();

				MySqlCommand cmd = new MySqlCommand(query, _con);
				MySqlDataReader rd = cmd.ExecuteReader();

				if (rd.Read())
				{
					return $"{rd.GetString(0)}, {rd.GetDecimal(1)}";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				_con.Close();
			}

			return "";
		}

		public void SetOnReportsCortes()
		{
			MySqlConnection _con = new MySqlConnection(this.con);
			try
			{
				_con.Open();
				MySqlCommand cmd = _con.CreateCommand();
				cmd.CommandText = "SELECT @@SQL_MODE;\r\nSET GLOBAL sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''));";
				cmd.ExecuteNonQuery();
				MessageBox.Show("Habilitacion completada, verifica el reporte nuevamente", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (MySqlException ex)
			{
				MessageBox.Show("Ocurrio un error al intentar conectarse a la base de datos. \n" + ex.Message, "La Bajadita - Venta de Frutas y Verduras");
			}
			finally
			{
				_con.Close();
			}
		}

		public ClsConnection(string con)
		{
			this.con = con;
		}

		public DataTable GetQuery(string query)
		{
			MySqlConnection con = new MySqlConnection(this.con); ;
			DataTable reporte = new DataTable();
			try
			{
				con = new MySqlConnection(this.con);
				con.Open();

				MySqlCommand cmd = new MySqlCommand(query, con);
				cmd.ExecuteNonQuery();

				MySqlDataAdapter ad = new MySqlDataAdapter(cmd);

				ad.Fill(reporte);
				ReporteActivo = reporte;
			}
			catch (MySqlException ex)
			{
				MessageBox.Show($"Ocurrio un error\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				con.Close();
			}
			return reporte;
		}

		public DataTable GetQuery2(string query)
		{
			MySqlConnection con = new MySqlConnection(this.con); ;
			DataTable reporte = new DataTable();
			try
			{
				con = new MySqlConnection(this.con);
				con.Open();

				MySqlCommand cmd = new MySqlCommand(query, con);
				cmd.ExecuteNonQuery();

				MySqlDataAdapter ad = new MySqlDataAdapter(cmd);

				ad.Fill(reporte);
			}
			catch (MySqlException ex)
			{
				MessageBox.Show($"Ocurrio un error\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				con.Close();
			}
			return reporte;
		}

		public void SetQuery(string query)
		{
			MySqlConnection con = new MySqlConnection(this.con);
			DataTable reporte = new DataTable();
			try
			{
				con = new MySqlConnection(this.con);
				con.Open();

				MySqlCommand cmd = new MySqlCommand(query, con);
				cmd.CommandTimeout = 540;
				cmd.ExecuteNonQuery();

				MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
				DataSet tables = new DataSet();

				if (query.Split(';').Length > 2)
				{
					ad.Fill(tables);
					sendTables?.Invoke(tables);
					ReporteActivo = tables.Tables[0];
					return;
				}

				ad.Fill(reporte);
				sendReport?.Invoke(reporte);
			}
			catch (MySqlException ex)
			{
				if (!ex.Message.Contains("already"))
					MessageBox.Show($"Ocurrio un error\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				con.Close();
			}
			ReporteActivo = reporte;
		}

		public void PrintReportInPDFCompras(string fechaA, string fechaB, string path)
		{
			try
			{
				Document doc = new Document();
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Título del documento
					iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
					Paragraph title = new Paragraph("Reporte de Compras", titleFont);
					title.Alignment = Element.ALIGN_CENTER;
					doc.Add(title);

					// Subtítulo con el periodo
					string periodo = $"Periodo: {fechaA} al {fechaB}";
					Paragraph period = new Paragraph(periodo)
					{
						Alignment = Element.ALIGN_CENTER
					};
					doc.Add(period);

					// Añadir un espacio después del encabezado
					doc.Add(new Paragraph("\n"));

					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(4);
					table.WidthPercentage = 100;

					// Establecer anchos de las columnas
					float[] columnWidths = new float[] { 3f, 2f, 2f, 2f }; // Ajusta estos valores según sea necesario
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;

					// Añadir encabezados
					headerCell = new PdfPCell(new Phrase("Departamento", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Total de compra", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("IVA", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f }; // Celda vacía para la columna de totales
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					double totalCompra = 0;
					double totalImpuesto = 0;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string departamento = row["Departamento"].ToString();
						string venta = "$" + double.Parse(row["Total"].ToString()).ToString("N2");
						string costo = "$" + double.Parse(row["Iva"].ToString()).ToString("N2");

						dataCell = new PdfPCell(new Phrase(departamento, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(venta, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase("", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f }; // Celda vacía para la columna de totales
						table.AddCell(dataCell);

						totalCompra += double.Parse(row["Total"].ToString());
						totalImpuesto += double.Parse(row["Iva"].ToString());
					}

					// Añadir fila de totales
					PdfPCell totalCell;
					totalCell = new PdfPCell(new Phrase("TOTAL:", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f, Colspan = 1 };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("$" + totalCompra.ToString("N2"), headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("$" + totalImpuesto.ToString("N2"), headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);

					Paragraph date = new Paragraph($"Fecha: {DateTime.Now}")
					{
						Alignment = Element.ALIGN_LEFT
					};

					doc.Add(table);
					doc.Add(new Paragraph("\n"));

					Paragraph nota = new Paragraph($"Nota:\n ")
					{
						Alignment = Element.ALIGN_LEFT
					};

					doc.Add(date);
					doc.Close();
					writer.Close();
				}
				MessageBox.Show("Reporte PDF generado exitosamente, por favor espera y se abrirá el archivo automáticamente después de que cierres este mensaje.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrió un error al generar el PDF\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void PrintReportInPDFTOP(string fechaA, string fechaB, string path, string tipo, string departamento)
		{
			try
			{
				Document doc = new Document();
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					// Añadir el evento de página para el encabezado con imagen
					//ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
					//writer.PageEvent = pageEventHelper;

					doc.Open();

					// Título del documento
					iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
					Paragraph title = new Paragraph($"Reporte de productos mas vendidos por {tipo}", titleFont);
					title.Alignment = Element.ALIGN_CENTER;
					doc.Add(title);

					// Subtítulo con el periodo
					string periodo = $"Periodo: {fechaA} al {fechaB}";
					Paragraph period = new Paragraph(periodo)
					{
						Alignment = Element.ALIGN_CENTER
					};// Alineación centrada
					doc.Add(period);

					period = new Paragraph($"\nDepartamento: {departamento}")
					{
						Alignment = Element.ALIGN_CENTER
					};// Alineación centrada
					doc.Add(period);

					// Añadir un espacio después del encabezado
					doc.Add(new Paragraph("\n"));

					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(4);
					table.WidthPercentage = 100;

					// Añadir encabezados
					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12); // Fuente en negrita y tamaño 12
					PdfPCell headerCell;
					headerCell = new PdfPCell(new Phrase(ReporteActivo.Columns[0].ColumnName, headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase(ReporteActivo.Columns[1].ColumnName, headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase(ReporteActivo.Columns[2].ColumnName, headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase(ReporteActivo.Columns[3].ColumnName, headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					// Añadir datos
					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string codigo = row[0].ToString();
						string descripcion = row[1].ToString();
						string existencia = row[2].ToString();
						string parametro = row[3].ToString();

						dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(descripcion, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(existencia, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(parametro, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);



					}

					Paragraph date = new Paragraph($"Fecha: {DateTime.Now}");
					date.Alignment = Element.ALIGN_LEFT;
					doc.Add(table);
					doc.Add(new Paragraph("\n"));
					doc.Add(date);
					doc.Close();
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "La Bajadita - Venta de Frutas y Verduras");
			}
		}


		public void PrintReportInPDFVentas(string fechaA, string fechaB, string path)
		{
			try
			{
				DataTable merma = GetQuery2($@" select caa.DES_AGR as Departamento, round(sum(cos_uni*can_ren),2) as Merma
												from tblrenalmacen ren_alm
												inner join tblgralalmacen enc_alm on ren_alm.REF_MOV=enc_alm.REF_MOV
												inner join tblgpoarticulos ga on ren_alm.COD1_ART = ga.COD1_ART 
												inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR
												where (enc_alm.FEC_MOV between'{fechaA}' and '{fechaB}') and enc_alm.cod_con='SMER' and caa.COD_GPO=25
												group by caa.des_agr;");

				Document doc = new Document(PageSize.A4, 10, 10, 30, 50);
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					// Añadir el evento de página para el encabezado con imagen
					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Título del documento
					iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
					Paragraph title = new Paragraph("Reporte de Ventas y Costos", titleFont);
					title.Alignment = Element.ALIGN_CENTER;
					doc.Add(title);

					// Subtítulo con el periodo
					string periodo = $"Periodo: {fechaA} al {fechaB}";
					Paragraph period = new Paragraph(periodo)
					{
						Alignment = Element.ALIGN_CENTER
					};// Alineación centrada
					doc.Add(period);

					// Añadir un espacio después del encabezado
					doc.Add(new Paragraph("\n"));

					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(6);
					table.WidthPercentage = 100;
					//float[] columnWidths = new float[] { 1f, 2f, 1f, 1f, 1f, 1f }; // Ajusta estos valores según sea necesario
					//table.SetWidths(columnWidths);

					// Añadir encabezados
					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12); // Fuente en negrita y tamaño 12
					PdfPCell headerCell;
					headerCell = new PdfPCell(new Phrase("Departamento", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Venta", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Costo", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Utilidad", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Merma", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("M/V *", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					// Añadir datos
					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					double totalVenta = 0;
					double totalCosto = 0;
					double mermaTotal = 0;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string departamento = row[0].ToString();
						string venta = "$" + double.Parse(row[1].ToString()).ToString("N2");
						string costo = "$" + double.Parse(row[2].ToString()).ToString("N2");
						string utilidad = row[3].ToString() + "%";


						dataCell = new PdfPCell(new Phrase(departamento, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(venta, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(utilidad, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);

						double mermaActual = 0;
						

						for (int i = 0; i < merma.Rows.Count; i++)
						{
							if (merma.Rows[i][0].ToString() == departamento)
							{
								mermaActual = Convert.ToDouble(merma.Rows[i][1].ToString());
								mermaTotal += Convert.ToDouble(merma.Rows[i][1].ToString());
								break;
							}
						}
						if (mermaActual != 0)
						{
							dataCell = new PdfPCell(new Phrase($"${mermaActual.ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{Math.Round((mermaActual / double.Parse(row[1].ToString())) * 100,2)}%", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(dataCell);
						}
						else
						{
							dataCell = new PdfPCell(new Phrase($"$0.00", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"0.00%", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(dataCell);
						}

						totalVenta += double.Parse(row[1].ToString());
						totalCosto += double.Parse(row[2].ToString());
					}

					// Añadir fila de totales
					PdfPCell totalCell;
					totalCell = new PdfPCell(new Phrase("TOTAL:", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("$" + totalVenta.ToString("N2"), headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("$" + totalCosto.ToString("N2"), headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase($"${Math.Round(mermaTotal,2):N2}", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);
					totalCell = new PdfPCell(new Phrase("", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 10f };
					table.AddCell(totalCell);

					Paragraph date = new Paragraph($"Fecha: {DateTime.Now}                                                                                    *MERMA/VENTA");
					date.Alignment = Element.ALIGN_LEFT;

					doc.Add(table);
					doc.Add(new Paragraph("\n"));
					doc.Add(date);
					doc.Close();
					writer.Close();
				}
				MessageBox.Show("Reporte PDF generado exitosamente, por favor espere y se abrirá el archivo automáticamente después de que cierre este mensaje.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrió un error al generar el PDF\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void PrintReportInExcel(string path)
		{
			Workbook workbook = new Workbook();
			Worksheet worksheet = workbook.Worksheets[0];

			// Importar los datos desde el DataTable
			worksheet.Cells.ImportData(ReporteActivo, 1, 0, new ImportTableOptions { IsFieldNameShown = true });
			worksheet.AutoFitColumns();

			// Aplicar formato a las columnas B y C (indices 1 y 2)
			Style style = workbook.CreateStyle();
			style.Custom = "$ #,##0.00";
			Range rangeB = worksheet.Cells.CreateRange(1, 1, ReporteActivo.Rows.Count, 1); // Columna B
			Range rangeC = worksheet.Cells.CreateRange(1, 2, ReporteActivo.Rows.Count, 1); // Columna C
			rangeB.ApplyStyle(style, new StyleFlag() { NumberFormat = true });
			rangeC.ApplyStyle(style, new StyleFlag() { NumberFormat = true });

			// Agregar el título
			worksheet.Cells[0, 0].PutValue("Reporte de ventas por departamento con costo");
			Style styleHeader = worksheet.Cells[0, 0].GetStyle();
			styleHeader.HorizontalAlignment = TextAlignmentType.Center;
			styleHeader.VerticalAlignment = TextAlignmentType.Center;
			styleHeader.Font.IsBold = true;
			worksheet.Cells[0, 0].SetStyle(styleHeader);

			// Fusionar celdas para el título
			int totalCols = ReporteActivo.Columns.Count;
			Range range = worksheet.Cells.CreateRange(0, 0, 1, totalCols);
			range.Merge();

			// Crear la tabla a partir del rango de datos
			int totalRows = ReporteActivo.Rows.Count + 1; // +1 para incluir los encabezados
			ListObjectCollection tables = worksheet.ListObjects;
			int index = tables.Add(1, 0, totalRows, totalCols - 1, true); // Ajustar totalCols-1
			ListObject table = tables[index];
			table.TableStyleType = TableStyleType.TableStyleMedium2;

			// Guardar el archivo Excel
			try
			{
				workbook.Save(path, SaveFormat.Xlsx);
				MessageBox.Show("Reporte Excel generado exitosamente, por favor espera y se abrira el archivo automaticamente despues de que cierres este mensaje.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (IOException)
			{
				MessageBox.Show("Ocurrio un error al guardar el archivo. " +
					"Si tienes abierto un archivo con el mismo nombre que " +
					"quieres asignar por favor cierralo y vuelve a intentarlo.", "La Bajadita - Venta de Frutas y Verduras",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void PrintReportInPDFExistencia(string path, string encabezado)
		{
			try
			{
				Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsHeader pageEventHelper = new ClsHeader("Imagenes/LOGO_EMPRESA-removebg-preview.png", encabezado, "Hojas de conteo");
					writer.PageEvent = pageEventHelper;

					doc.Open();



					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(5) { WidthPercentage = 100 };
					float[] columnWidths = new float[] { 3f, 3f, 2f, 2f, 2f }; // Ajusta estos valores según sea necesario
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;

					// Añadir encabezados
					headerCell = new PdfPCell(new Phrase("Código", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Descripción", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Costo", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Existencia", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Existencia Real", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string codigo = row["Codigo"].ToString();
						string descripcion = row["Descripcion"].ToString();
						string costo = "$" + double.Parse(row["Costo"].ToString()).ToString("N2");
						string existencia = row["Existencia"].ToString(); // Suponiendo que esta columna está en el DataTable

						dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(descripcion, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(existencia, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase("____________", dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
					}

					// Añadir la tabla al documento
					doc.Add(table);

					doc.Close();
					writer.Close();
				}
				MessageBox.Show("Reporte PDF generado exitosamente, por favor espera y se abrirá el archivo automáticamente después de que cierres este mensaje.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrió un error al generar el PDF\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void PrintReportForMovimientos(string fechaA, string fechaB, string path, string movimiento)
		{
			try
			{
				Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Título del documento
					iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
					Paragraph title = new Paragraph($"Reporte de {movimiento}", titleFont)
					{
						Alignment = Element.ALIGN_CENTER
					};
					doc.Add(title);

					// Añadir un espacio después del encabezado
					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph($"Periodo: {fechaA} al {fechaB}"));
					doc.Add(new Paragraph("\n"));


					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
					float[] columnWidths = new float[] { 3f, 3f }; // Ajusta estos valores según sea necesario
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;

					// Añadir encabezados
					headerCell = new PdfPCell(new Phrase("Departamento", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("Total", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string codigo = row[0].ToString();
						string costo = row[1].ToString();

						dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
					}

					// Añadir la tabla al documento
					doc.Add(table);

					decimal total = Convert.ToDecimal(ReporteActivo.Compute($"SUM(Total)", string.Empty));

					doc.Add(new Paragraph($"Total: {total}"));

					doc.Add(new Paragraph($"\nFecha: {DateTime.Now.ToString("yyyy/MM/dd")}"));

					doc.Close();
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrio un error al generar el PDF\n{ex.Message}", "La Bajadita - Venta de Frutas y Verduras");
			}
		}

		public void PrintReportMovimientosDetail(string departamento, string titulo)
		{
			try
			{
				Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
				string pdfPath = "movimientos.pdf";
				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsHeader pageEventHelper = new ClsHeader("Imagenes/LOGO_EMPRESA-removebg-preview.png", departamento, titulo);
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
					float[] columnWidths = new float[] { 3f, 3f, 3f, 3f }; // Ajusta estos valores según sea necesario
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;

					// Añadir encabezados
					headerCell = new PdfPCell(new Phrase("CODIGO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("DESCRIPCION", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("PIEZAS", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("COSTO", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						dataCell = new PdfPCell(new Phrase(row[0].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[1].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[2].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[3].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
					}

					doc.Add(table);

					doc.Close();
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintReportPDFMargen(string departamento, string titulo)
		{
			try
			{
				Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
				string pdfPath = "margenes.pdf";
				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsHeader pageEventHelper = new ClsHeader("Imagenes/LOGO_EMPRESA-removebg-preview.png", departamento, titulo);
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Crear una tabla para los datos
					PdfPTable table = new PdfPTable(6) { WidthPercentage = 100 };
					float[] columnWidths = new float[] { 3f, 3f, 3f, 3f, 3f, 3f }; // Ajusta estos valores según sea necesario
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;

					// Añadir encabezados
					headerCell = new PdfPCell(new Phrase("CODIGO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("DESCRIPCION", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("COSTO PROMEDIO", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("ULTIMA COMPRA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("PRECIO ACTUAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("MARGEN ACTUAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						dataCell = new PdfPCell(new Phrase(row[0].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[1].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase("$" + row[2].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[3].ToString() != "" ? "$" + row[3].ToString() : "SIN COMPRAS", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase("$" + row[4].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[5].ToString() + "%", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
					}
					// Añadir la tabla al documento
					doc.Add(table);
					doc.Close();
					writer.Close();
				}
				Process.Start(pdfPath);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "La Bajadita - Venta de Frutas y Verduras");
			}
		}

		public void PrintReportInPDFNegativos(string titulo)
		{
			try
			{
				Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
				string pdfPath = "negativos.pdf";
				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsHeader pageEventHelper = new ClsHeader("Imagenes/LOGO_EMPRESA-removebg-preview.png", "", titulo);
					writer.PageEvent = pageEventHelper;

					doc.Open();
					PdfPTable table = new PdfPTable(3) { WidthPercentage = 100 };
					float[] columnWidths = new float[] { 4f, 7f, 3f };
					table.SetWidths(columnWidths);

					iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
					PdfPCell headerCell;
					headerCell = new PdfPCell(new Phrase("CODIGO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("DESCRIPCION", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("EXISTENCIA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);

					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;
					foreach (DataRow row in ReporteActivo.Rows)
					{
						dataCell = new PdfPCell(new Phrase(row[0].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 6f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[1].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 6f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(row[2].ToString(), dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 6f };
						table.AddCell(dataCell);
					}

					doc.Add(table);
					doc.Close();
					writer.Close();
					Process.Start(pdfPath);

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
//Programado por Bryan Valdez Muñoz
//6312988689
//alan272_@hotmail.com