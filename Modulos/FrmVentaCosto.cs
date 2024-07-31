using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmVentaCosto : Form
	{
		ClsConnection metodos;

		public FrmVentaCosto()
		{
			InitializeComponent();

			string anio = DateTime.Now.Year.ToString();
			string mes = DateTime.Now.Month.ToString();
			string dya = (DateTime.Now.Day - 1).ToString();

			FechaA.MaxDate = new DateTime(int.Parse(anio), int.Parse(mes), int.Parse(dya));
			FechaB.MaxDate = new DateTime(int.Parse(anio), int.Parse(mes), int.Parse(dya));

			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void SetearQuery(DataSet quer)
		{
			try
			{
				Invoke(new Action(() =>
				{
					//double total = 0;
					//foreach (DataRow dr in quer.Rows)
					//{
					//	total += Convert.ToDouble(dr[1]);
					//}
					//lblTotal.Visible = true;
					//lblTotal.Text = $"Total: ${total}";
					//reporte.DataSource = quer;
					reporte.Columns.Clear();
					reporte.Columns.Add("Departamento", "Departamento");
					reporte.Columns.Add("Venta", "Venta");
					reporte.Columns.Add("Costo", "Costo");
					reporte.Columns.Add("Utilidad", "Utilidad");
					reporte.Columns.Add("Merma", "Merma");
					reporte.Columns.Add("M/V", "M/V");

					DataTable venta = quer.Tables[0];
					DataTable merma = quer.Tables[1];

					foreach (DataRow row in venta.Rows)
					{
						bool mer = true;
						foreach (DataRow r in merma.Rows)
						{
							if (r[0].ToString() == row[0].ToString())
							{
								double porcentaje = (Convert.ToDouble(r[1]) / Convert.ToDouble(row[1])) * 100;
								reporte.Rows.Add(row[0], row[1], row[2], row[3], r[1], Math.Round(porcentaje, 2));
								merma.Rows.Remove(r);
								mer = false;
								break;
							}
							
						}
						if (mer) reporte.Rows.Add(row[0], row[1], row[2], row[3], "0.00", "0.00");
					}
				}));
			}
			catch (Exception) { }
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			lblTotal.Visible = false;
			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["empresa"].ToString())
			{
				sendTables = SetearQuery
			};

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			string query = "select caa.DES_AGR as Departamento, round(sum(rv.CAN_ART * rv.PCIO_VEN),2) as 'Venta Total', round(sum(rv.CAN_ART * rv.COS_VEN),2) as Costo, round((1 - (sum(rv.CAN_ART * rv.COS_VEN) / sum(rv.CAN_ART * rv.PCIO_VEN))) * 100, 2) as Porc from tblgralventas gv " +
					"inner join tblrenventas rv on gv.REF_DOC = rv.REF_DOC " +
					"inner join tblgpoarticulos ga on rv.COD1_ART = ga.COD1_ART " +
					"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where  (gv.FEC_DOC between '{parametroA}' and '{parametroB}') and ga.COD_GPO = 25 " +
					$"GROUP BY caa.DES_AGR " +
					$"ORDER BY 'Departamento' ASC; select caa.DES_AGR as Departamento, round(sum(cos_uni*can_ren),2) as Merma\r\n\t\t\t\t\t\t\t\t\t\t\t\tfrom tblrenalmacen ren_alm\r\n\t\t\t\t\t\t\t\t\t\t\t\tinner join tblgralalmacen enc_alm on ren_alm.REF_MOV=enc_alm.REF_MOV\r\n\t\t\t\t\t\t\t\t\t\t\t\tinner join tblgpoarticulos ga on ren_alm.COD1_ART = ga.COD1_ART \r\n\t\t\t\t\t\t\t\t\t\t\t\tinner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR\r\n\t\t\t\t\t\t\t\t\t\t\t\twhere (enc_alm.FEC_MOV between'{parametroA}' and '{parametroB}') and enc_alm.cod_con='SMER' and caa.COD_GPO=25\r\n\t\t\t\t\t\t\t\t\t\t\t\tgroup by caa.des_agr;";

			BtnCorrerQuery.Enabled = false;
			label4.Visible = true;
			FechaA.Enabled = false;
			FechaB.Enabled = false;

			cronometro = new Stopwatch();
			cronometro.Start();

			await Task.Run(() => metodos.SetQuery(query));

			cronometro.Stop();

			Thread t = new Thread(SetLabel);
			t.Start();

			BtnCorrerQuery.Enabled = true;
			label4.Visible = false;
			FechaA.Enabled = true;
			FechaB.Enabled = true;

		}

		Stopwatch cronometro;

		public void SetLabel()
		{

			try
			{
				Invoke(new Action(() =>
				{
					label5.Visible = true;
					label5.Text = $"Tiempo de respuesta: {cronometro.ElapsedMilliseconds / 1000} s";


				}));
				Thread.Sleep(6000);
				Invoke(new Action(() => label5.Visible = false));
			}
			catch (Exception)
			{

			}
		}

		private void BtnExcel_Click(object sender, EventArgs e)
		{
			if (metodos == null)
			{
				MessageBox.Show("Primero presiona el boton de Ver Reporte antes de guardarlo.", "La Bajadita - Venta de Frutas y Verduras", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			guardarArchivo.Filter = "Archivos de Excel|*.xlsx|Todos los archivos|*.*";
			if (guardarArchivo.ShowDialog() == DialogResult.OK)
			{
				metodos.PrintReportInExcel(guardarArchivo.FileName);
				Process.Start(guardarArchivo.FileName);
			}
		}

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
			guardarArchivo.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*";
			guardarArchivo.FileName = $"Ventas_{DateTime.Now:dd-MM-yy}";

			if (guardarArchivo.ShowDialog() == DialogResult.OK)
			{
				metodos.PrintReportInPDFVentas(parametroA, parametroB, guardarArchivo.FileName);
				Process.Start(guardarArchivo.FileName);
			}
		}
	}
}
