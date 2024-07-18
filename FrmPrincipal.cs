using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmPrincipal : Form
	{
		public FrmPrincipal()
		{
			InitializeComponent();
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Imagenes", "LOGO_EMPRESA-removebg-preview.ico");

			this.Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void BtnVentaCosto_Click(object sender, EventArgs e)
		{
			FrmVentaCosto frm = new FrmVentaCosto();
			frm.ShowDialog();
		}

		private void BtnCompras_Click(object sender, EventArgs e)
		{
			FrmComprasPD frm = new FrmComprasPD();	
			frm.ShowDialog();
		}

		private void BtnTopProductos_Click(object sender, EventArgs e)
		{
			FrmTopProductos frm = new FrmTopProductos();
			frm.ShowDialog();
		}

		private void BtnActiveReport_Click(object sender, EventArgs e)
		{
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			con.SetOnReportsCortes();
			con = null;
		}

		private void BtnCountProducts_Click(object sender, EventArgs e)
		{
			FrmConteo frm = new FrmConteo();
			frm.ShowDialog();
		}

		private void BtnMovAlm_Click(object sender, EventArgs e)
		{
			FrmMovimientos frm = new FrmMovimientos();
			frm.ShowDialog();
		}

		private void BtnVentasDetalladas_Click(object sender, EventArgs e)
		{
			FrmVentaDetallada frm = new FrmVentaDetallada();
			frm.ShowDialog();
		}

		private void BtnTicketChofer_Click(object sender, EventArgs e)
		{
			FrmTickets frm = new FrmTickets();
			frm.ShowDialog();
		}

		private void FrmPrincipal_Load(object sender, EventArgs e)
		{
		}
	}
}
