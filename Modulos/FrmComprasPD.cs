using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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

			FechaA.MaxDate = DateTime.Now;
			FechaB.MaxDate = DateTime.Now;

			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void SetearQuery(DataTable quer)
		{
			try
			{
				Invoke(new Action(() => { reporte.DataSource = quer; }));
			}
			catch (Exception) { }
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			string query = "";

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");


			if (Program.Empresa == 0)
			{
				if (!(FechaA.Value.Date < new DateTime(2025, 02, 01) || FechaA.Value.Date < new DateTime(2025, 02, 01)))
				{
					if (FechaA.Value.Date == DateTime.Now.Date && FechaB.Value.Date == DateTime.Now.Date)
					{
						metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
					}
					else
					{
						metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
					}
				}
				else
				{
					metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["antes"].ToString());
				}

				query = "SELECT tblcatagrupacionart.DES_AGR as Departamento, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam),2) AS Total, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam*tblcomprasren.imp1_art)/100,2) AS Iva " +
				"FROM (tblcomprasenc INNER JOIN tblcomprasren ON tblcomprasenc.FOL_DOC = tblcomprasren.FOL_DOC) " +
				"INNER JOIN ((tblCatArticulos INNER JOIN tblGpoArticulos ON tblCatArticulos.COD1_ART = tblGpoArticulos.COD1_ART) " +
				"INNER JOIN tblcatagrupacionart ON tblGpoArticulos.COD_AGR = tblcatagrupacionart.COD_AGR) ON tblcomprasren.COD1_ART = tblCatArticulos.COD1_ART " +
				$"WHERE (((tblcomprasenc.COD_ESTATUS)=1) AND ((tblGpoArticulos.COD_GPO)=25)) and (tblcomprasenc.FEC_DOC)>= '{parametroA}' and (tblcomprasenc.FEC_DOC)<= '{parametroB}' GROUP BY tblcatagrupacionart.DES_AGR " +
				$"ORDER BY Departamento ASC;";
			}
			else if (Program.Empresa == 1)
			{
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());
				query = "SELECT tblcatagrupacionart.DES_AGR as Departamento, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam),2) AS Total, round(Sum(tblcomprasren.COS_com*tblcomprasren.can_art*tblcomprasenc.tip_cam*tblcomprasren.imp1_art)/100,2) AS Iva " +
				"FROM (tblcomprasenc INNER JOIN tblcomprasren ON tblcomprasenc.FOL_DOC = tblcomprasren.FOL_DOC) " +
				"INNER JOIN ((tblCatArticulos INNER JOIN tblGpoArticulos ON tblCatArticulos.COD1_ART = tblGpoArticulos.COD1_ART) " +
				"INNER JOIN tblcatagrupacionart ON tblGpoArticulos.COD_AGR = tblcatagrupacionart.COD_AGR) ON tblcomprasren.COD1_ART = tblCatArticulos.COD1_ART " +
				$"WHERE (((tblcomprasenc.COD_ESTATUS)=1) AND ((tblGpoArticulos.COD_GPO)=1)) and (tblcomprasenc.FEC_DOC)>= '{parametroA}' and (tblcomprasenc.FEC_DOC)<= '{parametroB}' GROUP BY tblcatagrupacionart.DES_AGR " +
				$"ORDER BY Departamento ASC;";
			}

			metodos.sendReport = SetearQuery;

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

		private void BtnPDF_Click(object sender, EventArgs e)
		{
			if (metodos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DateTime fechaA = FechaA.Value;
			DateTime fechaB = FechaB.Value;

			string parametroA = fechaA.ToString("yyyy/MM/dd");
			string parametroB = fechaB.ToString("yyyy/MM/dd");
			guardarArchivo.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*";
			guardarArchivo.FileName = $"compras_{DateTime.Now:dd-MM-yy}.pdf";

			metodos.PrintReportInPDFCompras(parametroA, parametroB, "compras.pdf");
			Process.Start("compras.pdf");

		}

		private void BtnExcel_Click(object sender, EventArgs e)
		{

		}

		private void FrmComprasPD_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
