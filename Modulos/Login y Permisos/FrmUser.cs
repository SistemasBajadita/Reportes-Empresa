using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Reportes.Modulos.Login_y_Permisos
{
	public partial class FrmUser : Form
	{
		public FrmUser()
		{
			InitializeComponent();
		}

		public delegate void Evento(object sender, EventArgs e);
		public event Evento RefreshUsers;

		private void BtnRegistrar_Click(object sender, EventArgs e)
		{
			if (TxtPassword.Text != TxtConfirmacion.Text)
			{
				MessageBox.Show("Las contraseñas no coinciden", "OJO", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DialogResult resultado = MessageBox.Show("¿Estas seguro de registrar a este usuario?", "La Bajadita - Reportes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (resultado == DialogResult.Yes)
			{
				ClsLoginVerification login = new ClsLoginVerification(ConfigurationManager.ConnectionStrings["log"].ToString());

				string id = login.CrearUsuario(TxtName.Text, TxtPassword.Text);

				login.CrearRoles(id);

				RefreshUsers?.Invoke(sender, e);
				MessageBox.Show("A continuacion puedes gestionar los permisos del nuevo usuario creado", "La Bajadita - Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				TxtPassword.UseSystemPasswordChar = false;
				TxtConfirmacion.UseSystemPasswordChar = false;
			}
			else
			{
				TxtPassword.UseSystemPasswordChar = true;
				TxtConfirmacion.UseSystemPasswordChar = true;
			}
		}
	}
}
