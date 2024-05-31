using Aspose.Cells;
using Aspose.Cells.Tables;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reportes
{
	public class ClsConnection
	{
		public Action<DataTable> sendReport;
		readonly string con;
		private DataTable ReporteActivo;

		public ClsConnection(string con)
		{
			this.con = con;
		}

		public void ReportVentasCosto(string fechaA, string fechaB)
		{
			DataTable reporte = new DataTable();

			try
			{
				MySqlConnection con = new MySqlConnection(this.con);
				con.Open();

				string query = "select caa.DES_AGR as Departamento, round(sum(rv.CAN_ART * rv.PCIO_VEN),2) as 'Venta Total', round(sum(rv.CAN_ART * rv.COS_VEN),2) as Costo, round((1 - (sum(rv.CAN_ART * rv.COS_VEN) / sum(rv.CAN_ART * rv.PCIO_VEN))) * 100, 2) as Porc from tblgralventas gv " +
					"inner join tblrenventas rv on gv.REF_DOC = rv.REF_DOC " +
					"inner join tblgpoarticulos ga on rv.COD1_ART = ga.COD1_ART " +
					"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where  (gv.FEC_DOC between '{fechaA}' and '{fechaB}') and ga.COD_GPO = 25 " +
					$"GROUP BY caa.DES_AGR";

				MySqlDataAdapter ad = new MySqlDataAdapter(query, con);

				ad.Fill(reporte);
				sendReport(reporte);
			}
			catch (MySqlException ex)
			{
				MessageBox.Show($"Ocurrio un error\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			ReporteActivo = reporte;
		}

		public void PrintReportInPDF(string fechaA, string fechaB, string path)
		{
			try
			{
				Document doc = new Document();
				string pdfPath = path;

				using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					PdfWriter writer = PdfWriter.GetInstance(doc, fs);

					// Añadir el evento de página para el encabezado con imagen
					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("C:/Users/HUAWEI/Desktop/Empresa/Reportes/Imagenes/LOGO_EMPRESA-removebg-preview.png");
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
					PdfPTable table = new PdfPTable(4);
					table.WidthPercentage = 100;

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

					// Añadir datos
					iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
					PdfPCell dataCell;

					double totalVenta = 0;
					double totalCosto = 0;

					foreach (DataRow row in ReporteActivo.Rows)
					{
						string departamento = row["Departamento"].ToString();
						string venta = "$" + double.Parse(row["Venta Total"].ToString()).ToString("N2");
						string costo = "$" + double.Parse(row["Costo"].ToString()).ToString("N2");
						string utilidad = row["Porc"].ToString() + "%";

						dataCell = new PdfPCell(new Phrase(departamento, dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(venta, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(costo, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);
						dataCell = new PdfPCell(new Phrase(utilidad, dataFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f };
						table.AddCell(dataCell);

						totalVenta += double.Parse(row["Venta Total"].ToString());
						totalCosto += double.Parse(row["Costo"].ToString());
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

					Paragraph date = new Paragraph($"Fecha: {DateTime.Now}");
					date.Alignment = Element.ALIGN_LEFT;

					doc.Add(table);
					doc.Add(new Paragraph("\n"));
					doc.Add(date);
					doc.Close();
					writer.Close();
				}
				MessageBox.Show("Reporte PDF generado exitosamente, por favor espera y se abrirá el archivo automáticamente después de que cierres este mensaje.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrió un error al generar el PDF\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				MessageBox.Show("Reporte Excel generado exitosamente, por favor espera y se abrira el archivo automaticamente despues de que cierres este mensaje.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (IOException)
			{
				MessageBox.Show("Ocurrio un error al guardar el archivo. " +
					"Si tienes abierto un archivo con el mismo nombre que " +
					"quieres asignar por favor cierralo y vuelve a intentarlo.", "Error al intentar guardar el archivo",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
//Programado por Bryan Valdez Muñoz
//6312988689
//alan272_@hotmail.com