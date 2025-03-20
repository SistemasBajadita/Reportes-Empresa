using Aspose.Cells;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos
{
	public partial class FrmYearSelection : Form
	{
		public FrmYearSelection()
		{
			InitializeComponent();
		}

		private void FrmYearSelection_Load(object sender, EventArgs e)
		{
			for (int i = 2024; i <= DateTime.Now.Year; i++)
			{
				cbYears.Items.Add(i);
			}
			pictureBox1.Image = Image.FromFile("Imagenes/load.gif");
			label1.Visible = false;
		}

		static string GetMonthName(int monthNumber)
		{
			if (monthNumber < 1 || monthNumber > 12)
				throw new ArgumentOutOfRangeException("monthNumber", "El número del mes debe estar entre 1 y 12.");

			string[] months = {
				"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
				"Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
			};
			return months[monthNumber - 1];
		}

		private void SetExcel()
		{
			int anio = 0;
			Invoke(new Action(() =>
			{
				anio = int.Parse(cbYears.Text);
			}));
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings[anio == 2024 ? "antes":"empresa"].ToString());
			DataTable departamentos = con.GetQuery("select DISTINCT agr.cod_agr, des_agr from tblcatagrupacionart agr " +
				"inner join tblgpoarticulos cod on cod.COD_AGR=agr.COD_AGR where cod.COD_GPO=25 " +
				"order by des_agr asc;");
			Workbook excel = new Workbook();

			WorksheetCollection sheets = excel.Worksheets;

			sheets.RemoveAt(0);

			for (int i = 0; i < departamentos.Rows.Count; i++)
			{
				Worksheet hojaActual = excel.Worksheets.Add(departamentos.Rows[i][1].ToString());


				int meses = 0;

				if (anio == DateTime.Now.Year)
					meses = DateTime.Now.Month;
				else
					meses = 12;

				hojaActual.Cells[0, 0].Value = "Meses";
				hojaActual.Cells[0, 1].Value = "Venta";
				hojaActual.Cells[0, 2].Value = "Costo";

				for (int j = 1; j <= meses; j++)
				{
					string[] resultado = con.HandleProcedureVentasMensuales(int.Parse(departamentos.Rows[i][0].ToString()), anio, j);
					hojaActual.Cells[j, 0].Value = GetMonthName(j);

					Cell cell = hojaActual.Cells[j, 1];

					// Asignar un valor numérico a la celda
					cell.Value = double.Parse(resultado != null ? resultado[0] : "0.00");
					Style style = cell.GetStyle();
					style.Number = 5; // Formato de número general
					cell.SetStyle(style);

					cell = hojaActual.Cells[j, 2];
					cell.Value = double.Parse(resultado != null ? resultado[1] : "0.00");
					style = cell.GetStyle();
					style.Number = 5; // Formato de número general
					cell.SetStyle(style);

				}

				int chartIndex = hojaActual.Charts.Add(Aspose.Cells.Charts.ChartType.Column3D, meses + 2, 0, meses + 25, 10);
				Aspose.Cells.Charts.Chart chart = hojaActual.Charts[chartIndex];

				string rangeVentas = $"B2:B{meses + 1}";
				string rangeCosto = $"C2:C{meses + 1}";
				chart.NSeries.Add(rangeVentas, true);
				chart.NSeries.Add(rangeCosto, true);
				chart.NSeries.CategoryData = $"A2:A{meses + 1}";

				chart.Title.Text = "Ventas y Costos Mensuales - " + departamentos.Rows[i][1].ToString();
				chart.ValueAxis.Title.Text = "Valores";
				chart.CategoryAxis.Title.Text = "Meses";
				chart.Style = i + 1;

				chart.NSeries[0].Name = "Ventas";
				chart.NSeries[1].Name = "Costo";

				Random color = new Random();

				chart.PlotArea.Area.ForegroundColor = Color.White;
				chart.PlotArea.Border.IsVisible = false;

				double porcentaje = ((double)(i + 1) / (double)departamentos.Rows.Count) * 100.0;

				Invoke(new Action(() => { label1.Text = $"{porcentaje:N2}%"; }));

				hojaActual.AutoFitColumns();
			}



			Worksheet evalSheet = sheets["Evaluation Warning"];
			if (evalSheet != null)
			{
				sheets.RemoveAt(evalSheet.Index);
			}

			excel.Save("ventas mensuales.xlsx");
			Process.Start("ventas mensuales.xlsx");
		}

		private async void BtnGetExcel_Click(object sender, EventArgs e)
		{
			pictureBox1.Visible = true;
			label1.Visible = true;
			await Task.Run(SetExcel);
			pictureBox1.Visible = false;
			label1.Visible = false;
			Close();
		}
	}
}
