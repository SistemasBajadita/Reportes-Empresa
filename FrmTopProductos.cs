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
	public partial class FrmTopProductos : Form
	{

		ClsConnection metodos;

		public FrmTopProductos()
		{
			InitializeComponent();
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());

			DataTable departamentos = metodos.GetQuery("select cod_agr as Codigo, des_agr as Agrupacion\r\nfrom tblcatagrupacionart agr inner join tblagrupacionart gpo on gpo.cod_gpo=agr.COD_GPO\r\nwhere agr.cod_gpo=25");
			cbDepartamentos.DataSource = departamentos;
			cbDepartamentos.ValueMember = "Codigo";
			cbDepartamentos.DisplayMember = "Agrupacion";

			metodos = null;
		}

		private void SetearQuery(DataTable quer)
		{
			Invoke(new Action(() => { reporte.DataSource = quer; }));
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString())
			{
				sendReport = SetearQuery
			};

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			string query = $"SELECT tblcatarticulos.COD1_ART as Codigo, tblcatarticulos.DES1_ART as Descripcion, tblcatarticulos.EXI_ACT as Existencia, round(Sum(EQV_UND * tblrenventas.CAN_ART),4) AS Desp, round(Sum(EQV_UND * tblrenventas.CAN_ART* tblrenventas.PCIO_VEN),2) as Dinero " +
				$"FROM (((tblgralventas INNER JOIN tblrenventas ON tblgralventas.REF_DOC = tblrenventas.REF_DOC) " +
				$"INNER JOIN tblcatarticulos ON tblrenventas.COD1_ART = tblcatarticulos.COD1_ART) " +
				$"INNER JOIN tblgpoarticulos ON tblcatarticulos.COD1_ART = tblgpoarticulos.COD1_ART) " +
				$"Where (tblgralventas.FEC_DOC >= '{parametroA}' and tblgralventas.FEC_DOC <= '{parametroB}' And tblgpoarticulos.COD_GPO = 25 and tblgpoarticulos.COD_AGR={cbDepartamentos.SelectedValue}) " +
				$"GROUP BY tblcatarticulos.COD1_ART;";

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			await Task.Run(() => metodos.SetQuery(query));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
		}
	}
}
