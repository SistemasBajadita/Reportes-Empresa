using Aspose.Cells;
using Reportes.Modulos;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModuloDespProv;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Collections.Generic;
using Reportes.Modulos.Contrarecibo;

namespace Reportes
{
	public partial class FrmPrincipal : Form
	{
		private readonly string userid;
		private Action<string> sendText;

		public List<Button> GetAllButtons(Control parent)
		{
			List<Button> buttons = new List<Button>();

			foreach (Control control in parent.Controls)
			{
				if (control is Button button)
				{
					buttons.Add(button);
				}
				else if (control.HasChildren)
				{
					buttons.AddRange(GetAllButtons(control));
				}
			}
			return buttons;
		}

		public FrmPrincipal(string userid)
		{
			this.userid = userid;
			InitializeComponent();
			Text = $"Reportes - {(Program.Empresa == 0 ? "Jardines del Bosque" : "Colinas del Yaqui")}";
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["log"].ConnectionString);
			lblBienvenid.Text = $"Bienvenid@, {con.GetScalar($"select Name from users_reports.users where userid={userid}").Split(' ')[0]}";
		}

		private void BtnVentaCosto_Click(object sender, EventArgs e)
		{
			FrmVentaCosto frm = new FrmVentaCosto(int.Parse(userid));
			frm.ShowDialog();
		}

		private void BtnCompras_Click(object sender, EventArgs e)
		{
			FrmComprasPD frm = new FrmComprasPD();
			frm.ShowDialog();
		}

		private void BtnTopProductos_Click(object sender, EventArgs e)
		{
			FrmTopProductos frm = new FrmTopProductos();
			frm.ShowDialog();
		}

		private void BtnActiveReport_Click(object sender, EventArgs e)
		{
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings[$"{(Program.Empresa == 0 ? "servidor" : "marcos")}"].ToString());
			con.SetOnReportsCortes();
		}

		private void BtnCountProducts_Click(object sender, EventArgs e)
		{
			FrmConteo frm = new FrmConteo();
			frm.ShowDialog();
		}

		private void BtnMovAlm_Click(object sender, EventArgs e)
		{
			FrmMovimientos frm = new FrmMovimientos();
			frm.ShowDialog();
		}

		private void BtnVentasDetalladas_Click(object sender, EventArgs e)
		{
			FrmVentaDetallada frm = new FrmVentaDetallada();
			frm.ShowDialog();
		}

		private void BtnTicketChofer_Click(object sender, EventArgs e)
		{
			FrmTickets frm = new FrmTickets();
			frm.ShowDialog();
		}

		private void BtnNegativos_Click(object sender, EventArgs e)
		{
			ClsConnection _con = null;
			if (Program.Empresa == 0)
			{
				_con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			}
			else if (Program.Empresa == 1)
			{
				_con = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());
			}
			_ = _con.GetQuery($@"SELECT 
									art.cod1_art,
									CONCAT(art.des1_art, ' (', und.cod_und, ')'),
									agr.DES_AGR,
									exi.EXI_ALM
								FROM
									tblcatarticulos art
										LEFT JOIN
									tblgpoarticulos gpo ON gpo.cod1_art = art.cod1_art
										INNER JOIN
									tblexiporalmacen exi on exi.COD1_ART=art.cod1_art and exi.COD_ALM='A001'
										INNER JOIN
									tblcatagrupacionart agr ON agr.COD_AGR = gpo.COD_AGR
										INNER JOIN
									tblundcospreart und ON und.cod1_art = art.cod1_art
										AND und.eqv_und = 1
								WHERE
									art.EXI_ACT < 0 AND gpo.COD_GPO = {(Program.Empresa == 0 ? "25" : "1")}
								ORDER BY agr.des_agr ASC; ");
			DialogResult response = MessageBox.Show($"Hay {_con.GetScalar("select count(art.cod1_art) from tblcatarticulos art inner join tblexiporalmacen exi on exi.cod1_art=art.cod1_art where exi.exi_alm<0 and exi.cod_alm='A001'")} productos con existencia negativa\n" +
				$"¿Deseas generar el archivo txt para hacer ajuste de negativos?", "La Bajadita - Existencias Negativas", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (response == DialogResult.Yes)
			{
				SaveFileDialog save = new SaveFileDialog();

				save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

				// Nombre del archivo que quieres guardar
				save.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Ajuste de negativos.txt";


				if (save.ShowDialog() == DialogResult.OK)
				{
					_con.GetTxtExistencias(save.FileName);
				}
			}

			_con.PrintReportInPDFNegativos($"Negativos en inventario\nSucursal: {(Program.Empresa == 0 ? "Jardines del Bosque" : "Colinas del Yaqui")}");
		}

		private async void BtnCocinaPrecios_Click(object sender, EventArgs e)
		{
			ClsConnection bd = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Archivos de Excel (*.xls)|*.xls";

			if (open.ShowDialog() == DialogResult.OK)
			{
				ProgressProductsCocina progressForm = new ProgressProductsCocina();
				sendText = progressForm.SetMessage;
				progressForm.Show();

				var progress = new Progress<int>(value =>
				{
					progressForm.UpdateProgress(value);
				});

				await Task.Run(() => ProcessExcel(open.FileName, bd, progress));

				progressForm.Close();

				MessageBox.Show("Proceso completado y archivo Excel actualizado.", "La Bajadita -  Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void ProcessExcel(string filePath, ClsConnection bd, IProgress<int> progress)
		{
			Workbook workbook = new Workbook(filePath);

			Worksheet hoja = workbook.Worksheets[0];

			int totalRows = hoja.Cells.MaxDataRow + 1;
			int r = 1;
			Cell codigo = hoja.Cells[r, 0];

			while (!string.IsNullOrEmpty(codigo.Value?.ToString()))
			{
				string q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
							  FROM tblcatarticulos art 
							  INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
							  WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{codigo.Value}';";

				DataTable query = bd.GetQuery(q);

				if (query.Rows.Count > 0)
				{
					string precioBD = query.Rows[0][1].ToString();
					string precioExcel = hoja.Cells[r, 2].Value?.ToString();

					if (precioExcel != precioBD)
					{
						hoja.Cells[r, 2].PutValue(precioBD);
						sendText($"Se actualizo el precio de {codigo} de {precioExcel} a {precioBD}");
					}
				}
				else
				{
					// Manejo de códigos de 3 y 4 dígitos
					if (codigo.Value.ToString().Length == 3)
					{
						string nuevoCodigo = "0" + codigo.Value.ToString();
						q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
							   FROM tblcatarticulos art 
							   INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
							   WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
						query = bd.GetQuery(q);

						if (query.Rows.Count == 0)
						{
							nuevoCodigo = nuevoCodigo.Insert(3, "-");
							q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
								   FROM tblcatarticulos art 
								   INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
								   WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
							query = bd.GetQuery(q);
						}

						if (query.Rows.Count > 0)
						{
							string precioBD = query.Rows[0][1].ToString();
							string precioExcel = hoja.Cells[r, 2].Value?.ToString();

							if (precioExcel != precioBD)
							{
								hoja.Cells[r, 2].PutValue(precioBD);
								sendText($"Se actualizo el precio de {codigo} de {precioExcel} a {precioBD}");
							}
						}
						else
						{
							sendText($"El código {codigo.Value} no se encuentra en la base de datos.");
						}
					}
					else if (codigo.Value.ToString().Length == 4)
					{
						string nuevoCodigo = codigo.Value.ToString().Insert(3, "-");
						q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
							   FROM tblcatarticulos art 
							   INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
							   WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
						query = bd.GetQuery(q);

						if (query.Rows.Count > 0)
						{
							string precioBD = query.Rows[0][1].ToString();
							string precioExcel = hoja.Cells[r, 2].Value?.ToString();

							if (precioExcel != precioBD)
							{
								hoja.Cells[r, 2].PutValue(precioBD);
								sendText($"Se actualizo el precio de {codigo} de {precioExcel} a {precioBD}");
							}
						}
						else
						{
							sendText($"El código {codigo.Value} no se encuentra en la base de datos.");
						}
					}
				}
				r++;
				codigo = hoja.Cells[r, 0];

				int progressPercentage = (int)((r / (float)totalRows) * 100);
				progress.Report(progressPercentage);
			}
			workbook.Save(filePath);
		}

		FrmDespProv frm;

		private void button1_Click(object sender, EventArgs e)
		{
			frm = new FrmDespProv(ConfigurationManager.ConnectionStrings[Program.Empresa == 0 ? "servidor" : "marcos"].ToString());
			frm.MandarDataTable += RecibirDataTable;
			frm.ShowDialog();
		}

		private async void RecibirDataTable(string prov, DataTable datos, DateTime FechaA, DateTime FechaB)
		{
			ClsConnection con = null;

			if (Program.Empresa == 0)
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			else if (Program.Empresa == 1)
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());

			frm.ActiveReport(true);
			await Task.Run(() =>
			{

				string name = con.GetScalar($"select nom_prov from tblcatproveedor where cod_prov='{prov}'");
				try
				{
					Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
					string pdfPath = "desp.pdf";

					using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
					{
						PdfWriter writer = PdfWriter.GetInstance(doc, fs);

						ClsHeader pageEventHelper = new ClsHeader("Imagenes/LOGO_EMPRESA-removebg-preview.png", name != "" ? $"Proveedor: {name}\nPeriodo: {FechaA:dd/MM/yy} a {FechaB:dd/MM/yy} " : $"Todos los proveedores\nPeriodo: {FechaA:dd/MM/yy} a {FechaB:dd/MM/yy}", $"Desplazamiento de proveedor\nSucursal: {(Program.Empresa == 0 ? "Jardines del Bosque" : "Colinas del Yaqui")}");
						writer.PageEvent = pageEventHelper;

						doc.Open();

						PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
						float[] columnWidths = new float[] { 2f, 2f, 1f, 1f }; // Ajusta estos valores según sea necesario
						table.SetWidths(columnWidths);

						iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
						PdfPCell headerCell;

						// Añadir encabezados

						iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
						PdfPCell dataCell;

						if (prov != "")
						{
							headerCell = new PdfPCell(new Phrase("Código", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("Descripción", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("Desplazamiento", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("Unidad", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
							table.AddCell(headerCell);

							foreach (DataRow row in datos.Rows)
							{
								string codigo = row[0].ToString();
								string descripcion = row[1].ToString();
								string costo = double.Parse(row[2].ToString()).ToString("N2");
								string existencia = row[3].ToString();

								dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
								table.AddCell(dataCell);
								dataCell = new PdfPCell(new Phrase(descripcion, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
								table.AddCell(dataCell);
								dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
								table.AddCell(dataCell);
								dataCell = new PdfPCell(new Phrase(existencia, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
								table.AddCell(dataCell);
							}

							doc.Add(table);

							doc.Close();
							writer.Close();
						}
						else
						{
							DataTable provT = con.GetQuery("select cod_prov, nom_prov from tblcatproveedor;");
							foreach (DataRow p in provT.Rows)
							{
								DataTable productos = con.GetQuery($@"SELECT 
													ren.cOD1_ART AS Codigo, 
													art.DES1_ART AS Descripcion, 
													ROUND(SUM(ren.CAN_ART), 2) AS Desplazamiento, 
													und.DES_UND AS Unidad
												FROM tblrenventas ren
												INNER JOIN tblgralventas enc ON enc.REF_DOC = ren.REF_DOC
												INNER JOIN tblcatarticulos art ON art.cod1_art = ren.cod1_Art 
												INNER JOIN tblcatunidades und ON und.COD_UND = ren.COD_UND
												INNER JOIN tblartiproveedor prov ON prov.cod1_Art = ren.cod1_Art
												WHERE enc.FEC_DOC BETWEEN '{FechaA:yyyy-MM-dd}' AND '{FechaB:yyyy-MM-dd}' 
												AND prov.COD_PROV = '{p[0]}'
												GROUP BY ren.cOD1_ART, art.DES1_ART, und.DES_UND;");

								if (productos.Rows.Count > 0)
								{
									doc.Add(new Paragraph($"Proveedor: {p[1]}", headerFont) { Alignment = Element.ALIGN_CENTER });
									table = new PdfPTable(4) { WidthPercentage = 100 };
									columnWidths = new float[] { 2f, 2f, 1f, 1f };
									table.SetWidths(columnWidths);

									headerCell = new PdfPCell(new Phrase("Código", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
									table.AddCell(headerCell);
									headerCell = new PdfPCell(new Phrase("Descripción", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
									table.AddCell(headerCell);
									headerCell = new PdfPCell(new Phrase("Desplazamiento", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
									table.AddCell(headerCell);
									headerCell = new PdfPCell(new Phrase("Unidad", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
									table.AddCell(headerCell);

									foreach (DataRow row in productos.Rows)
									{
										string codigo = row[0].ToString();
										string descripcion = row[1].ToString();
										string costo = double.Parse(row[2].ToString()).ToString("N2");
										string existencia = row[3].ToString();

										dataCell = new PdfPCell(new Phrase(codigo, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
										table.AddCell(dataCell);
										dataCell = new PdfPCell(new Phrase(descripcion, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
										table.AddCell(dataCell);
										dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
										table.AddCell(dataCell);
										dataCell = new PdfPCell(new Phrase(existencia, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
										table.AddCell(dataCell);
									}
									doc.Add(table);
									doc.Add(new Paragraph("\n") { Alignment = Element.ALIGN_CENTER });
								}
							}
							doc.Close();
							writer.Close();
						}
						Process.Start("desp.pdf");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			});
			frm.ActiveReport(false);
		}

		private async void FrmPrincipal_Load(object sender, EventArgs e)
		{
			ControlBox = false;

			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");

			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["log"].ToString());
			List<Button> buttons = GetAllButtons(this);

			foreach (Button b in buttons)
			{
				string result = "False";
				await Task.Run(() => result = con.GetScalar($"select {b.Name} from users_roles where userid={userid}"));
				if (result == "True") b.Enabled = true;
				else { b.Enabled = false; b.Visible = false; }
			}

			string super = "False";

			await Task.Run(() => super = con.GetScalar($"select super from users_roles where userid={userid}"));

			modificarRolesToolStripMenuItem.Visible = super == "True";
			verMovimientosToolStripMenuItem.Visible = super == "True";

			if (Program.Empresa == 1)
			{
				toolTip1.SetToolTip(Tickets, "Esta opcion no esta disponible en la sucursal de Colinas del Yaqui");
				Tickets.Click -= BtnTicketChofer_Click;
			}
		}

		private void modificarRolesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FrmRoles prueba = new FrmRoles();
			prueba.ShowDialog();
		}

		private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void Cerrar()
		{
			DialogResult respuesta =
			MessageBox.Show("¿Deseas entrar con otro usuario?", "Cerrar sesion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (respuesta == DialogResult.Yes)
			{
				Application.Restart();
			}
			else if (respuesta == DialogResult.No)
			{
				Application.Exit();
			}
		}

		private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cerrar();
		}

		private void Tortillas_Click(object sender, EventArgs e)
		{
			FrmVentaDeTortilla frm = new FrmVentaDeTortilla();
			frm.ShowDialog();
		}

		private void Codigos_Click(object sender, EventArgs e)
		{
			FrmCodigo frm = new FrmCodigo();
			frm.ShowDialog();
		}

		private void Precios_Click(object sender, EventArgs e)
		{
			FrmPrecios frm = new FrmPrecios();
			frm.ShowDialog();
		}

		private void Clientes_Click(object sender, EventArgs e)
		{
			FrmClientes clientes = new FrmClientes();
			clientes.ShowDialog();
		}

		private void Contrarecibo_Click(object sender, EventArgs e)
		{
			FrmContrareciboMenu frm = new FrmContrareciboMenu();
			frm.ShowDialog();
		}
	}
}
