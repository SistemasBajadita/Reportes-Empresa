using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmMovimientos : Form
	{
		ClsConnection movimientos;

		public FrmMovimientos()
		{
			InitializeComponent();

			DataTable conceptos = new DataTable();
			conceptos.Columns.Add("fol", typeof(string));
			conceptos.Columns.Add("nom", typeof(string));

			conceptos.Rows.Add("SMER", "SALIDA POR MERMA");

			cbConceptos.DataSource = conceptos;
			cbConceptos.DisplayMember = "nom";
			cbConceptos.ValueMember = "fol";

			cbConceptos2.DataSource = conceptos;
			cbConceptos2.DisplayMember = "nom";
			cbConceptos2.ValueMember = "fol";
		}

		private void SetearQuery(DataTable quer)
		{
			Invoke(new Action(() => { reporte.DataSource = quer; }));
		}

		private void SetearQuery2(DataTable quer)
		{
			try
			{
				Invoke(new Action(() => { reporte2.DataSource = quer; }));
			}
			catch (Exception) { }
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			movimientos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString())
			{
				sendReport = SetearQuery
			};

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			string query = $"SELECT tblCatAgrupacionArt.DES_AGR as Departamento, concat('$',round(Sum(COS_UNI * can_ren * tblGralAlmacen.TIP_CAM),2)) AS Total " +
				$"FROM ((tblGralAlmacen INNER JOIN tblRenAlmacen ON tblGralAlmacen.REF_MOV = tblRenAlmacen.REF_MOV) " +
				$"INNER JOIN tblCatArticulos ON tblRenAlmacen.COD1_ART = tblCatArticulos.COD1_ART) " +
				$"INNER JOIN tblGpoArticulos ON tblCatArticulos.COD1_ART = tblGpoArticulos.COD1_ART " +
				$"INNER JOIN tblCatAgrupacionArt ON tblCatAgrupacionArt.COD_AGR = tblGpoArticulos.COD_AGR " +
				$"Where ((tblGralAlmacen.FEC_MOV >= '{parametroA}' And tblGralAlmacen.FEC_MOV <= '{parametroB}') And (tblGralAlmacen.COD_CON = '{cbConceptos.SelectedValue}') and (tblGralAlmacen.cod_sts =1) " +
				$"AND (tblGpoArticulos.COD_GPO = 25)) GROUP BY tblGpoArticulos.COD_AGR, tblCatagrupacionArt.COD_AGR;";

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			await Task.Run(() => movimientos.SetQuery(query));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;

		}

		private string GetSelectedTextFromCombo()
		{
			// Asegúrate de que hay un elemento seleccionado
			if (cbConceptos.SelectedItem != null)
			{
				// Accede al DataRowView del elemento seleccionado
				DataRowView selectedRow = cbConceptos.SelectedItem as DataRowView;

				// Asegúrate de que la conversión fue exitosa
				if (selectedRow != null)
				{
					// Obtén el texto del elemento seleccionado usando el DisplayMember
					string selectedText = selectedRow[cbConceptos.DisplayMember].ToString();

					// Muestra el texto seleccionado (por ejemplo, en un MessageBox)
					return selectedText;
				}
			}
			return null;
		}

		private void BtnPDF_Click(object sender, EventArgs e)
		{
			if (movimientos == null)
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
				movimientos.PrintReportForMovimientos(parametroA, parametroB, guardarArchivo.FileName, GetSelectedTextFromCombo());
				Process.Start(guardarArchivo.FileName);
			}
		}

		private async void FrmMovimientos_Load(object sender, EventArgs e)
		{
			movimientos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString());
			await Task.Run(() =>
			{
				DataTable departamentos = movimientos.GetQuery("select cod_agr as Codigo, des_agr as Agrupacion " +
					"from tblcatagrupacionart agr inner join tblagrupacionart gpo on gpo.cod_gpo=agr.COD_GPO " +
					"where agr.cod_gpo=25");
				Invoke(new Action(() =>
				{
					cbDepartamentos.DataSource = departamentos;
					cbDepartamentos.ValueMember = "Codigo";
					cbDepartamentos.DisplayMember = "Agrupacion";
				}));
			});
			movimientos = null;
		}

		private async void BtnCorrerQueryArticulos_Click(object sender, EventArgs e)
		{
			movimientos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString())
			{
				sendReport = SetearQuery2
			};

			string parametroA = FechaA2.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB2.Value.ToString("yyyy-MM-dd");

			string query = $"SELECT tblCatArticulos.cod1_art as Codigo, des1_art as Descripcion, Sum(can_ren) as Piezas, Sum(COS_UNI * can_ren * tblGralAlmacen.TIP_CAM) AS Costo " +
				$"FROM ((tblGralAlmacen INNER JOIN tblRenAlmacen ON tblGralAlmacen.REF_MOV = tblRenAlmacen.REF_MOV) INNER JOIN tblCatArticulos ON tblRenAlmacen.COD1_ART = tblCatArticulos.COD1_ART) " +
				$"INNER JOIN tblGpoArticulos ON tblCatArticulos.COD1_ART = tblGpoArticulos.COD1_ART " +
				$"Where ((tblGralAlmacen.FEC_MOV >= '{parametroA}' And tblGralAlmacen.FEC_MOV <= '{parametroB}') And (tblGralAlmacen.COD_CON = '{cbConceptos2.SelectedValue}') AND (tblGpoArticulos.COD_AGR = {cbDepartamentos.SelectedValue})) " +
				$"GROUP BY tblCatArticulos.cod1_art " +
				$"Order BY Costo DESC;";

			BtnCorrerQueryArticulos.Enabled = false;
			label5.Visible = true;
			FechaA2.Enabled = false;
			FechaB2.Enabled = false;

			await Task.Run(() => movimientos.SetQuery(query));

			BtnCorrerQueryArticulos.Enabled = true;
			label5.Visible = false;
			FechaA2.Enabled = true;
			FechaB2.Enabled = true;
		}
	}
}
