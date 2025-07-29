using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos.Contrarecibo
{
	public partial class FrmReporte : Form
	{
		public FrmReporte()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private async void BtnShow_Click(object sender, EventArgs e)
		{
			ClsContrareciboOperaciones op = new ClsContrareciboOperaciones(ConfigurationManager.ConnectionStrings["servidor"].ToString());

			await op.GenerarPdfPagosDiarios(Fecha.Value);
		}
	}
}
