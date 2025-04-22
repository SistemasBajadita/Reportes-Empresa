using System;
using System.Configuration;
using System.Data;
using System.Drawing;
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
			users = con.GetQuery("select userid, name from users where active=1 order by name asc;");
			try
			{
				cbUsers.DataSource = users;
				cbUsers.ValueMember = "userid";
				cbUsers.DisplayMember = "name";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\nLa aplicacion se cerrara, asegurate de estar conectado a la red de la empresa",
					"La Bajadita -  Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			cmbEmpresa.SelectedIndex = 0;
			cbUsers.Text = "";
		}

		private async void BtnLog_Click(object sender, EventArgs e)
		{
			ClsLoginVerification login = new ClsLoginVerification(ConfigurationManager.ConnectionStrings["log"].ToString());
			if (login.VerificarLogin(cbUsers.SelectedValue.ToString(), ClsLoginVerification.Encriptar(TxtPassword.Text)))
			{
				Program.Empresa = cmbEmpresa.SelectedIndex;
				FrmPrincipal principal = new FrmPrincipal(cbUsers.SelectedValue.ToString());
				principal.Show();
				Hide();
			}
			else
			{
				lbMessage.Visible = true;
				await Task.Delay(3000);
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

		}

		private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				BtnLog_Click(sender, new EventArgs());
			}
		}
	}
}
