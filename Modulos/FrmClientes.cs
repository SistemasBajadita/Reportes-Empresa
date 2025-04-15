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
					con = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			}
			else
				con = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ConnectionString);

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			con.sendReport = metodo;

			await Task.Run(() =>
			{
				con.SetQuery($@"select gral.cod_cli as Codigo, cli.NOM_CLI as Cliente, count(ref_doc) as Tickets, round(sum(tot_doc),2) as SumaTotal 
								from tblgralventas gral
								inner join tblcatclientes cli on cli.COD_CLI=gral.cod_cli
								where gral.fec_doc between '{parametroA}' and '{parametroB}' 
								group by gral.cod_cli
								order by SumaTotal desc;");
			});

		}


		private void metodo(DataTable table)
		{
			Invoke(new Action(() => reporte.DataSource = table));
		}

		private void BtnPDF_Click(object sender, EventArgs e)
		{

		}
	}
}
