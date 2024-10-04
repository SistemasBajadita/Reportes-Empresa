using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public partial class FrmLogin : Form
	{
		public FrmLogin()
		{
			InitializeComponent();

			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;

			this.Move += FrmLogin_Move;

			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private void FrmLogin_Move(object sender, EventArgs e)
		{
			this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
			this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
		}

		DataTable users;
		ClsConnection con;

		private void FrmLogin_Load(object sender, EventArgs e)
		{
			con = new ClsConnection(ConfigurationManager.ConnectionStrings["log"].ToString());
			users = con.GetQuery("select userid, name from users where active=1;");
			try
			{
				cbUsers.DataSource = users;
				cbUsers.ValueMember = "userid";
				cbUsers.DisplayMember = "name";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message+"\nLa aplicacion se cerrara, asegurate de estar conectado a la red de la empresa",
					"La Bajadita -  Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			cbUsers.Text = "";
		}

		private async void BtnLog_Click(object sender, EventArgs e)
		{
			ClsLoginVerification login = new ClsLoginVerification(ConfigurationManager.ConnectionStrings["log"].ToString());
			if (login.VerificarLogin(cbUsers.SelectedValue.ToString(), ClsLoginVerification.Encriptar(TxtPassword.Text)))
			{
				FrmPrincipal principal = new FrmPrincipal(cbUsers.SelectedValue.ToString());
				principal.Show();
				Hide();
			}
			else
			{
				lbMessage.Visible = true;
				await Task.Run(() => Thread.Sleep(3000));
				lbMessage.Visible = false;
			}
		}

		private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Verificar si la tecla presionada es la comilla simple
			if (e.KeyChar == '\'')
			{
				// Si es comilla simple, cancelar el evento para evitar que se ingrese
				e.Handled = true;
			}
		}

		private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (chkShowPassword.Checked)
			{
				TxtPassword.UseSystemPasswordChar = false;
				return;
			}
			TxtPassword.UseSystemPasswordChar = true;
		}

		private void FrmLogin_Paint(object sender, PaintEventArgs e)
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
