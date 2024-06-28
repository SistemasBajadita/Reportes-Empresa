using iTextSharp.text.pdf.codec.wmf;
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
	public partial class FrmVentaDetallada : Form
	{
		ClsConnection metodos;
		public FrmVentaDetallada()
		{
			InitializeComponent();
		}

		private async void FrmVentaDetallada_Load(object sender, EventArgs e)
		{
			cbDepartamentos.Enabled = false;
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			await Task.Run(() =>
			{
				DataTable departamentos = metodos.GetQuery("select cod_agr as Codigo, des_agr as Agrupacion " +
					"from tblcatagrupacionart agr inner join tblagrupacionart gpo on gpo.cod_gpo=agr.COD_GPO " +
					"where agr.cod_gpo=25");
				Invoke(new Action(() =>
				{
					cbDepartamentos.DataSource = departamentos;
					cbDepartamentos.ValueMember = "Codigo";
					cbDepartamentos.DisplayMember = "Agrupacion";
				}));

			});
			metodos = null;
			cbDepartamentos.Enabled = true;
		}
	}
}
