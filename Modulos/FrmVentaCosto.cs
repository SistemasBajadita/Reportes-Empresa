using Reportes.Modulos;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
			FechaA.MaxDate = DateTime.Now;
			FechaB.MaxDate = DateTime.Now;
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");

			if (Program.Empresa == 1)
			{
				BtnExcel.Enabled = false;
			}
		}

		private void SetearQuery(DataSet quer)
		{
			try
			{
				Invoke(new Action(() =>
				{
					reporte.Columns.Clear();
					reporte.Columns.Add("Departamento", "Departamento");
					reporte.Columns.Add("Venta", "Venta");
					reporte.Columns.Add("Costo", "Costo");
					reporte.Columns.Add("Utilidad", "Utilidad");
					reporte.Columns.Add("Merma", "Merma");
					reporte.Columns.Add("M/V", "M/V");

					DataTable venta = quer.Tables[0];
					DataTable merma = quer.Tables[1];
					double total = 0;

					foreach (DataRow row in venta.Rows)
					{
						bool mer = true;
						total += Convert.ToDouble(row[1]);
						foreach (DataRow r in merma.Rows)
						{
							if (r[0].ToString() == row[0].ToString())
							{
								double porcentaje = (Convert.ToDouble(r[1]) / Convert.ToDouble(row[1])) * 100;
								reporte.Rows.Add(row[0], row[1], row[2], row[3], double.Parse(r[1].ToString()), Math.Round(porcentaje, 2));
								merma.Rows.Remove(r);
								mer = false;
								break;
							}

						}
						if (mer) reporte.Rows.Add(row[0], row[1], row[2], row[3], 0.00, 0.00);
					}

					lblTotal.Visible = true;
					lblTotal.Text = $"Total: ${Convert.ToDouble(total):N2}";
				}));
			}
			catch (Exception) { }
		}

		private async void BtnCorrerQuery_Click(object sender, EventArgs e)
		{
			lblTotal.Visible = false;

			string query = "";

			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			if (Program.Empresa == 0)
			{

				if (FechaA.Value.Date < new DateTime(2025, 02, 01) && FechaB.Value.Date < new DateTime(2025, 02, 01))
				{
					metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["antes"].ToString());
				}
				else
				{
					metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
				}

				query = "select caa.DES_AGR as Departamento, round(sum(rv.CAN_ART * rv.PCIO_VEN),2) as 'Venta Total', round(sum(rv.CAN_ART * rv.COS_VEN),2) as Costo, round((1 - (sum(rv.CAN_ART * rv.COS_VEN) / sum(rv.CAN_ART * rv.PCIO_VEN))) * 100, 2) as Porc from tblgralventas gv " +
					"inner join tblrenventas rv on gv.REF_DOC = rv.REF_DOC " +
					"inner join tblgpoarticulos ga on rv.COD1_ART = ga.COD1_ART " +
					"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where  (gv.FEC_DOC between '{parametroA}' and '{parametroB}') and ga.COD_GPO = 25 " +
					$"GROUP BY caa.DES_AGR " +
					$"ORDER BY Departamento ASC; " +
					$"select caa.DES_AGR as Departamento, round(sum(cos_uni*can_ren),2) as Merma " +
					$"from tblrenalmacen ren_alm " +
					$"inner join tblgralalmacen enc_alm on ren_alm.REF_MOV=enc_alm.REF_MOV " +
					$"inner join tblgpoarticulos ga on ren_alm.COD1_ART = ga.COD1_ART " +
					$"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where (enc_alm.FEC_MOV between'{parametroA}' and '{parametroB}') and enc_alm.cod_con='SMER' and caa.COD_GPO=25 AND COD_STS=1 " +
					$"group by caa.des_agr;";
			}
			if (Program.Empresa == 1)
			{
				metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["marcos"].ToString());

				query = "select caa.DES_AGR as Departamento, round(sum(rv.CAN_ART * rv.PCIO_VEN),2) as 'Venta Total', round(sum(rv.CAN_ART * rv.COS_VEN),2) as Costo, round((1 - (sum(rv.CAN_ART * rv.COS_VEN) / sum(rv.CAN_ART * rv.PCIO_VEN))) * 100, 2) as Porc from tblgralventas gv " +
					"inner join tblrenventas rv on gv.REF_DOC = rv.REF_DOC " +
					"inner join tblgpoarticulos ga on rv.COD1_ART = ga.COD1_ART " +
					"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where  (gv.FEC_DOC between '{parametroA}' and '{parametroB}') and ga.COD_GPO = 1 " +
					$"GROUP BY caa.DES_AGR " +
					$"ORDER BY Departamento ASC; " +
					$"select caa.DES_AGR as Departamento, round(sum(cos_uni*can_ren),2) as Merma " +
					$"from tblrenalmacen ren_alm " +
					$"inner join tblgralalmacen enc_alm on ren_alm.REF_MOV=enc_alm.REF_MOV " +
					$"inner join tblgpoarticulos ga on ren_alm.COD1_ART = ga.COD1_ART " +
					$"inner join tblcatagrupacionart caa on ga.COD_AGR = caa.COD_AGR " +
					$"where (enc_alm.FEC_MOV between'{parametroA}' and '{parametroB}') and enc_alm.cod_con='SMER' and caa.COD_GPO=1 and cod_sts=1 " +
					$"group by caa.des_agr;";
			}

			metodos.sendTables = SetearQuery;

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
			catch (Exception ex)
			{
				if (!ex.Message.ToLower().Contains("invoke"))
					MessageBox.Show(ex.Message);
			}
		}

		private void BtnExcel_Click(object sender, EventArgs e)
		{
			FrmYearSelection frmYearSelection = new FrmYearSelection();
			frmYearSelection.ShowDialog();
		}

		private async void BtnPDF_Click(object sender, EventArgs e)
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
			guardarArchivo.FileName = $"Ventas_{DateTime.Now:dd-MM-yy}.pdf";

			label4.Visible = true;
			await Task.Run(() => metodos.PrintReportInPDFVentas(parametroA, parametroB, guardarArchivo.FileName));
			label4.Visible = false;
			Process.Start(guardarArchivo.FileName);
		}

		private void BtnDesglosadoDiario_Click(object sender, EventArgs e)
		{
			string parametroA = FechaA.Value.ToString("yyyy-MM-dd");
			string parametroB = FechaB.Value.ToString("yyyy-MM-dd");

			metodos = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());

			DataTable ventaGeneral = metodos.GetQuery($@"select fec_doc as Fecha,
														sum(case when aux.COD_CAJ!=9 and aux.COD_FRP=1 then aux.IMP_MBA else 0 end) - SUM(CASE WHEN AUX.COD_CON = 'RCAJ' and aux.COD_CAJ!=9 THEN AUX.IMP_MBA ELSE 0 END) as Efectivo,
														sum(case when aux.COD_CAJ!=9 and aux.COD_FRP!=1 then aux.IMP_MBA else 0 end) as Terminal,
														sum(case when aux.COD_CAJ=9 then aux.IMP_MBA else 0 end) - SUM(CASE WHEN AUX.COD_CON = 'RCAJ' and aux.COD_CAJ=9 THEN AUX.IMP_MBA ELSE 0 END)  as Mayoreo
														from tblauxcaja aux
														where fec_doc between '{parametroA}' AND '{parametroB}'
														group by fec_doc
														order by fec_doc asc;");

			ClsGenerarExcel excel = new ClsGenerarExcel(ventaGeneral);

			excel.GenerarReporte();
		}
	}
}
