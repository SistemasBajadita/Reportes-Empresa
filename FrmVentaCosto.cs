using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmVentaCosto : Form
	{
		ClsConnection metodos;

		public FrmVentaCosto()
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

			DateTime fechaA = FechaA.Value;
			DateTime fechaB = FechaB.Value;

			string parametroA = fechaA.ToString("yyyy-MM-dd");
			string parametroB = fechaB.ToString("yyyy-MM-dd");

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			await Task.Run(() => metodos.ReportVentasCosto(parametroA, parametroB));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
		}

		private void BtnExcel_Click(object sender, EventArgs e)
		{
			if(metodos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "No se puede guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			guardarArchivo.Filter = "Archivos de Excel|*.xlsx|Todos los archivos|*.*";
			if (guardarArchivo.ShowDialog() == DialogResult.OK)
			{
				metodos.PrintReportInExcel(guardarArchivo.FileName);
				Process.Start(guardarArchivo.FileName);
			}
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

			if (guardarArchivo.ShowDialog() ==DialogResult.OK)
			{
				metodos.PrintReportInPDF(parametroA, parametroB, guardarArchivo.FileName);
				Process.Start(guardarArchivo.FileName);
			}
		}
	}
}
