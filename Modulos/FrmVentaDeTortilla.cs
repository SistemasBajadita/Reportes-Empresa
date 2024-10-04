using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Reportes
{
	public partial class FrmVentaDeTortilla : Form
	{
		public FrmVentaDeTortilla()
		{
			InitializeComponent();
		}

		private void FrmVentaDeTortilla_Paint(object sender, PaintEventArgs e)
		{
			System.Drawing.Rectangle rect = this.ClientRectangle;

			Color color1 = Color.FromArgb(251, 147, 60); //--original
			Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original

			// Crear un pincel con un degradado lineal
			using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.ForwardDiagonal))
			{
				e.Graphics.FillRectangle(brush, rect);
			}
		}

		private void FrmVentaDeTortilla_Load(object sender, System.EventArgs e)
		{
			for (int i = 2024; i <= DateTime.Now.Year; i++)
			{
				cmbAnio.Items.Add(i);
			}
		}

		private async void BtnReport_Click(object sender, EventArgs e)
		{
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);
			if (cmbMes.Text != "" && cmbAnio.Text != "")
			{
				string numTicketsGeneralStr = "", numTicketsSinTortillaStr = "";
				string mes = (cmbMes.SelectedIndex + 1).ToString();
				string anio = cmbAnio.SelectedItem.ToString();

				BtnPrint.Enabled = false;
				BtnReport.Enabled = false;

				// Obtener los resultados de la base de datos
				await Task.Run(() =>
				{
					numTicketsGeneralStr = con.GetScalar($"select count(*) from tblgralventas where month(fec_doc)={mes} and year(fec_doc)={anio};");
					numTicketsSinTortillaStr = con.GetScalar($"SELECT COUNT(DISTINCT g.REF_DOC) " +
						$"FROM tblgralventas g " +
						$"LEFT JOIN tblrenventas r ON g.REF_DOC = r.REF_DOC AND r.COD1_ART IN ('565', '565-1', '565-2') " +
						$"WHERE r.REF_DOC IS NULL " +
						$"AND MONTH(g.FEC_DOC) = {mes} and YEAR(g.FEC_DOC)={anio};");
				});

				BtnPrint.Enabled = true;
				BtnReport.Enabled = true;

				// Convertir a números
				int numTicketsGeneral = int.Parse(numTicketsGeneralStr);
				int numTicketsSinTortilla = int.Parse(numTicketsSinTortillaStr);

				// Limpiar series y áreas de gráfico anteriores si ya existen
				chart1.Series.Clear();
				chart1.ChartAreas.Clear();
				chart1.Titles.Clear();

				// Crear un área de gráfico
				ChartArea chartArea = new ChartArea();

				// Ajustar los ejes
				chartArea.AxisX.Interval = 2;
				chartArea.AxisY.Title = "Número de Tickets"; // Título del eje Y
				chartArea.Area3DStyle.Enable3D = true; // Activar 3D
				chartArea.Area3DStyle.Inclination = 15; // Inclinación del gráfico
				chartArea.Area3DStyle.Rotation = 10;    // Rotación del gráfico en 3D
				chartArea.Area3DStyle.WallWidth = 0;

				string selectedDate = new DateTime(int.Parse(cmbAnio.Text), cmbMes.SelectedIndex + 1, 1).ToString("MMMM yyyy");

				// Ajustar el título del eje X para mostrar la fecha
				chartArea.AxisX.Title = selectedDate.ToUpper(); // Mostrar la fecha en el eje X
				chartArea.AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold); // Estilo del título
				chartArea.AxisX.TitleAlignment = StringAlignment.Center;

				chart1.ChartAreas.Add(chartArea);

				// Crear una serie
				Series series1 = new Series("Tickets del mes");
				series1.ChartType = SeriesChartType.Column; // Gráfico de barras verticales
				series1.Points.Add(numTicketsGeneral);

				// Mostrar los valores en las barras
				series1.IsValueShownAsLabel = true;

				Series series2 = new Series("Tickets sin tortilla");
				series2.ChartType = SeriesChartType.Column; // Gráfico de barras verticales
				series2.Points.Add(numTicketsSinTortilla);

				// Mostrar los valores en las barras
				series2.IsValueShownAsLabel = true;

				// Añadir la serie al gráfico
				chart1.Series.Add(series1);
				chart1.Series.Add(series2);

				// Añadir un título al gráfico
				Title title = new Title();
				title.Text = "Reporte tickets sin tortilla"; // Texto del título
				title.Font = new Font("Arial", 16, FontStyle.Bold); // Puedes cambiar el estilo de la fuente si quieres
				chart1.Titles.Add(title);

				TextAnnotation annotation = new TextAnnotation
				{
					Text = $"Fecha de impresión: {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")}",
					ForeColor = Color.Black,
					Font = new Font("Arial", 10, FontStyle.Regular),
					IsSizeAlwaysRelative = false,
					AnchorDataPoint = null
				};

				// Configurar la posición de la anotación
				annotation.X = 50;
				annotation.Y = 94;
				annotation.Alignment = ContentAlignment.MiddleLeft;

				// Añadir la anotación al gráfico
				chart1.Annotations.Add(annotation);
			}
			else
			{
				MessageBox.Show("Selecciona el mes y el año por favor");
			}
		}

		private void BtnPrint_Click(object sender, EventArgs e)
		{
			if (chart1.Series.Count == 0)
			{
				MessageBox.Show("Primero presiona ver reporte");
				return;
			}
			chart1.SaveImage("image.png", ChartImageFormat.Png);
			Process.Start("image.png");
		}
	}
}
