using System;
using System.Windows.Forms;

namespace Reportes.Modulos.Contrarecibo
{
	public partial class FrmContrareciboMenu : Form
	{
		public FrmContrareciboMenu()
		{
			InitializeComponent();
		}

		private void BtnGenerar_Click(object sender, EventArgs e)
		{
			FrmGenerarContrarecibo frm = new FrmGenerarContrarecibo();
			frm.ShowDialog();
		}

		private void BtnConsultar_Click(object sender, EventArgs e)
		{
			FrmConsultarContrarecibo frm = new FrmConsultarContrarecibo();
			frm.ShowDialog();
		}

		private void BtnAplicar_Click(object sender, EventArgs e)
		{
			FrmAplicarContrarecibo frm = new FrmAplicarContrarecibo();
			frm.ShowDialog();
		}

		private void BtnDiario_Click(object sender, EventArgs e)
		{
			FrmReporte r = new FrmReporte();
			r.ShowDialog();
		}
	}
}
