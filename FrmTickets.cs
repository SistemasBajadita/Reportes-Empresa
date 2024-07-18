using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
		}

		private string GetSelectedTextFromCombo()
		{
			if (cmbVendedor.SelectedItem != null)
			{
				DataRowView selectedRow = cmbVendedor.SelectedItem as DataRowView;

				if (selectedRow != null)
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
			string fechaA = dateTimePicker3.Value.ToString("yyyy-MM-dd");
			string fechaB = dateTimePicker3.Value.ToString("yyyy-MM-dd");

			await Task.Run(() => tickets = conn.GetQuery($@"select DISTINCT ven.fec_doc, ven.ref_doc, cli.NOM_CLI, hora_reg, concat('$',round(tot_doc,2)), ven.COD_USU, ren.cod_ven
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
					doc.Add(new Paragraph($"REPORTE DE TICKETS POR CHOFER\nCHOFER: {GetSelectedTextFromCombo()}\nPERIODO: {dateTimePicker3.Value.ToString("dd/MM/yyyy")} a {dateTimePicker4.Value.ToString("dd/MM/yyyy")}"));
					doc.Add(new Paragraph("\n"));

					PdfPTable table = new PdfPTable(5);
					table.WidthPercentage = 100;

					float[] columnWidths = new float[] { 1f, 2f, 2f, 1f, 1f };
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

					PdfPCell dataCell;
					Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

					foreach (DataRow r in tickets.Rows)
					{
						dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]).ToString("dd/MM/yyyy")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase($"{r[4]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
						table.AddCell(dataCell);
					}

					double total = 0;

					doc.Add(table);

					foreach (DataRow r in tickets.Rows)
					{
						total += double.Parse(r[4].ToString().Substring(1));
					}


					doc.Add(new Paragraph($"Total: ${total}", titleFont) { Alignment = Element.ALIGN_RIGHT });

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

			DataTable vendedores = new DataTable();

			await Task.Run(() => vendedores = conn.GetQuery("select cod_ven, nom_ven from tblvendedores;"));

			cmbVendedor.DataSource = vendedores;
			cmbVendedor.DisplayMember = "nom_ven";
			cmbVendedor.ValueMember = "cod_ven";


			cmbVendedor.Enabled = true;
		}
	}
}
