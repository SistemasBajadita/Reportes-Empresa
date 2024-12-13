using Reportes.Modulos.Login_y_Permisos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Reportes
{
    public partial class FrmRoles : Form
    {
        public FrmRoles()
        {
            InitializeComponent();
            Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
        }

        List<CheckBox> chk;

        private void prueba_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            chk = new List<CheckBox>();
            ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["log"].ToString());

            DataTable columnas = con.GetQuery("DESCRIBE users_roles;");
            DataTable usuarios = con.GetQuery("select userid, name from users;");

            comboBox1.DataSource = usuarios;
            comboBox1.ValueMember = "userid";
            comboBox1.DisplayMember = "name";

            for (int i = 1; i < columnas.Rows.Count; i++)
            {
                chk.Add(new CheckBox());
                chk[i - 1].BackColor = Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(79)))), ((int)(((byte)(83)))));
                chk[i - 1].AutoSize = true;
                chk[i - 1].Font = new Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                chk[i - 1].ForeColor = Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(235)))), ((int)(((byte)(214)))));
                chk[i - 1].Name = columnas.Rows[i][0].ToString();
                chk[i - 1].Text = columnas.Rows[i][0].ToString();
                flowLayoutPanel1.Controls.Add(chk[i - 1]);
            }
            comboBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedValue.ToString().Contains("System.Data.DataRowView"))
            {
                ClsConnection con = new ClsConnection(ConfigurationManager.ConnectionStrings["log"].ToString());
                for (int i = 0; i < chk.Count; i++)
                {
                    string enable = con.GetScalar($"select {chk[i].Name} from users_roles where userid={comboBox1.SelectedValue}");
                    if (enable == "True")
                        chk[i].Checked = true;
                    else
                        chk[i].Checked = false;
                }

                var state = con.GetScalar($"select active from users where userid={comboBox1.SelectedValue}");
                if (state == "True")
                {
                    BtnDesactivarUsuario.Text = "Deshabilitar usuario";
                    BtnDesactivarUsuario.BackColor = Color.Red;
                    BtnDesactivarUsuario.ForeColor = Color.White;
                }
                else if (state == "False")
                {
                    BtnDesactivarUsuario.Text = "Habilitar usuario";
                    BtnDesactivarUsuario.BackColor = Color.Green;
                    BtnDesactivarUsuario.ForeColor = Color.White;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ClsLoginVerification login = new ClsLoginVerification(ConfigurationManager.ConnectionStrings["log"].ToString());
            if (TxtPassword.Text != "")
            {
                login.CambiarPassword(comboBox1.SelectedValue.ToString(), TxtPassword.Text);
            }

            for (int i = 0; i < chk.Count; i++)
            {
                login.GuardarConfig(comboBox1.SelectedValue.ToString(), chk[i].Name, chk[i].Checked == true ? "1" : "0");
            }
            MessageBox.Show("Configuracion guardada.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnNewUser_Click(object sender, EventArgs e)
        {
            FrmUser newUser = new FrmUser();
            newUser.RefreshUsers += RefreshUsers;
            newUser.ShowDialog();
        }

        private void RefreshUsers(object sender, EventArgs e)
        {
            prueba_Load(sender, e);
        }

        private void BtnEliminarUsuario_Click(object sender, EventArgs e)
        {
            ClsLoginVerification cls = new ClsLoginVerification(ConfigurationManager.ConnectionStrings["log"].ConnectionString);
            if (BtnDesactivarUsuario.BackColor == Color.Red)
            {
                cls.DeshabilitarHabilitarUsuario(comboBox1.SelectedValue.ToString(), "0");
                MessageBox.Show("Usuario deshabilitado", "La Bajadita - Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (BtnDesactivarUsuario.BackColor == Color.Green)
            {
                cls.DeshabilitarHabilitarUsuario(comboBox1.SelectedValue.ToString(), "1");
                MessageBox.Show("Usuario habilitado", "La Bajadita - Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void FrmRoles_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
