using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Reportes
{
	public partial class FrmVentaDeTortilla : Form
	{
		ClsConnection con;

		public FrmVentaDeTortilla()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void FrmVentaDeTortilla_Paint(object sender, PaintEventArgs e)
		{

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
			if (Program.Empresa == 0)
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);
			else if (Program.Empresa == 1)
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ConnectionString);
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
				graphic.Series.Clear();
				graphic.ChartAreas.Clear();
				graphic.Titles.Clear();

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

				graphic.ChartAreas.Add(chartArea);

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
				graphic.Series.Add(series1);
				graphic.Series.Add(series2);

				// Añadir un título al gráfico
				Title title = new Title();
				title.Text = $"Reporte tickets sin tortilla -  Sucursal: {(Program.Empresa == 0 ? "Jardines del Bosque" : "Colinas del Yaqui")} "; // Texto del título
				title.Font = new Font("Arial", 16, FontStyle.Bold); // Puedes cambiar el estilo de la fuente si quieres
				graphic.Titles.Add(title);

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
				graphic.Annotations.Add(annotation);
			}
			else
			{
				MessageBox.Show("Selecciona el mes y el año por favor");
			}
		}

		private void BtnPrint_Click(object sender, EventArgs e)
		{
			if (graphic.Series.Count == 0)
			{
				MessageBox.Show("Primero presiona ver reporte");
				return;
			}
			graphic.SaveImage("image.png", ChartImageFormat.Png);
			Process.Start("image.png");
		}
	}
}
