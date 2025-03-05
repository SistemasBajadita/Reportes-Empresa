using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reportes
{
	public class ClsGenerarExcel
	{
		public DataTable ventaGeneral;
		public DataTable ventaMayoreo;

		public ClsGenerarExcel(DataTable ventaGeneral)
		{
			this.ventaGeneral = ventaGeneral;
		}

		public void GenerarReporte(bool grafica)
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

				//Creamos el formato para que se pueda visualizar numericamente en excel los valores, y poder hacer calculos con ellos
				CellsFactory f = new CellsFactory();
				Style estilo = f.CreateStyle();
				estilo.Number = 4;

				Cell valor;

				for (int i = 0; i < ventaGeneral.Rows.Count; i++)
				{
					//Fecha
					hoja.Cells[0, i + 1].Value = ventaGeneral.Rows[i]["Fecha"].ToString().Split(' ')[0];

					//Efectivo
					valor = hoja.Cells[1, i + 1];
					valor.PutValue(double.Parse(ventaGeneral.Rows[i]["Efectivo"].ToString()));
					valor.SetStyle(estilo);

					//Terminal
					valor = hoja.Cells[2, i + 1];
					valor.PutValue(double.Parse(ventaGeneral.Rows[i]["Terminal"].ToString()));
					valor.SetStyle(estilo);

					//Mayoreo
					valor = hoja.Cells[3, i + 1];
					valor.PutValue(double.Parse(ventaGeneral.Rows[i]["Mayoreo"].ToString()));
					valor.SetStyle(estilo);
				}

				if (grafica)
				{
					// Crear gráfico de líneas
					int chartIndex = hoja.Charts.Add(ChartType.Line, 5, 0, 25, 10);
					Chart grafico = hoja.Charts[chartIndex];

					// Configurar series de datos por FILAS en lugar de COLUMNAS
					grafico.NSeries.Add("B2:" + Convert.ToChar('A' + ventaGeneral.Rows.Count) + "2", false); // Efectivo
					grafico.NSeries.Add("B3:" + Convert.ToChar('A' + ventaGeneral.Rows.Count) + "3", false); // Terminal
					grafico.NSeries.Add("B4:" + Convert.ToChar('A' + ventaGeneral.Rows.Count) + "4", false); // Mayoreo

					// Configurar etiquetas del eje X (Fechas)
					grafico.NSeries.CategoryData = "B1:" + Convert.ToChar('A' + ventaGeneral.Rows.Count) + "1";

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
