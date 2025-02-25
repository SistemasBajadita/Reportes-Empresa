using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reportes
{
	public class ClsGenerarExcel
	{
		public DataTable ventaTienda;
		public DataTable ventaMayoreo;

		public ClsGenerarExcel(DataTable ventaTienda, DataTable ventaMayoreo)
		{
			this.ventaMayoreo = ventaMayoreo;
			this.ventaTienda = ventaTienda;
		}

		public void GenerarReporte()
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

				List<DateTime> rango = new List<DateTime>(); 

				for (int i = 0; i < ventaTienda.Rows.Count / 2; i++)
				{
					Cell valor = hoja.Cells[0, i + 1];
					hoja.Cells[0, i + 1].Value = ventaTienda.Rows[i][0].ToString();
				}

				//Creamos el formato para que se pueda visualizar numericamente en excel los valores, y poder hacer calculos con ellos
				CellsFactory f = new CellsFactory();
				Style estilo = f.CreateStyle();
				estilo.Number = 4;

				//Ahora inserto los registros donde corresponden
				//efectivo
				for (int i = 0; i < ventaTienda.Rows.Count / 2; i++)
				{
					Cell valor = hoja.Cells[1, i + 1];
					valor.PutValue(double.Parse(ventaTienda.Rows[i][1].ToString()));
					valor.SetStyle(estilo);
				}
				//terminal
				for (int i = ventaTienda.Rows.Count / 2; i < ventaTienda.Rows.Count; i++)
				{
					Cell valor = hoja.Cells[2, i + 1 - ventaTienda.Rows.Count / 2];
					valor.PutValue(double.Parse(ventaTienda.Rows[i][1].ToString()));
					valor.SetStyle(estilo);
				}

				//Ahora, insertamos los registros de la venta de mayoreo

				hoja.AutoFitColumns();
				excel.Save("venta.xlsx", SaveFormat.Xlsx);
				Process.Start("venta.xlsx");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


	}
}
