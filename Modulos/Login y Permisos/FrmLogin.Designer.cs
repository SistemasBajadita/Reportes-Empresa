namespace Reportes
{
	partial class FrmLogin
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtPassword = new System.Windows.Forms.TextBox();
			this.cbUsers = new System.Windows.Forms.ComboBox();
			this.BtnLog = new System.Windows.Forms.Button();
			this.lbMessage = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.chkShowPassword = new System.Windows.Forms.CheckBox();
			this.cmbEmpresa = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(40, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Usuario";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 140);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "Contraseña";
			// 
			// TxtPassword
			// 
			this.TxtPassword.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtPassword.Location = new System.Drawing.Point(153, 137);
			this.TxtPassword.Name = "TxtPassword";
			this.TxtPassword.Size = new System.Drawing.Size(254, 28);
			this.TxtPassword.TabIndex = 2;
			this.TxtPassword.UseSystemPasswordChar = true;
			this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
			this.TxtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPassword_KeyPress);
			// 
			// cbUsers
			// 
			this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbUsers.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbUsers.FormattingEnabled = true;
			this.cbUsers.Location = new System.Drawing.Point(153, 105);
			this.cbUsers.Name = "cbUsers";
			this.cbUsers.Size = new System.Drawing.Size(254, 27);
			this.cbUsers.TabIndex = 1;
			// 
			// BtnLog
			// 
			this.BtnLog.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnLog.Location = new System.Drawing.Point(188, 231);
			this.BtnLog.Name = "BtnLog";
			this.BtnLog.Size = new System.Drawing.Size(179, 49);
			this.BtnLog.TabIndex = 5;
			this.BtnLog.Text = "Ingresar";
			this.BtnLog.UseVisualStyleBackColor = true;
			this.BtnLog.Click += new System.EventHandler(this.BtnLog_Click);
			// 
			// lbMessage
			// 
			this.lbMessage.AutoSize = true;
			this.lbMessage.BackColor = System.Drawing.Color.LightCoral;
			this.lbMessage.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbMessage.ForeColor = System.Drawing.Color.Maroon;
			this.lbMessage.Location = new System.Drawing.Point(188, 191);
			this.lbMessage.Name = "lbMessage";
			this.lbMessage.Size = new System.Drawing.Size(192, 19);
			this.lbMessage.TabIndex = 6;
			this.lbMessage.Text = "Contraseña incorrecta";
			this.lbMessage.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(79, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(406, 26);
			this.label3.TabIndex = 7;
			this.label3.Text = "Reportes Generales - La Bajadita";
			// 
			// chkShowPassword
			// 
			this.chkShowPassword.AutoSize = true;
			this.chkShowPassword.BackColor = System.Drawing.Color.Transparent;
			this.chkShowPassword.Font = new System.Drawing.Font("Lucida Sans Typewriter", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkShowPassword.Location = new System.Drawing.Point(416, 142);
			this.chkShowPassword.Name = "chkShowPassword";
			this.chkShowPassword.Size = new System.Drawing.Size(93, 19);
			this.chkShowPassword.TabIndex = 8;
			this.chkShowPassword.Text = "Mostrar ";
			this.chkShowPassword.UseVisualStyleBackColor = false;
			this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
			// 
			// cmbEmpresa
			// 
			this.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbEmpresa.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbEmpresa.FormattingEnabled = true;
			this.cmbEmpresa.Items.AddRange(new object[] {
            "Jardines del Bosque",
            "Colinas del Yaqui"});
			this.cmbEmpresa.Location = new System.Drawing.Point(153, 72);
			this.cmbEmpresa.Name = "cmbEmpresa";
			this.cmbEmpresa.Size = new System.Drawing.Size(254, 27);
			this.cmbEmpresa.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(40, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 19);
			this.label4.TabIndex = 9;
			this.label4.Text = "Empresa";
			// 
			// FrmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(147)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(539, 296);
			this.Controls.Add(this.cmbEmpresa);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkShowPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbMessage);
			this.Controls.Add(this.BtnLog);
			this.Controls.Add(this.cbUsers);
			this.Controls.Add(this.TxtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FrmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Iniciar Sesion";
			this.Load += new System.EventHandler(this.FrmLogin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtPassword;
		private System.Windows.Forms.ComboBox cbUsers;
		private System.Windows.Forms.Button BtnLog;
		private System.Windows.Forms.Label lbMessage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkShowPassword;
		private System.Windows.Forms.ComboBox cmbEmpresa;
		private System.Windows.Forms.Label label4;
	}
}