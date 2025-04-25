using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos.Contrarecibo
{
	public partial class FrmConsultarContrarecibo : Form
	{
		ClsContrareciboOperaciones cr;

		public FrmConsultarContrarecibo()
		{
			InitializeComponent();
			cr = new ClsContrareciboOperaciones(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private async void FrmConsultarContrarecibo_Load(object sender, EventArgs e)
		{
			label1.Text = "Cargando...";
			reporte.DataSource = await cr.ObtenerContrarecibos(dateTimePicker1.Value, dateTimePicker2.Value);
			label1.Text = "";
		}

		private async void CambioDateTime(object sender, EventArgs e)
		{
			label1.Text = "Cargando...";
			reporte.DataSource = await cr.ObtenerContrarecibos(dateTimePicker1.Value, dateTimePicker2.Value);
			label1.Text = "";
		}

		private async void Descargar_PDF(object sender, EventArgs e)
		{
			string idContrarecibo = reporte.Rows[reporte.SelectedRows[0].Index].Cells[0].Value.ToString();
			string idProveedor = reporte.Rows[reporte.SelectedRows[0].Index].Cells[1].Value.ToString();
			string fecha = Convert.ToDateTime(reporte.Rows[reporte.SelectedRows[0].Index].Cells[3].Value.ToString()).ToString("yyyy-MM-dd");

			await cr.GenerarPdfContrarecibo(idContrarecibo, idProveedor, fecha);
		}

		private async void TxtFiltro_TextChanged(object sender, EventArgs e)
		{
			reporte.DataSource = await cr.ObtenerContrarecibos(dateTimePicker1.Value, dateTimePicker2.Value, TxtFiltro.Text);
		}
	}
}
