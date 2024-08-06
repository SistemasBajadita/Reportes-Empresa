using Aspose.Cells;
using MySql.Data.MySqlClient;
using Reportes.Modulos;
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
	public partial class FrmPrincipal : Form
	{
		public FrmPrincipal()
		{
			InitializeComponent();
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Imagenes", "LOGO_EMPRESA-removebg-preview.ico");

			this.Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void BtnVentaCosto_Click(object sender, EventArgs e)
		{
			FrmVentaCosto frm = new FrmVentaCosto();
			frm.ShowDialog();
		}

		private void BtnCompras_Click(object sender, EventArgs e)
		{
			FrmComprasPD frm = new FrmComprasPD();
			frm.ShowDialog();
		}

		private void BtnTopProductos_Click(object sender, EventArgs e)
		{
			FrmTopProductos frm = new FrmTopProductos();
			frm.ShowDialog();
		}

		private void BtnActiveReport_Click(object sender, EventArgs e)
		{
			ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			con.SetOnReportsCortes();
			con = null;
		}

		private void BtnCountProducts_Click(object sender, EventArgs e)
		{
			FrmConteo frm = new FrmConteo();
			frm.ShowDialog();
		}

		private void BtnMovAlm_Click(object sender, EventArgs e)
		{
			FrmMovimientos frm = new FrmMovimientos();
			frm.ShowDialog();
		}

		private void BtnVentasDetalladas_Click(object sender, EventArgs e)
		{
			FrmVentaDetallada frm = new FrmVentaDetallada();
			frm.ShowDialog();
		}

		private void BtnTicketChofer_Click(object sender, EventArgs e)
		{
			FrmTickets frm = new FrmTickets();
			frm.ShowDialog();
		}

		private void FrmPrincipal_Load(object sender, EventArgs e)
		{

		}

		private void BtnNegativos_Click(object sender, EventArgs e)
		{
			ClsConnection _con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			DataTable negativos = _con.GetQuery("select cod1_art, des1_art, exi_act from tblcatarticulos where EXI_ACT <0");

			_con.PrintReportInPDFNegativos("Negativos en inventario");
		}

		private void CompararPrecios(string precio1, string precio2)
		{

		}

		private async void BtnCocinaPrecios_Click(object sender, EventArgs e)
		{
			ClsConnection bd = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Archivos de Excel (*.xls)|*.xls";

			if (open.ShowDialog() == DialogResult.OK)
			{
				// Crear y mostrar el formulario de progreso
				ProgressProductsCocina progressForm = new ProgressProductsCocina();
				sendText = progressForm.SetMessage;
				progressForm.Show();

				// Crear la instancia de IProgress<int>
				var progress = new Progress<int>(value =>
				{
					progressForm.UpdateProgress(value); // Usar delegado para actualizar el ProgressBar
				});

				// Ejecutar la tarea larga de forma asíncrona, reportando el progreso
				await Task.Run(() => ProcessExcel(open.FileName, bd, progress));

				// Cerrar el formulario de progreso
				progressForm.Close();

				MessageBox.Show("Proceso completado y archivo Excel actualizado.");
			}
		}

		Action<string> sendText;

		private void ProcessExcel(string filePath, ClsConnection bd, IProgress<int> progress)
		{
			// Cargar el archivo de Excel
			Workbook workbook = new Workbook(filePath);

			// Seleccionar la hoja (index 0 es la primera hoja)
			Worksheet hoja = workbook.Worksheets[0];

			int totalRows = hoja.Cells.MaxDataRow + 1; // Obtener el número total de filas a procesar
			int r = 1;
			Cell codigo = hoja.Cells[r, 0];

			while (!string.IsNullOrEmpty(codigo.Value?.ToString()))
			{
				string q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
                      FROM tblcatarticulos art 
                      INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
                      WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{codigo.Value}';";

				DataTable query = bd.GetQuery(q);

				if (query.Rows.Count > 0)
				{
					string precioBD = query.Rows[0][1].ToString();
					string precioExcel = hoja.Cells[r, 2].Value?.ToString();

					if (precioExcel != precioBD)
					{
						hoja.Cells[r, 2].PutValue(precioBD);
					}
				}
				else
				{
					// Manejo de códigos de 3 y 4 dígitos
					if (codigo.Value.ToString().Length == 3)
					{
						string nuevoCodigo = "0" + codigo.Value.ToString();
						q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
                       FROM tblcatarticulos art 
                       INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
                       WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
						query = bd.GetQuery(q);

						if (query.Rows.Count == 0)
						{
							nuevoCodigo = nuevoCodigo.Insert(3, "-");
							q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
                           FROM tblcatarticulos art 
                           INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
                           WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
							query = bd.GetQuery(q);
						}

						if (query.Rows.Count > 0)
						{
							string precioBD = query.Rows[0][1].ToString();
							string precioExcel = hoja.Cells[r, 2].Value?.ToString();

							if (precioExcel != precioBD)
							{
								hoja.Cells[r, 2].PutValue(precioBD);
							}
						}
						else
						{
							sendText($"El código {codigo.Value} no se encuentra en la base de datos.");
						}
					}
					else if (codigo.Value.ToString().Length == 4)
					{
						string nuevoCodigo = codigo.Value.ToString().Insert(3, "-");
						q = $@"SELECT art.cod1_art, ROUND(pr.pre_iva, 1) 
                       FROM tblcatarticulos art 
                       INNER JOIN tblprecios pr ON pr.COD1_ART = art.COD1_ART
                       WHERE pr.COD_LISTA = 1 AND art.cod1_art = '{nuevoCodigo}';";
						query = bd.GetQuery(q);

						if (query.Rows.Count > 0)
						{
							string precioBD = query.Rows[0][1].ToString();
							string precioExcel = hoja.Cells[r, 2].Value?.ToString();

							if (precioExcel != precioBD)
							{
								hoja.Cells[r, 2].PutValue(precioBD);
							}
						}
						else
						{
							sendText($"El código {codigo.Value} no se encuentra en la base de datos.");
						}
					}
				}

				r++;
				codigo = hoja.Cells[r, 0];

				// Reportar el progreso
				int progressPercentage = (int)((r / (float)totalRows) * 100);
				progress.Report(progressPercentage);
			}

			// Guardar los cambios en el archivo Excel
			workbook.Save(filePath);
		}

	}
}
