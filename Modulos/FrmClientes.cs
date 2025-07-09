using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos
{
	public partial class FrmClientes : Form
	{

		//Objeto global para hacer la consulta, y generar los reportes
		ClsConnection con;


		public FrmClientes()
		{
			InitializeComponent();
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			if (Program.Empresa == 0)
			{
				if (FechaA.Value < new DateTime(2025, 02, 01) && FechaA.Value < new DateTime(2025, 02, 01))
					con = new ClsConnection(ConfigurationManager.ConnectionStrings["antes"].ToString());
				else
				{
					if (FechaA.Value.Month == DateTime.Now.Month)
						con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
					else
						con = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
				}
			}
			else
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ConnectionString);

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			con.sendReport = metodo;

			await Task.Run(() =>
			{
				con.SetQuery($@"SELECT 
								  gral.cod_cli AS Codigo, 
								  cli.NOM_CLI AS Cliente, 
								  SUM(
									CASE 
									  WHEN COALESCE(dev.tot_dev, 0) < gral.tot_doc THEN 1
									  ELSE 0
									END
								  ) AS Tickets,
								  ROUND(SUM(gral.tot_doc - COALESCE(dev.tot_dev, 0)), 2) AS SumaTotal
								FROM tblgralventas gral
								LEFT JOIN tblencdevolucion dev ON dev.REF_DOC = gral.REF_DOC
								INNER JOIN tblcatclientes cli ON cli.COD_CLI = gral.cod_cli
								WHERE gral.fec_doc BETWEEN '{parametroA}' AND '{parametroB}'
								  AND gral.cod_cli != 'PUBLIC'
								GROUP BY gral.cod_cli, cli.NOM_CLI
								ORDER BY SumaTotal DESC;");
			});

		}


		private void metodo(DataTable table)
		{
			Invoke(new Action(() => reporte.DataSource = table));
		}

		private void BtnPDF_Click(object sender, EventArgs e)
		{
			if (con == null)
			{
				MessageBox.Show("Presiona ver reporte antes de descargar la informacion",
					"OJO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string parametroA = FechaA.Value.ToString("yyyy/MM/dd");
			string parametroB = FechaB.Value.ToString("yyyy/MM/dd");

			con.PrintReportInClientesVentas("Reporte de venta de clientes", $"Periodo: {parametroA} al {parametroB}");
		}
	}
}
