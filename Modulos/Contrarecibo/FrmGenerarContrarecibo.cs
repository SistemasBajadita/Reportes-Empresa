using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos.Contrarecibo
{
	public partial class FrmGenerarContrarecibo : Form
	{

		ClsConnection con;

		public FrmGenerarContrarecibo()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}


		private void BtnSelect_Click(object sender, EventArgs e)
		{
			FrmSelectProv frm = new FrmSelectProv();
			frm.sendId = RecibirID;
			frm.ShowDialog();
		}

		private void RecibirID(string obj)
		{
			reporte.Rows.Clear();

			TxtIDProv.Text = obj;
			string nombreProveedor = con.GetScalar($"select nom_prov from tblcatproveedor where cod_prov='{obj}'");
			lblNombreProv.Text = nombreProveedor;

			DataTable recepciones = con.GetQuery($@"select enc.fol_doc as Folio, replace(enc.FEC_DOC, '-', '/') as Fecha, enc.SUB_DOC+enc.IVA_DOC - coalesce(dev_enc.tot_dev,0) as Total 
													from tblcomprasenc enc
													left join tbldevolucionenc dev_enc on dev_enc.FOL_REC=enc.FOL_DOC
													inner join contrarecibo.folios fols on fols.fol=enc.FOL_DOC
													where enc.COD_PROV='{obj}';;");

			foreach (DataRow r in recepciones.Rows)
			{
				reporte.Rows.Add(false, r[0].ToString(), r[1].ToString(), r[2].ToString());
			}

		}

		private void FrmGenerarContrarecibo_Load(object sender, EventArgs e)
		{
			Fecha.MinDate = DateTime.Now;

			con = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);

			DataTable metodos = con.GetQuery("SELECT cod_frp as codigo, des_frp as descripcion FROM tblformaspago;");

			cbOP.DataSource = metodos;
			cbOP.ValueMember = "codigo";
			cbOP.DisplayMember = "descripcion";

		}

		private async void BtnGenerar_Click(object sender, EventArgs e)
		{
			if (reporte.Rows.Count == 0)
			{
				MessageBox.Show("El proveedor que seleccionaste no tiene notas recientes o no seleccionaste ningun proveedor",
					"OJO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			bool AlMenosUnoMarcado = false;

			foreach (DataGridViewRow row in reporte.Rows)
			{
				if (!row.IsNewRow)
				{
					var valor = Convert.ToBoolean(row.Cells[0].Value);
					if (valor)
					{
						AlMenosUnoMarcado = true;
						break;
					}
				}
			}

			if (!AlMenosUnoMarcado)
			{
				MessageBox.Show("Debes seleccionar al menos un folio para generar un contrarecibo");
				return;
			}


			ClsContrareciboOperaciones operaciones = new ClsContrareciboOperaciones(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);

			List<string> folios = new List<string>();

			decimal total = 0;

			foreach (DataGridViewRow r in reporte.Rows)
			{
				if (bool.Parse(r.Cells[0].Value.ToString()) == true)
				{
					folios.Add(r.Cells[1].Value.ToString());

					total += Convert.ToDecimal(r.Cells[3].Value);
				}
			}

			bool resultado = await operaciones.GenerarContrarecibo(folios, Fecha.Value, TxtIDProv.Text, total);

			if (resultado)
			{
				reporte.Rows.Clear();
				TxtIDProv.Text = "";
				lblNombreProv.Text = "";
			}
		}
	}
}
