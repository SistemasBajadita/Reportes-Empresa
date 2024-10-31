using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private async void FrmVentaDetallada_Load(object sender, EventArgs e)
        {

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
            // Crear un rectángulo que cubra todo el formulario
            Rectangle rect = this.ClientRectangle;

            // Definir los colores del degradado (por ejemplo, de azul a blanco)
            Color color1 = Color.FromArgb(251, 147, 60); //--original
            Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original
                                                                //Color color1 = ColorTranslator.FromHtml("#0C1A47");
                                                                //Color color2 = ColorTranslator.FromHtml("#EC3F5D");

            // Crear un pincel con un degradado lineal
            if (rect.X > 0 && rect.Y > 0)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.BackwardDiagonal))
                {
                    // Dibujar el degradado en el fondo del formulario
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }
    }
}