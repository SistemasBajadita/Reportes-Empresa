using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmPrincipal : Form
	{
		public FrmPrincipal()
		{
			InitializeComponent();
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
	}
}
