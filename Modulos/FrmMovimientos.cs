using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;
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

			FechaA.MaxDate = DateTime.Now.AddDays(-1);
			FechaB.MaxDate = DateTime.Now.AddDays(-1);

			FechaA2.MaxDate = DateTime.Now.AddDays(-1);
			FechaB2.MaxDate = DateTime.Now.AddDays(-1);

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

			string query = $"SELECT tblCatAgrupacionArt.DES_AGR as Departamento, round(Sum(COS_UNI * can_ren * tblGralAlmacen.TIP_CAM),2) AS Total " +
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

		private string GetSelectedTextFromCombo(int op)
		{

			if (op == 1)
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
			if (op == 2)
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
			if (op == 3)
			{
				// Asegúrate de que hay un elemento seleccionado
				if (cbConceptos2.SelectedItem != null)
				{
					// Accede al DataRowView del elemento seleccionado
					DataRowView selectedRow = cbConceptos2.SelectedItem as DataRowView;

					// Asegúrate de que la conversión fue exitosa
					if (selectedRow != null)
					{
						// Obtén el texto del elemento seleccionado usando el DisplayMember
						string selectedText = selectedRow[cbConceptos2.DisplayMember].ToString();

						// Muestra el texto seleccionado (por ejemplo, en un MessageBox)
						return selectedText;
					}
				}
				return null;
			}
			return null;
		}

		private void BtnPDF_Click(object sender, EventArgs e)
		{
			if (movimientos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DateTime fechaA = FechaA.Value;
			DateTime fechaB = FechaB.Value;

			string parametroA = fechaA.ToString("yyyy/MM/dd");
			string parametroB = fechaB.ToString("yyyy/MM/dd");
			guardarArchivo.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*";
			guardarArchivo.FileName = $"movimientos_{DateTime.Now:dd-MM-yy}";

			if (guardarArchivo.ShowDialog() == DialogResult.OK)
			{
				movimientos.PrintReportForMovimientos(parametroA, parametroB, guardarArchivo.FileName, GetSelectedTextFromCombo(1));
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
				try
				{
					Invoke(new Action(() =>
							{
								cbDepartamentos.DataSource = departamentos;
								cbDepartamentos.ValueMember = "Codigo";
								cbDepartamentos.DisplayMember = "Agrupacion";
							}));
				}
				catch (Exception)
				{

				}
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

			string query = $"SELECT tblCatArticulos.cod1_art as Codigo, des1_art as Descripcion, round(Sum(can_ren),2) as Piezas, round(Sum(COS_UNI * can_ren * tblGralAlmacen.TIP_CAM),2) AS Costo " +
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

		private void BtnPDFDetails_Click(object sender, EventArgs e)
		{
			movimientos.PrintReportMovimientosDetail(GetSelectedTextFromCombo(2), $"Reporte de {GetSelectedTextFromCombo(3)}");
			Process.Start("movimientos.pdf");
		}

		private void FrmMovimientos_Paint(object sender, PaintEventArgs e)
		{
			// Crear un rectángulo que cubra todo el formulario
			System.Drawing.Rectangle rect = this.ClientRectangle;

			// Definir los colores del degradado (por ejemplo, de azul a blanco)
			Color color1 = Color.FromArgb(251, 147, 60); //--original
			Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original

			// Crear un pincel con un degradado lineal
			using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.ForwardDiagonal))
			{
				// Dibujar el degradado en el fondo del formulario
				e.Graphics.FillRectangle(brush, rect);
			}
		}
	}
}
