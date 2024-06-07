using Aspose.Cells;
using Aspose.Cells.Tables;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

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
			}
			catch (MySqlException ex)
			{
				MessageBox.Show($"Ocurrio un error\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				cmd.CommandTimeout = 120;
				cmd.ExecuteNonQuery();

				MySqlDataAdapter ad = new MySqlDataAdapter(cmd);

				ad.Fill(reporte);
				sendReport(reporte);
			}
			catch (MySqlException ex)
			{
				MessageBox.Show($"Ocurrio un error\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

					string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Imagenes", "LOGO_EMPRESA-removebg-preview.png");


					ClsPageEventHelper pageEventHelper = new ClsPageEventHelper(imagePath);
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


		public void PrintReportInPDFVentas(string fechaA, string fechaB, string path)
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