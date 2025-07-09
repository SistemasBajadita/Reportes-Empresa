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

			if (Program.Empresa == 0)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			else if (Program.Empresa == 1)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());

			DataTable opciones = null;

			label2.Visible = true;

			if (rbCategoria.Checked)
			{
				await Task.Run(() => opciones = metodos.GetQuery($"select cod_agr, des_agr from tblcatagrupacionart where cod_gpo={(Program.Empresa == 0 ? 26 : 2)} order by des_agr asc;"));
			}
			else
			{
				await Task.Run(() => opciones = metodos.GetQuery($"select cod_agr, des_agr from tblcatagrupacionart where cod_gpo={(Program.Empresa == 0 ? 25 : 1)} order by des_agr asc;"));
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

				// Asegúrate de que la conversión fue exitosa
				if (cbOP.SelectedItem is DataRowView selectedRow)
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
			if (Program.Empresa == 0)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			else if (Program.Empresa == 1)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());

			metodos.sendReport = GetReport;

			string query = $"select art.cod1_art as Codigo, des1_art as Descripcion, c.Cos_Pro as Costo, exi.exi_alm as Existencia " +
				$"from tblcatarticulos art " +
				$"inner join tblundcospreart c on c.COD1_ART=art.COD1_ART and c.eqv_und=1 " +
				$"inner join tblgpoarticulos g on g.COD1_ART=art.COD1_ART " +
				$"inner join tblexiporalmacen exi on exi.cod1_art=art.cod1_art and exi.cod_alm='{cbAlmacen.SelectedValue}' " +
				$"left JOIN tbldescontinuados des on des.COD1_ART=art.cod1_art " +
				$"where COD_AGR={cbOP.SelectedValue} AND (des.NO_VENTA IS NULL OR des.NO_VENTA = 0) order by Descripcion asc;";

			await Task.Run(() => metodos.SetQuery(query));

			if (metodos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			guardarArchivo.FileName = $"conteo {GetSelectedTextFromCombo().Replace("/", " ")}_{DateTime.Now:dd-MM-yy}.pdf";

			string Encabezado = "";

			if (rbDepartamento.Checked)
				Encabezado = $"Departamento : {GetSelectedTextFromCombo()}\nAlmacen: {cbAlmacen.SelectedValue}";
			else
				Encabezado = $"Categoria : {GetSelectedTextFromCombo()}\nAlmacen: {cbAlmacen.SelectedValue}";


			metodos.PrintReportInPDFExistencia(guardarArchivo.FileName, Encabezado);
			Process.Start(guardarArchivo.FileName);

		}

		private async void FrmConteo_Load(object sender, EventArgs e)
		{
			cbOP.DataSource = null;
			cbOP.Items.Clear();

			if (Program.Empresa == 0)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			else if (Program.Empresa == 1)
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());

			DataTable opciones = null;
			DataTable almacenes = null;

			label2.Visible = true;

			await Task.Run(() => opciones = metodos.GetQuery($"select cod_agr, des_agr from tblcatagrupacionart where cod_gpo={(Program.Empresa == 0 ? 25 : 1)} order by des_agr asc"));
			await Task.Run(() => almacenes = metodos.GetQuery("select cod_alm as Codigo, DES_ALM as Almacen from tblcatalmacenes where cod_tip=1;"));
			cbOP.DataSource = opciones;
			cbOP.DisplayMember = "des_agr";
			cbOP.ValueMember = "cod_agr";

			cbAlmacen.DataSource = almacenes;
			cbAlmacen.DisplayMember = "Almacen";
			cbAlmacen.ValueMember = "Codigo";

			label2.Visible = false;
			metodos = null;
		}

		private void FrmConteo_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
