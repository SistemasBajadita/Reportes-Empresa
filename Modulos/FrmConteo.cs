using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class FrmConteo : Form
    {
        ClsConnection metodos;
        public FrmConteo()
        {
            InitializeComponent();
            Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
        }

        public async void SetOptionInCombo()
        {
            cbOP.DataSource = null;
            cbOP.Items.Clear();

            metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());

            DataTable opciones = null;

            label2.Visible = true;

            if (rbCategoria.Checked)
            {
                await Task.Run(() => opciones = metodos.GetQuery("select cod_agr, des_agr from tblcatagrupacionart where cod_gpo=26 order by des_agr asc"));
            }
            else
            {
                await Task.Run(() => opciones = metodos.GetQuery("select cod_agr, des_agr from tblcatagrupacionart where cod_gpo=25 order by des_agr asc"));
            }

            cbOP.DataSource = opciones;
            cbOP.DisplayMember = "des_agr";
            cbOP.ValueMember = "cod_agr";

            label2.Visible = false;
            metodos = null;

        }

        private string GetSelectedTextFromCombo()
        {
            // Asegúrate de que hay un elemento seleccionado
            if (cbOP.SelectedItem != null)
            {
                // Accede al DataRowView del elemento seleccionado
                DataRowView selectedRow = cbOP.SelectedItem as DataRowView;

                // Asegúrate de que la conversión fue exitosa
                if (selectedRow != null)
                {
                    // Obtén el texto del elemento seleccionado usando el DisplayMember
                    string selectedText = selectedRow[cbOP.DisplayMember].ToString();

                    // Muestra el texto seleccionado (por ejemplo, en un MessageBox)
                    return selectedText;
                }
            }
            return null;
        }

        public void ChangeRadios(object sender, EventArgs e)
        {
            SetOptionInCombo();
        }

        private void GetReport(DataTable data)
        {
            //metodo de comodin para que no de excepcion mi metodo en la clase ClsConnection
        }

        private async void BtnPDF_Click(object sender, EventArgs e)
        {
            metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            metodos.sendReport = GetReport;

            string query = $"select art.cod1_art as Codigo, des1_art as Descripcion, c.Cos_Pro as Costo, EXI_ACT as Existencia " +
                $"from tblcatarticulos art " +
                $"inner join tblundcospreart c on c.COD1_ART=art.COD1_ART " +
                $"inner join tblgpoarticulos g on g.COD1_ART=art.COD1_ART " +
                $"where COD_AGR={cbOP.SelectedValue};";

            await Task.Run(() => metodos.SetQuery(query));

            if (metodos == null)
            {
                MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            guardarArchivo.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*";
            guardarArchivo.FileName = $"conteo {GetSelectedTextFromCombo()}_{DateTime.Now:dd-MM-yy}";

            string Encabezado = "";

            if (rbDepartamento.Checked)
                Encabezado = $"Departamento : {GetSelectedTextFromCombo()}";
            else
                Encabezado = $"Categoria : {GetSelectedTextFromCombo()}";


            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                metodos.PrintReportInPDFExistencia(guardarArchivo.FileName, Encabezado);
                Process.Start(guardarArchivo.FileName);
            }
        }

        private async void FrmConteo_Load(object sender, EventArgs e)
        {
            cbOP.DataSource = null;
            cbOP.Items.Clear();

            metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());

            DataTable opciones = null;

            label2.Visible = true;

            await Task.Run(() => opciones = metodos.GetQuery("select cod_agr, des_agr from tblcatagrupacionart where cod_gpo=25 order by des_agr asc"));

            cbOP.DataSource = opciones;
            cbOP.DisplayMember = "des_agr";
            cbOP.ValueMember = "cod_agr";

            label2.Visible = false;
            metodos = null;
        }

        private void FrmConteo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
