using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTextSharp.text.pdf.PdfDocument;

namespace Reportes
{
	public partial class FrmVentaDetallada : Form
	{
		ClsConnection metodos;
		public FrmVentaDetallada()
		{
			InitializeComponent();
		}

		private async void FrmVentaDetallada_Load(object sender, EventArgs e)
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
			metodos = null;
			cbDepartamentos.Enabled = true;
		}

		private void SetearQuery(DataTable quer)
		{

			DataGridView cell = new DataGridView();
			try
			{
				BeginInvoke(new Action(() =>
				{
					// Limpiar el DataGridView
					reporte.Rows.Clear();
					reporte.Columns.Clear();

					// Añadir columnas al DataGridView
					foreach (DataColumn column in quer.Columns)
					{
						reporte.Columns.Add(column.ColumnName, column.ColumnName);
					}

					// Añadir filas al DataGridView
					foreach (DataRow row in quer.Rows)
					{
						reporte.Rows.Add(row.ItemArray);
					}
				}));
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

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;
			cbDepartamentos.Enabled = false;

			string query = $@"SELECT gral.FEC_DOC as 'Fecha de Venta',art.cod1_art as Codigo, art.DES1_ART as Descripcion, COS_VEN as Costo,ven.PCIO_UNI as PrecioVenta, can_art as Cantidad , PCIO_UNI*CAN_ART as Total, ((pcio_uni-cos_ven)/pcio_uni)*100 as Margen
								FROM tblrenventas ven
								INNER JOIN tblcatarticulos art on ven.cod1_art=art.cod1_art
								INNER JOIN tblgpoarticulos gpo on art.cod1_art=gpo.cod1_art
								INNER JOIN tblgralventas gral on gral.REF_DOC=ven.REF_DOC
								WHERE gpo.COD_AGR={cbDepartamentos.SelectedValue} and gral.FEC_DOC between '{parametroA}' and '{parametroB}'";

			await Task.Run(() => metodos.SetQuery(query));

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;
			cbDepartamentos.Enabled = true;
		}
	}
}
