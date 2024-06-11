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

namespace Reportes
{
	public partial class FrmConteo : Form
	{
		ClsConnection metodos;
		public FrmConteo()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		public async void SetOptionInCombo()
		{
			cbOP.DataSource = null;
			cbOP.Items.Clear();

			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());

			DataTable opciones = null;

			if (rbCategoria.Checked)
			{
				await Task.Run(() => opciones = metodos.GetQuery("select cod_agr, des_agr from tblcatagrupacionart where cod_gpo=26 order by des_agr asc"));
			}
			else
			{
				await Task.Run(() => opciones = metodos.GetQuery("select des_agr from tblcatagrupacionart where cod_gpo=25 order by des_agr asc"));
			}

			cbOP.DataSource = opciones;
			cbOP.DisplayMember = "des_agr";
			cbOP.ValueMember = "cod_agr";
			metodos = null;

		}

		public void ChangeRadios(object sender, EventArgs e)
		{
			SetOptionInCombo();
		}

		private void getReport(DataTable data)
		{
			MessageBox.Show("Hoja generada exitosamente");
		}

		private async void BtnPDF_Click(object sender, EventArgs e)
		{
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			metodos.sendReport = getReport;

			string query = $"select art.cod1_art as Codigo, des1_art as Descripcion, c.Cos_Pro as Costo, EXI_ACT as Existencia " +
				$"from tblcatarticulos art " +
				$"inner join tblundcospreart c on c.COD1_ART=art.COD1_ART " +
				$"inner join tblgpoarticulos g on g.COD1_ART=art.COD1_ART " +
				$"where COD_AGR={cbOP.SelectedValue};";

			await Task.Run(()=> metodos.SetQuery(query));




		}
	}
}
