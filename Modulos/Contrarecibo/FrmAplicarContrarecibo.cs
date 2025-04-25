using System;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos.Contrarecibo
{
	public partial class FrmAplicarContrarecibo : Form
	{
		ClsContrareciboOperaciones cr;

		public FrmAplicarContrarecibo()
		{
			InitializeComponent();
			cr = new ClsContrareciboOperaciones(ConfigurationManager.ConnectionStrings["servidor"].ConnectionString);
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private async void FrmAplicarContrarecibo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				string[] result = await cr.ObtenerDatosContrarecibo(int.Parse(TxtFolio.Text));

				if (result == null)
				{
					lblProveedor.Text = "Proveedor: NO ENCONTRADO";
					lblFecha.Text = "Monto a pagar: NO ENCONTRADO";
					lblMonto.Text = "Fecha de pago: NO ENCONTRADO";
					return;
				}



				lblProveedor.Text = "Proveedor: " + result[0];
				lblFecha.Text = "Monto a pagar: " + result[2];
				lblMonto.Text = "Fecha de pago: " + result[1].Split(' ')[0];

				if (result[3] == "1")
				{
					lblEstatus.Text = "NO PAGADO";
				}
				else
				{
					BtnSeleccionar.Enabled = false;
					lblEstatus.Text = "PAGADO";
					await Task.Delay(3000);

					TxtFolio.Text = "";
					lblProveedor.Text = "Fecha de pago:";
					lblFecha.Text = "Fecha de pago:";
					lblMonto.Text = "Monto a pagar:";
					lblEstatus.Text = "";
					BtnSeleccionar.Enabled = true;
				}
			}
		}

		private async void BtnSeleccionar_Click(object sender, EventArgs e)
		{
			await cr.AplicarComoPagado(int.Parse(TxtFolio.Text));
			lblProveedor.Text = "Proveedor: ";
			lblFecha.Text = "Monto a pagar: ";
			lblMonto.Text = "Fecha de pago: ";
		}
	}
}
