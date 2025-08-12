using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Reportes
{
	public partial class FrmTickets : Form
	{
		ClsConnection conn;
		public FrmTickets()
		{
			InitializeComponent();
			conn = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			pictureBox1.Image = System.Drawing.Image.FromFile("Imagenes/load.gif");
			pictureBox1.Visible = false;

			FechaA.MaxDate = DateTime.Now;
			FechaB.MaxDate = DateTime.Now;
		}

		private string GetSelectedTextFromCombo()
		{
			if (cmbVendedor.SelectedItem != null)
			{
				if (cmbVendedor.SelectedItem is DataRowView selectedRow)
				{
					string selectedText = selectedRow[cmbVendedor.DisplayMember].ToString();
					return selectedText;
				}
			}
			return null;
		}

		private async void BtnPrintReport_Click(object sender, EventArgs e)
		{
			DataTable tickets = new DataTable();
			string cod = cmbVendedor.SelectedValue.ToString();
			string fechaA = FechaA.Value.ToString("yyyy-MM-dd");
			string fechaB = FechaB.Value.ToString("yyyy-MM-dd");

			if (FechaA.Value.Date < new DateTime(2025, 02, 01) || FechaB.Value.Date < new DateTime(2025, 02, 01))
			{
				conn = new ClsConnection(ConfigurationManager.ConnectionStrings["antes"].ToString());
			}

			await Task.Run(() => tickets = conn.GetQuery($@"select DISTINCT ven.fec_doc, ven.ref_doc, cli.NOM_CLI, hora_reg, round(tot_doc,2), ven.COD_USU, ren.cod_ven
												            from tblgralventas ven
												            inner join tblcatclientes cli on cli.COD_Cli=ven.COD_CLI
												            inner join tblrenventas ren on ren.REF_DOC=ven.REF_DOC
												            where CAJA_DOC=9 
												            and (ven.FEC_DOC between '{fechaA}' and '{fechaB}')
												            and ren.cod_ven='{cod}';"));

			try
			{
				Document doc = new Document();
				string pdfPath = "reporte.pdf";

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
					writer.PageEvent = pageEventHelper;

					doc.Open();

					// Título del documento
					Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

					doc.Add(new Paragraph("                         CURLANGO RAMOS CHRISTIAN YARELY \n" +
						"                         R.F.C CURC890920PW1", titleFont)
					{ Alignment = Element.ALIGN_LEFT });
					//Paragraph title = new Paragraph("                         LA BAJADITA - VENTA DE FRUTAS Y VERDURAS", titleFont);
					//title.Alignment = Element.ALIGN_LEFT;
					//doc.Add(title);
					doc.Add(new Paragraph("                         CALLE AVENIDA DE LOS MAESTROS #42 LOCAL 10", titleFont) { Alignment = Element.ALIGN_LEFT });
					doc.Add(new Paragraph("                         COL. JARDINES DEL BOSQUE", titleFont) { Alignment = Element.ALIGN_LEFT });
					doc.Add(new Paragraph("                         C.P. 84063 H. NOGALES, SONORA", titleFont) { Alignment = Element.ALIGN_LEFT });

					doc.Add(new Paragraph("\n"));
					doc.Add(new Paragraph($"REPORTE DE TICKETS POR CHOFER\nCHOFER: {GetSelectedTextFromCombo()}\nPERIODO: {FechaA.Value.ToString("dd/MM/yyyy")} a {FechaB.Value.ToString("dd/MM/yyyy")}"));
					doc.Add(new Paragraph("\n"));

					PdfPTable table = new PdfPTable(6);
					table.WidthPercentage = 100;

					float[] columnWidths = new float[] { 1f, 2f, 2f, 1f, 1f, 1f };
					table.SetWidths(columnWidths);

					Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
					PdfPCell headerCell;

					headerCell = new PdfPCell(new Phrase("FECHA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("FOLIO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("CLIENTE", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("HORA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("TOTAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);
					headerCell = new PdfPCell(new Phrase("DEV", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
					table.AddCell(headerCell);

					PdfPCell dataCell;
					Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

					string query = "";
					double devTotal = 0;

					foreach (DataRow r in tickets.Rows)
					{
						query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5;";

						string ro = conn.GetRowQuery(query);

						bool creditFlag = false;

						if (conn.GetScalar($"select cod_frp from tblauxcaja where ref_doc = '{r[1]}'") == "5")
						{
							creditFlag = true;
						}


						if (ro == "")
						{
							dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]):N2}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"${0}.00", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = creditFlag ? new BaseColor(255, 255, 0) : new BaseColor(255, 255, 255) };
							table.AddCell(dataCell);
						}
						else
						{
							devTotal += Convert.ToDouble(Convert.ToDouble(ro));

							dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]):N2}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
							dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(ro):N2}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
							table.AddCell(dataCell);
						}
					}

					double subtotal = 0;
					double credito = 0;

					doc.Add(table);

					foreach (DataRow r in tickets.Rows)
					{
						query = $"select cod_frp from tblauxcaja where ref_doc = '{r[1]}'";
						if (conn.GetScalar(query) == "5")
						{
							query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5;";
							if (conn.GetRowQuery(query) == "")
								credito += double.Parse(r[4].ToString());
							else
								credito += double.Parse(r[4].ToString()) - double.Parse(conn.GetRowQuery(query));
						}
						subtotal += double.Parse(r[4].ToString());
					}

					doc.Add(new Paragraph(Environment.NewLine));

					PdfPTable colores = new PdfPTable(1)
					{
						WidthPercentage = 20,
						HorizontalAlignment = Element.ALIGN_RIGHT
					};
					colores.AddCell(new PdfPCell(new Phrase("Devolucion")) { BackgroundColor = new BaseColor(230, 133, 138) });
					colores.AddCell(new PdfPCell(new Phrase("Credito")) { BackgroundColor = new BaseColor(255, 255, 0) });
					doc.Add(colores);

					doc.Add(new Paragraph($"Subtotal: ${Convert.ToDouble(subtotal):N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
					doc.Add(new Paragraph($"Credito: ${credito:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
					doc.Add(new Paragraph($"Devoluciones: ${Convert.ToDouble(devTotal):N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
					doc.Add(new Paragraph($"Total a entregar: ${Convert.ToDouble(subtotal - devTotal - credito):N2}\n\n", titleFont) { Alignment = Element.ALIGN_RIGHT });



					doc.Close();
					writer.Close();
					Process.Start("reporte.pdf");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "La Bajadita - Venta de Frutas y Verduras");
			}
		}

		private async void FrmTickets_Load(object sender, EventArgs e)
		{
			cmbVendedor.Enabled = false;
			BtnAllTickets.Enabled = false;
			BtnPrintReport.Enabled = false;

			DataTable vendedores = new DataTable();

			await Task.Run(() => vendedores = conn.GetQuery("select cod_ven, nom_ven from tblvendedores where cod_ven!='BORR' " +
				"order by nom_ven asc;"));

			try
			{
				cmbVendedor.DataSource = vendedores;
				cmbVendedor.DisplayMember = "nom_ven";
				cmbVendedor.ValueMember = "cod_ven";
			}
			catch (Exception)
			{
				MessageBox.Show("No se pudieron cargar los vendedores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			cmbVendedor.Enabled = true;
			BtnAllTickets.Enabled = true;
			BtnPrintReport.Enabled = true;
		}

		private async void BtnAllTickets_Click(object sender, EventArgs e)
		{
			DataTable vendedores = new DataTable();
			string fechaA = FechaA.Value.ToString("yyyy-MM-dd");
			string fechaB = FechaB.Value.ToString("yyyy-MM-dd");
			string query = $@"  select ren.COD_VEN, v.NOM_VEN as Chofer
								from tblrenventas ren
								inner join tblvendedores v on v.COD_VEN=ren.cod_ven
								inner join tblgralventas gral on gral.REF_DOC=ren.REF_DOC
								where (gral.fec_doc between '{fechaA}' and '{fechaB}') and caja_doc=9
								group by ren.cod_ven;"; //Con este query obtengo los vendedores que se usaron en las notas del periodo ingresa por el usuario

			BtnAllTickets.Enabled = false;
			BtnPrintReport.Enabled = false;
			cmbVendedor.Enabled = false;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			pictureBox1.Visible = true;

			await Task.Run(async () =>
			{
				await Task.Run(() => vendedores = conn.GetQuery(query));


				Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
				DataTable tickets = new DataTable();

				string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fuentes", "mozilla.ttf");

				// Registrar y crear la fuente
				BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
				Font customFont = new Font(bf, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);

				//Inicializo en cero los contadores globales
				double sub = 0;
				double dev = 0;
				double cred = 0;
				double trans = 0;
				double totalDelDia = 0;

				try
				{
					Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
					
					string pdfPath = "reporte de tickets.pdf";

					using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
					{
						PdfWriter writer = PdfWriter.GetInstance(doc, fs);

						doc.AddCreator("La Bajadita - Venta de Frutas y Verduras");
						doc.AddAuthor("Bryan Allan Valdez Muñoz - Sistemas - La Bajadita");
						doc.AddTitle("Reporte de Tickets por Chofer");

						ClsHeaderTickets pageEventHelper = new ClsHeaderTickets("Imagenes/LOGO_EMPRESA-removebg-preview.png", "Ventas separadas por chofer", fechaA.Replace('-', '/'), fechaB.Replace('-', '/'));
						writer.PageEvent = pageEventHelper;
						doc.Open();

						//Con este for voy obteniendo los tickets de cada vendedor
						foreach (DataRow vendedor in vendedores.Rows)
						{
							doc.Add(new Paragraph($"Chofer: {vendedor[1]}", customFont) { Alignment = Element.ALIGN_CENTER });
							doc.Add(new Paragraph("\n") { Alignment = Element.ALIGN_CENTER });

							query = $@"select DISTINCT ven.fec_doc, ven.ref_doc, cli.NOM_CLI, hora_reg, round(tot_doc,2), ven.COD_USU, ren.cod_ven
												from tblgralventas ven
												inner join tblcatclientes cli on cli.COD_Cli=ven.COD_CLI
												inner join tblrenventas ren on ren.REF_DOC=ven.REF_DOC
												where CAJA_DOC=9 
												and (ven.FEC_DOC between '{fechaA}' and '{fechaB}')
												and ren.cod_ven='{vendedor[0]}';";
							//Query para obtener tickets de vendedores

							await Task.Run(() => tickets = conn.GetQuery(query));

							PdfPTable table = new PdfPTable(6);
							table.WidthPercentage = 100;

							float[] columnWidths = new float[] { 1f, 2f, 2f, 1f, 1f, 1f };
							table.SetWidths(columnWidths);

							Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
							PdfPCell headerCell;

							headerCell = new PdfPCell(new Phrase("FECHA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("FOLIO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("CLIENTE", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("HORA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("TOTAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);
							headerCell = new PdfPCell(new Phrase("DEV", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
							table.AddCell(headerCell);

							PdfPCell dataCell;
							Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

							query = "";
							double devTotal = 0;


							//Ahora recorro los tickets obtenidos, para saber si alguno tiene devolucion
							foreach (DataRow r in tickets.Rows)
							{
								//query para saber si hubo devolucion en el ticket
								query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5";

								string ro = conn.GetRowQuery(query);

								bool creditFlag = false;
								bool transferFlag = false;

								string frp = conn.GetRowQuery($"select cod_frp from tblauxcaja where ref_doc = '{r[1]}'");

								//Banderas para verificar si la nota es de credito o de transferencia
								if (frp == "5")
									creditFlag = true;
								if (frp == "9")
									transferFlag = true;

								//Si no hubo devoluciones, devuelve una cadena vacia
								if (ro == "")
								{
									BaseColor colorFondo = creditFlag ? new BaseColor(255, 255, 0) :
												transferFlag ? new BaseColor(173, 216, 230) : // Azul claro
												new BaseColor(255, 255, 255);

									dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"${0}.00", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = colorFondo };
									table.AddCell(dataCell);
								}
								else
								{
									devTotal += Convert.ToDouble(ro);

									dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
									dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(ro).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
									table.AddCell(dataCell);
								}
							}

							double subtotal = 0;
							double credito = 0;
							double transferencia = 0;
							//ahora recorremos la lista de vuelta para calcular el subtotal
							doc.Add(table);

							foreach (DataRow r in tickets.Rows)
							{
								query = $"select cod_frp from tblauxcaja where ref_doc = '{r[1]}'";
								if (conn.GetRowQuery(query) == "5")
								{
									query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5;";
									if (conn.GetRowQuery(query) == "")
										credito += double.Parse(r[4].ToString());
									else
										credito += double.Parse(r[4].ToString()) - double.Parse(conn.GetRowQuery(query));
								}
								if (conn.GetRowQuery(query) == "9")
								{
									query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5;";
									if (conn.GetRowQuery(query) == "")
										transferencia += double.Parse(r[4].ToString());
									else
										transferencia += double.Parse(r[4].ToString()) - double.Parse(conn.GetRowQuery(query));
								}
								subtotal += double.Parse(r[4].ToString());
								//subtotal += double.Parse(r[4].ToString());
							}


							doc.Add(new Paragraph($"Subtotal: ${subtotal:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
							doc.Add(new Paragraph($"Devoluciones: ${devTotal:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
							doc.Add(new Paragraph($"Credito: ${credito:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
							doc.Add(new Paragraph($"Transferencias: ${transferencia:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
							doc.Add(new Paragraph($"Total: ${(subtotal - devTotal - credito - transferencia).ToString("N2")}", titleFont) { Alignment = Element.ALIGN_RIGHT, });

							sub += subtotal;
							dev += devTotal;
							cred += credito;
							trans += transferencia;
							totalDelDia += subtotal - devTotal - credito - transferencia;
						}

						//Suma general de todos los montos

						// Crear una tabla con 2 columnas
						PdfPTable table1 = new PdfPTable(2)
						{
							WidthPercentage = 70 // Ancho de la tabla al 100%
						};

						titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
						titleFont.SetColor(255, 255, 255);

						doc.Add(new Paragraph(" "));

						//Tabla final de total del dia

						//Subtotal
						table1.AddCell(new PdfPCell(new Phrase("+SUBTOTAL DEL DIA:", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_RIGHT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(42, 82, 190)
						});
						table1.AddCell(new PdfPCell(new Phrase($"${sub:N2}", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_LEFT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(42, 82, 190)
						});

						//Devoluciones
						table1.AddCell(new PdfPCell(new Phrase("-DEVOLUCIONES DEL DIA:", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_RIGHT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(231, 76, 60)
						});
						table1.AddCell(new PdfPCell(new Phrase($"${dev:N2}", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_LEFT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(231, 76, 60)
						});

						//Credito
						table1.AddCell(new PdfPCell(new Phrase("-CREDITO DEL DIA:", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_RIGHT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(244, 208, 63)
						});
						table1.AddCell(new PdfPCell(new Phrase($"${cred:N2}", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_LEFT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(244, 208, 63)
						});

						//Transferencias
						table1.AddCell(new PdfPCell(new Phrase("-TRANSFERENCIAS DEL DIA:", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_RIGHT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(173, 216, 230)
						});
						table1.AddCell(new PdfPCell(new Phrase($"${trans:N2}", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_LEFT,
							Border = PdfPCell.NO_BORDER,
							BackgroundColor = new BaseColor(173, 216, 230)
						});

						//Venta del dia
						table1.AddCell(new PdfPCell(new Phrase("=VENTA TOTAL NETA DEL DIA:", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_RIGHT,
							Border = PdfPCell.TOP_BORDER,
							BackgroundColor = new BaseColor(39, 174, 96)
						});
						table1.AddCell(new PdfPCell(new Phrase($"${totalDelDia:N2}", titleFont))
						{
							HorizontalAlignment = Element.ALIGN_LEFT,
							Border = PdfPCell.TOP_BORDER,
							BackgroundColor = new BaseColor(39, 174, 96)
						});

						doc.Add(table1);

						doc.Close();
						writer.Close();
						Process.Start(pdfPath);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "La Bajadita - Venta de frutas y verduras");
				}
			});

			BtnAllTickets.Enabled = true;
			BtnPrintReport.Enabled = true;
			cmbVendedor.Enabled = true;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
			pictureBox1.Visible = false;
		}
	}
}
