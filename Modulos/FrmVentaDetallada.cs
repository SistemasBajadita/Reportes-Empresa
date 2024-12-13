using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class FrmVentaDetallada : Form
    {
        ClsConnection metodos;
        public FrmVentaDetallada()
        {
            InitializeComponent();
            Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
        }

        private void SetearQuery(DataTable quer)
        {

            DataGridView cell = new DataGridView();
            try
            {
                BeginInvoke(new Action(() =>
                {
                    reporte.Rows.Clear();
                    reporte.Columns.Clear();

                    foreach (DataColumn column in quer.Columns)
                    {
                        reporte.Columns.Add(column.ColumnName, column.ColumnName);
                    }

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

            BtnCorrerQuery.Enabled = false;
            label4.Visible = true;
            cbDepartamentos.Enabled = false;

            string query = $@"SELECT rv.cod1_art as Codigo, 
							   art.DES1_ART as Descripcion, 
							   round(AVG(rv.cos_ven),2) AS CostoPromedio, 
							   round(lc.last_cos_uni,2) as UltimaCompra, 
							   round(tp.pre_iva,2) as PrecioActual, 
							   round((1 - (COALESCE(lc.last_cos_uni, AVG(rv.cos_ven)) / tp.pre_iva)) * 100,2) AS MargenActual
								FROM tblrenventas rv
								LEFT JOIN temp_last_compras lc ON rv.cod1_art = lc.cod1_art
								LEFT JOIN temp_precios tp ON rv.cod1_art = tp.cod1_art
								INNER JOIN tblgpoarticulos gpo on gpo.COD1_ART = rv.cod1_art
								INNER JOIN tblcatarticulos art on art.COD1_ART = rv.cod1_art
								WHERE gpo.COD_AGR = {cbDepartamentos.SelectedValue}
								GROUP BY rv.cod1_art, lc.last_cos_uni, tp.pre_iva;";

            await Task.Run(() => metodos.SetQuery(query));

            BtnCorrerQuery.Enabled = true;
            label4.Visible = false;
            cbDepartamentos.Enabled = true;
        }

        private void FrmVentaDetallada_FormClosed(object sender, FormClosedEventArgs e)
        {
            //metodos.SetQuery("DROP TABLE temp_last_compras; DROP TABLE temp_precios;");
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            metodos?.PrintReportPDFMargen("Departamento: " + GetSelectedTextFromCombo(), "Reporte de margenes de articulos");
        }

        private void FrmVentaDetallada_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void FrmVentaDetallada_Load(object sender, EventArgs e)
        {
            BtnCorrerQuery.Enabled = false;
            lblMessage.Visible = true;
            cbDepartamentos.Enabled = false;
            BtnPDF.Enabled = false;
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
            await Task.Run(() =>
            {
                metodos.SetQuery(@"CREATE TEMPORARY TABLE temp_last_compras AS
									SELECT cod1_art, cos_uni AS last_cos_uni
									FROM (
										SELECT cr.cod1_art, cr.cos_uni, 
											   ROW_NUMBER() OVER (PARTITION BY cr.cod1_art ORDER BY cr.fol_doc DESC) AS rn
										FROM tblcomprasren cr
									) t
									WHERE rn = 1;
                    
									CREATE TEMPORARY TABLE temp_precios AS
									SELECT p.cod1_art, p.pre_iva 
									FROM tblprecios p 
									WHERE p.cod_lista = 1;");
            });
            metodos = null;
            BtnPDF.Enabled = true;
            cbDepartamentos.Enabled = true;
            lblMessage.Visible = false;
            BtnCorrerQuery.Enabled = true;
        }
    }
}