using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmComprasPD : Form
	{
		ClsConnection metodos;

		public FrmComprasPD()
		{
			InitializeComponent();
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

			string query = "SELECT tblcatagrupacionart.DES_AGR as Departamento, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam),2) AS Total, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam*tblcomprasren.imp1_art)/100,2) AS Iva " +
				"FROM (tblcomprasenc INNER JOIN tblcomprasren ON tblcomprasenc.FOL_DOC = tblcomprasren.FOL_DOC) " +
				"INNER JOIN ((tblCatArticulos INNER JOIN tblGpoArticulos ON tblCatArticulos.COD1_ART = tblGpoArticulos.COD1_ART) " +
				"INNER JOIN tblcatagrupacionart ON tblGpoArticulos.COD_AGR = tblcatagrupacionart.COD_AGR) ON tblcomprasren.COD1_ART = tblCatArticulos.COD1_ART " +
				$"WHERE (((tblcomprasenc.COD_ESTATUS)=1) AND ((tblGpoArticulos.COD_GPO)=25)) and (tblcomprasenc.FEC_DOC)>= '{parametroA}' and (tblcomprasenc.FEC_DOC)<= '{parametroB}' GROUP BY tblcatagrupacionart.DES_AGR " +
				$"ORDER BY Departamento ASC;";

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			await Task.Run(() => metodos.ReportVentasCosto(query));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
		}

		private void BtnPDF_Click(object sender, EventArgs e)
		{
			if (metodos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "No se puede guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DateTime fechaA = FechaA.Value;
			DateTime fechaB = FechaB.Value;

			string parametroA = fechaA.ToString("yyyy/MM/dd");
			string parametroB = fechaB.ToString("yyyy/MM/dd");
			guardarArchivo.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*";

			if (guardarArchivo.ShowDialog() == DialogResult.OK)
			{
				metodos.PrintReportInPDFCompras(parametroA, parametroB, guardarArchivo.FileName);
				Process.Start(guardarArchivo.FileName);
			}
		}
	}
}
