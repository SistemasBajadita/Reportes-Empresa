using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Reportes
{
	public class ClsGenerarExcel
	{
		public DataTable dataTable;
		public DataTable tickets;

		//Constructor para compras
		public ClsGenerarExcel(DataTable ventaGeneral)
		{
			dataTable = ventaGeneral;
		}

		public ClsGenerarExcel(DataTable ventaGeneral, DataTable Tickets)
		{
			dataTable = ventaGeneral;
			tickets = Tickets;
		}

		public void GenerarReporteDeCompras()
		{
			DateTime startDate = dataTable.AsEnumerable().Min(row => row.Field<DateTime>("fec_fac"));
			DateTime endDate = dataTable.AsEnumerable().Max(row => row.Field<DateTime>("fec_fac"));

			// Diccionario para almacenar todas las fechas en el rango con valores en 0
			Dictionary<DateTime, decimal> completeData = new Dictionary<DateTime, decimal>();

			for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
			{
				completeData[date] = 0; // Inicializar con 0
			}

			// Llenar con los valores existentes del DataTable
			foreach (DataRow row in dataTable.Rows)
			{
				DateTime fecha = row.Field<DateTime>("fec_fac");
				decimal total = row.Field<decimal>("total");
				completeData[fecha] = total;
			}

			// Crear el archivo Excel con Aspose.Cells
			Workbook workbook = new Workbook();
			Worksheet sheet = workbook.Worksheets[0];

			// Escribir encabezados
			sheet.Cells[1, 0].PutValue("Compra");

			CellsFactory f = new CellsFactory();
			Style estilo = f.CreateStyle();
			estilo.Number = 4;

			// Escribir datos en el archivo Excel
			int column = 1;
			foreach (var entry in completeData)
			{
				Cell valor = sheet.Cells[1, column];
				sheet.Cells[0, column].PutValue(entry.Key.ToString("yyyy/MM/dd"));
				valor.PutValue(entry.Value);
				valor.SetStyle(estilo);
				column++;
			}
			sheet.AutoFitColumns();

			// Guardar el archivo Excel
			string filePath = "compras.xlsx";
			workbook.Save(filePath, SaveFormat.Xlsx);
			Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

		}

		public void GenerarReporteDeVenta(bool grafica)
		{
			try
			{
				Workbook excel = new Workbook();
				excel.Worksheets.Clear();
				Worksheet hoja = excel.Worksheets.Add("venta");

				//Ingreso los datos de la hoja, por defecto, que son los conceptos y las fechas
				hoja.Cells[1, 0].Value = "EFECTIVO";
				hoja.Cells[2, 0].Value = "TERMINAL";
				hoja.Cells[3, 0].Value = "MAYOREO";
				hoja.Cells[5, 0].Value = "TicketsTienda";
				hoja.Cells[6, 0].Value = "PromedioTienda";
				hoja.Cells[7, 0].Value = "TicketsMayoreo";
				hoja.Cells[8, 0].Value = "PromedioMayoreo";


				//Creamos el formato para que se pueda visualizar numericamente en excel los valores, y poder hacer calculos con ellos
				CellsFactory f = new CellsFactory();
				Style estilo = f.CreateStyle();
				estilo.Number = 4;

				Cell valor;

				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					estilo.Number = 4;

					//Fecha
					hoja.Cells[0, i + 1].Value = dataTable.Rows[i]["Fecha"].ToString().Split(' ')[0];

					//Efectivo
					valor = hoja.Cells[1, i + 1];
					valor.PutValue(double.Parse(dataTable.Rows[i]["Efectivo"].ToString()));
					valor.SetStyle(estilo);

					//Terminal
					valor = hoja.Cells[2, i + 1];
					valor.PutValue(double.Parse(dataTable.Rows[i]["Terminal"].ToString()));
					valor.SetStyle(estilo);

					//Mayoreo
					valor = hoja.Cells[3, i + 1];
					valor.PutValue(double.Parse(dataTable.Rows[i]["Mayoreo"].ToString()));
					valor.SetStyle(estilo);

					

					//PromedioTienda
					valor = hoja.Cells[6, i + 1];
					valor.PutValue(double.Parse(tickets.Rows[i]["PromedioTienda"].ToString()));
					valor.SetStyle(estilo);

					//PromedioMayoreo
					valor = hoja.Cells[8, i + 1];
					valor.PutValue(double.Parse(tickets.Rows[i]["PromedioMayoreo"].ToString()));
					valor.SetStyle(estilo);

					estilo.Number = 1;

					//TicketsTienda
					valor = hoja.Cells[5, i + 1];
					valor.PutValue(double.Parse(tickets.Rows[i]["TicketsTienda"].ToString()));
					valor.SetStyle(estilo);

					//TicketsTienda
					valor = hoja.Cells[7, i + 1];
					valor.PutValue(double.Parse(tickets.Rows[i]["TicketsMayoreo"].ToString()));
					valor.SetStyle(estilo);

					
				}

				if (grafica)
				{
					// Crear gráfico de líneas
					int chartIndex = hoja.Charts.Add(ChartType.Line, 5, 0, 25, 10);
					Chart grafico = hoja.Charts[chartIndex];

					// Configurar series de datos por FILAS en lugar de COLUMNAS
					grafico.NSeries.Add("B2:" + Convert.ToChar('A' + dataTable.Rows.Count) + "2", false); // Efectivo
					grafico.NSeries.Add("B3:" + Convert.ToChar('A' + dataTable.Rows.Count) + "3", false); // Terminal
					grafico.NSeries.Add("B4:" + Convert.ToChar('A' + dataTable.Rows.Count) + "4", false); // Mayoreo

					// Configurar etiquetas del eje X (Fechas)
					grafico.NSeries.CategoryData = "B1:" + Convert.ToChar('A' + dataTable.Rows.Count) + "1";

					// Nombres de las series
					grafico.NSeries[0].Name = "Efectivo";
					grafico.NSeries[1].Name = "Terminal";
					grafico.NSeries[2].Name = "Mayoreo";

					// Configurar título y otros aspectos del gráfico
					grafico.Title.Text = "Ventas por Día";
					grafico.ValueAxis.Title.Text = "Monto en Pesos";
					grafico.CategoryAxis.Title.Text = "Fechas";
				}

				// Ajustar tamaño de columnas automáticamente
				hoja.AutoFitColumns();
				string archivo = "venta.xlsx";
				excel.Save(archivo, SaveFormat.Xlsx);
				Process.Start(archivo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
