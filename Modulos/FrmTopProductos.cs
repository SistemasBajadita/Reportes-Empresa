using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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

		private string GetSelectedTextFromCombo()
		{
			// Asegúrate de que hay un elemento seleccionado
			if (cbDepartamentos.SelectedItem != null)
			{
				// Accede al DataRowView del elemento seleccionado
				DataRowView selectedRow = cbDepartamentos.SelectedItem as DataRowView;

				// Asegúrate de que la conversión fue exitosa
				if (selectedRow != null)
				{
					// Obtén el texto del elemento seleccionado usando el DisplayMember
					string selectedText = selectedRow[cbDepartamentos.DisplayMember].ToString();

					// Muestra el texto seleccionado (por ejemplo, en un MessageBox)
					return selectedText;
				}
			}
			return null;
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			string query = "";
			if (Program.Empresa == 0)
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
			else if (Program.Empresa == 1)
			{
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());
			}


			metodos.sendReport = SetearQuery;


			if (rbDesplazamiento.Checked)
			{
				query = $"SELECT tblcatarticulos.COD1_ART as Codigo, tblcatarticulos.DES1_ART as Descripcion, tblcatarticulos.EXI_ACT as Existencia, round(Sum(EQV_UND * tblrenventas.CAN_ART),2) AS Desp " +
					$"FROM (((tblgralventas INNER JOIN tblrenventas ON tblgralventas.REF_DOC = tblrenventas.REF_DOC) " +
					$"INNER JOIN tblcatarticulos ON tblrenventas.COD1_ART = tblcatarticulos.COD1_ART) " +
					$"INNER JOIN tblgpoarticulos ON tblcatarticulos.COD1_ART = tblgpoarticulos.COD1_ART) " +
					$"Where (tblgralventas.FEC_DOC >= '{parametroA}' and tblgralventas.FEC_DOC <= '{parametroB}' And tblgpoarticulos.COD_GPO = {(Program.Empresa == 0 ? "25" : "1")} and tblgpoarticulos.COD_AGR={cbDepartamentos.SelectedValue}) " +
					$"GROUP BY tblcatarticulos.COD1_ART " +
					$"ORDER BY Desp desc;";

				tupe = "desplazamiento";

			}
			if (rbDinero.Checked)
			{
				query = $"SELECT tblcatarticulos.COD1_ART as Codigo, tblcatarticulos.DES1_ART as Descripcion, tblcatarticulos.EXI_ACT as Existencia,round( Sum(EQV_UND * tblrenventas.CAN_ART* tblrenventas.PCIO_VEN),2) as Dinero " +
					$"FROM (((tblgralventas INNER JOIN tblrenventas ON tblgralventas.REF_DOC = tblrenventas.REF_DOC) " +
					$"INNER JOIN tblcatarticulos ON tblrenventas.COD1_ART = tblcatarticulos.COD1_ART) " +
					$"INNER JOIN tblgpoarticulos ON tblcatarticulos.COD1_ART = tblgpoarticulos.COD1_ART) " +
					$"Where (tblgralventas.FEC_DOC >= '{parametroA}' and tblgralventas.FEC_DOC <= '{parametroB}' And tblgpoarticulos.COD_GPO = {(Program.Empresa == 0 ? "25" : "1")} and tblgpoarticulos.COD_AGR={cbDepartamentos.SelectedValue}) " +
					$"GROUP BY tblcatarticulos.cod1_art " +
					$"order by Dinero desc;";
				tupe = "dinero";
			}
			if (!rbDesplazamiento.Checked && !rbDinero.Checked)
			{
				MessageBox.Show("Selecciona algun parametro por favor", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Question);
				return;
			}

			departamento = GetSelectedTextFromCombo();

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;
			cbDepartamentos.Enabled = false;

			await Task.Run(() => metodos.SetQuery(query));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
			cbDepartamentos.Enabled = true;
		}

		private async void FrmTopProductos_Load(object sender, EventArgs e)
		{
			cbDepartamentos.Enabled = false;
			if (Program.Empresa == 0)

				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			else if (Program.Empresa == 1)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());
			await Task.Run(() =>
			{
				DataTable departamentos = metodos.GetQuery("select cod_agr as Codigo, des_agr as Agrupacion " +
					"from tblcatagrupacionart agr inner join tblagrupacionart gpo on gpo.cod_gpo=agr.COD_GPO " +
					$"where agr.cod_gpo={(Program.Empresa == 0 ? "25" : "1")} order by Agrupacion asc");

				try
				{
					Invoke(new Action(() =>
					{
						cbDepartamentos.DataSource = departamentos;
						cbDepartamentos.ValueMember = "Codigo";
						cbDepartamentos.DisplayMember = "Agrupacion";
					}));
				}
				catch (InvalidOperationException ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			});
			cbDepartamentos.Enabled = true;
			metodos = null;
		}

		string tupe = "";
		string departamento = "";


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

			metodos.PrintReportInPDFTOP(parametroA, parametroB, "productos.pdf", tupe, departamento);
			Process.Start("productos.pdf");

		}
	}
}
