namespace Reportes
{
	partial class FrmRoles
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.BtnSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtPassword = new System.Windows.Forms.TextBox();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.label3 = new System.Windows.Forms.Label();
			this.BtnNewUser = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.BtnDesactivarUsuario = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 168);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(491, 259);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("Lucida Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(93, 51);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(328, 29);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// BtnSave
			// 
			this.BtnSave.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnSave.Location = new System.Drawing.Point(27, 438);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(465, 40);
			this.BtnSave.TabIndex = 39;
			this.BtnSave.Text = "Guardar Configuracion";
			this.BtnSave.UseVisualStyleBackColor = true;
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(56, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(405, 26);
			this.label1.TabIndex = 40;
			this.label1.Text = "Contraseña y permisos de usuario";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 99);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(157, 19);
			this.label2.TabIndex = 41;
			this.label2.Text = "Contraseña nueva";
			// 
			// TxtPassword
			// 
			this.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.helpProvider1.SetHelpKeyword(this.TxtPassword, "");
			this.helpProvider1.SetHelpString(this.TxtPassword, "Dejala vacia si no quieres cambiar la contraseña");
			this.TxtPassword.Location = new System.Drawing.Point(196, 98);
			this.TxtPassword.Name = "TxtPassword";
			this.helpProvider1.SetShowHelp(this.TxtPassword, true);
			this.TxtPassword.Size = new System.Drawing.Size(307, 22);
			this.TxtPassword.TabIndex = 42;
			this.TxtPassword.UseSystemPasswordChar = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Lucida Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(116, 123);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(317, 15);
			this.label3.TabIndex = 43;
			this.label3.Text = "*dejalo vacio si no quieres cambiar la contraseña";
			// 
			// BtnNewUser
			// 
			this.BtnNewUser.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnNewUser.Location = new System.Drawing.Point(27, 531);
			this.BtnNewUser.Name = "BtnNewUser";
			this.BtnNewUser.Size = new System.Drawing.Size(465, 40);
			this.BtnNewUser.TabIndex = 44;
			this.BtnNewUser.Text = "Crear nuevo usuario";
			this.BtnNewUser.UseVisualStyleBackColor = true;
			this.BtnNewUser.Click += new System.EventHandler(this.BtnNewUser_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Lucida Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(116, 140);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(346, 15);
			this.label4.TabIndex = 45;
			this.label4.Text = "*la casilla super indica si el usuario gestiona permisos";
			// 
			// BtnDesactivarUsuario
			// 
			this.BtnDesactivarUsuario.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnDesactivarUsuario.Location = new System.Drawing.Point(27, 484);
			this.BtnDesactivarUsuario.Name = "BtnDesactivarUsuario";
			this.BtnDesactivarUsuario.Size = new System.Drawing.Size(465, 40);
			this.BtnDesactivarUsuario.TabIndex = 46;
			this.BtnDesactivarUsuario.Text = "Deshabilitar Usuario";
			this.BtnDesactivarUsuario.UseVisualStyleBackColor = true;
			this.BtnDesactivarUsuario.Click += new System.EventHandler(this.BtnEliminarUsuario_Click);
			// 
			// FrmRoles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(147)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(519, 583);
			this.Controls.Add(this.BtnDesactivarUsuario);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.BtnNewUser);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TxtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtnSave);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "FrmRoles";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Accesos y contraseñas";
			this.Load += new System.EventHandler(this.prueba_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmRoles_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtPassword;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BtnNewUser;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button BtnDesactivarUsuario;
	}
}