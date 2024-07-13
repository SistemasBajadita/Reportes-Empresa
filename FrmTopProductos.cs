using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
			string anio = DateTime.Now.Year.ToString();
			string mes = DateTime.Now.Month.ToString();
			string dya = (DateTime.Now.Day - 1).ToString();

			FechaA.MaxDate = new DateTime(int.Parse(anio), int.Parse(mes), int.Parse(dya));
			FechaB.MaxDate = new DateTime(int.Parse(anio), int.Parse(mes), int.Parse(dya));

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
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString())
			{
				sendReport = SetearQuery
			};

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			string query = "";

			if (rbDesplazamiento.Checked)
			{
				query = $"SELECT tblcatarticulos.COD1_ART as Codigo, tblcatarticulos.DES1_ART as Descripcion, tblcatarticulos.EXI_ACT as Existencia, round(Sum(EQV_UND * tblrenventas.CAN_ART),2) AS Desp " +
					$"FROM (((tblgralventas INNER JOIN tblrenventas ON tblgralventas.REF_DOC = tblrenventas.REF_DOC) " +
					$"INNER JOIN tblcatarticulos ON tblrenventas.COD1_ART = tblcatarticulos.COD1_ART) " +
					$"INNER JOIN tblgpoarticulos ON tblcatarticulos.COD1_ART = tblgpoarticulos.COD1_ART) " +
					$"Where (tblgralventas.FEC_DOC >= '{parametroA}' and tblgralventas.FEC_DOC <= '{parametroB}' And tblgpoarticulos.COD_GPO = 25 and tblgpoarticulos.COD_AGR={cbDepartamentos.SelectedValue}) " +
					$"GROUP BY tblcatarticulos.COD1_ART " +
					$"ORDER BY Desp desc limit 30;";

				tupe = "desplazamiento";

			}
			if (rbDinero.Checked)
			{
				query = $"SELECT tblcatarticulos.COD1_ART as Codigo, tblcatarticulos.DES1_ART as Descripcion, tblcatarticulos.EXI_ACT as Existencia,round( Sum(EQV_UND * tblrenventas.CAN_ART* tblrenventas.PCIO_VEN),2) as Dinero " +
					$"FROM (((tblgralventas INNER JOIN tblrenventas ON tblgralventas.REF_DOC = tblrenventas.REF_DOC) " +
					$"INNER JOIN tblcatarticulos ON tblrenventas.COD1_ART = tblcatarticulos.COD1_ART) " +
					$"INNER JOIN tblgpoarticulos ON tblcatarticulos.COD1_ART = tblgpoarticulos.COD1_ART) " +
					$"Where (tblgralventas.FEC_DOC >= '{parametroA}' and tblgralventas.FEC_DOC <= '{parametroB}' And tblgpoarticulos.COD_GPO = 25 and tblgpoarticulos.COD_AGR={cbDepartamentos.SelectedValue}) " +
					$"GROUP BY tblcatarticulos.cod1_art " +
					$"order by Dinero desc limit 30;";
				tupe = "dinero";
			}
			if (!rbDesplazamiento.Checked && !rbDinero.Checked)
			{
				MessageBox.Show("Selecciona algun parametro por favor", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
			cbDepartamentos .Enabled = true;
		}

		private async void FrmTopProductos_Load(object sender, EventArgs e)
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
			cbDepartamentos.Enabled = true;
			metodos = null;
		}

		string tupe = "";
		string departamento = "";


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
				metodos.PrintReportInPDFTOP(parametroA, parametroB, guardarArchivo.FileName, tupe, departamento);
				Process.Start(guardarArchivo.FileName);
			}
		}
	}
}
