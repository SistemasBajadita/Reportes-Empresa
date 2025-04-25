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
	public partial class FrmSelectProv : Form
	{
		ClsConnection conn;
		public Action<string> sendId;

		public FrmSelectProv()
		{
			InitializeComponent();
			conn = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private async void FrmSelectProv_Load(object sender, EventArgs e)
		{
			DataTable proveedores = new DataTable();
			await Task.Run(() =>
			{
				proveedores = conn.GetQuery("select cod_prov as Codigo, nom_prov as Proveedor from tblcatproveedor;");
			});

			reporte.DataSource = proveedores;
		}

		private void reporte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			string id = reporte.Rows[reporte.SelectedRows[0].Index].Cells[0].Value.ToString();
			sendId?.Invoke(id);
			Close();
		}

		private async void TxtFiltro_TextChanged(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			string filt = TxtFiltro.Text;
			await Task.Run(() => dt = conn.GetQuery($"select cod_prov as Codigo, nom_prov as Proveedor from tblcatproveedor where cod_prov like '%{filt}%' or nom_prov like '%{filt}%';"));
			reporte.DataSource = dt;
		}

		private void BtnSeleccionar_Click(object sender, EventArgs e)
		{
			string id = reporte.Rows[reporte.SelectedRows[0].Index].Cells[0].Value.ToString();
			sendId?.Invoke(id);
			Close();
		}
	}
}
