namespace Reportes.Modulos.Login_y_Permisos
{
	partial class FrmUser
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
			this.TxtPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtConfirmacion = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.TxtName = new System.Windows.Forms.TextBox();
			this.BtnRegistrar = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// TxtPassword
			// 
			this.TxtPassword.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtPassword.Location = new System.Drawing.Point(144, 110);
			this.TxtPassword.Name = "TxtPassword";
			this.TxtPassword.Size = new System.Drawing.Size(197, 28);
			this.TxtPassword.TabIndex = 2;
			this.TxtPassword.UseSystemPasswordChar = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(30, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Contraseña";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(70, 84);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Nombre";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(35, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(373, 32);
			this.label3.TabIndex = 7;
			this.label3.Text = "Registrar nuevo usuario";
			// 
			// TxtConfirmacion
			// 
			this.TxtConfirmacion.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtConfirmacion.Location = new System.Drawing.Point(144, 144);
			this.TxtConfirmacion.Name = "TxtConfirmacion";
			this.TxtConfirmacion.Size = new System.Drawing.Size(197, 28);
			this.TxtConfirmacion.TabIndex = 3;
			this.TxtConfirmacion.UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(40, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Confirmar";
			// 
			// TxtName
			// 
			this.TxtName.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtName.Location = new System.Drawing.Point(144, 78);
			this.TxtName.Name = "TxtName";
			this.TxtName.Size = new System.Drawing.Size(197, 28);
			this.TxtName.TabIndex = 1;
			// 
			// BtnRegistrar
			// 
			this.BtnRegistrar.Font = new System.Drawing.Font("Lucida Fax", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnRegistrar.Location = new System.Drawing.Point(62, 209);
			this.BtnRegistrar.Name = "BtnRegistrar";
			this.BtnRegistrar.Size = new System.Drawing.Size(298, 59);
			this.BtnRegistrar.TabIndex = 5;
			this.BtnRegistrar.Text = "Registrar usuario";
			this.BtnRegistrar.UseVisualStyleBackColor = true;
			this.BtnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.BackColor = System.Drawing.Color.Transparent;
			this.checkBox1.Font = new System.Drawing.Font("Lucida Sans", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBox1.Location = new System.Drawing.Point(198, 179);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(144, 19);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "Mostrar contraseña";
			this.checkBox1.UseVisualStyleBackColor = false;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// FrmUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(147)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(431, 280);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.BtnRegistrar);
			this.Controls.Add(this.TxtName);
			this.Controls.Add(this.TxtConfirmacion);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TxtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FrmUser";
			this.Text = "La Bajadita - Crear Usuario";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox TxtPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TxtConfirmacion;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox TxtName;
		private System.Windows.Forms.Button BtnRegistrar;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}