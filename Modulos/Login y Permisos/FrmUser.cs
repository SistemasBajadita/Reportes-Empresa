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
using System.Drawing.Drawing2D;

namespace Reportes.Modulos.Login_y_Permisos
{
	public partial class FrmUser : Form
	{
		public FrmUser()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
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

        private void FrmUser_Paint(object sender, PaintEventArgs e)
        {
            // Crear un rectángulo que cubra todo el formulario
            Rectangle rect = this.ClientRectangle;

            // Definir los colores del degradado (por ejemplo, de azul a blanco)
            Color color1 = Color.FromArgb(251, 147, 60); //--original
            Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original
                                                                //Color color1 = ColorTranslator.FromHtml("#0C1A47");
                                                                //Color color2 = ColorTranslator.FromHtml("#EC3F5D");

            // Crear un pincel con un degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.BackwardDiagonal))
            {
                // Dibujar el degradado en el fondo del formulario
                e.Graphics.FillRectangle(brush, rect);
            }
        }
    }
}
