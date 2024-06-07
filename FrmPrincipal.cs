using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmPrincipal : Form
	{
		public FrmPrincipal()
		{
			InitializeComponent();
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Imagenes", "LOGO_EMPRESA-removebg-preview.ico");

			this.Icon = new Icon(imagePath);
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
			MySqlConnection _con = new MySqlConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			try
			{
				_con.Open();
				MySqlCommand cmd = _con.CreateCommand();
				cmd.CommandText = "SELECT @@SQL_MODE;\r\nSET GLOBAL sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''));";
				cmd.ExecuteNonQuery();
				MessageBox.Show("Habilitacion completada, verifica el reporte nuevamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Application.Exit();
			}
			catch (MySqlException ex)
			{
				MessageBox.Show("Ocurrio un error al intentar conectarse a la base de datos. \n" + ex.Message);
			}
			finally
			{
				_con.Close();
			}
		}
	}
}
