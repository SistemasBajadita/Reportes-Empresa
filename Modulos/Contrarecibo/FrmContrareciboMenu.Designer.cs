namespace Reportes.Modulos.Contrarecibo
{
	partial class FrmContrareciboMenu
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
			this.BtnGenerar = new System.Windows.Forms.Button();
			this.BtnConsultar = new System.Windows.Forms.Button();
			this.BtnAplicar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnGenerar
			// 
			this.BtnGenerar.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnGenerar.Location = new System.Drawing.Point(82, 54);
			this.BtnGenerar.Name = "BtnGenerar";
			this.BtnGenerar.Size = new System.Drawing.Size(321, 58);
			this.BtnGenerar.TabIndex = 5;
			this.BtnGenerar.Text = "Generar";
			this.BtnGenerar.UseVisualStyleBackColor = true;
			// 
			// BtnConsultar
			// 
			this.BtnConsultar.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnConsultar.Location = new System.Drawing.Point(82, 118);
			this.BtnConsultar.Name = "BtnConsultar";
			this.BtnConsultar.Size = new System.Drawing.Size(321, 58);
			this.BtnConsultar.TabIndex = 6;
			this.BtnConsultar.Text = "Consultar";
			this.BtnConsultar.UseVisualStyleBackColor = true;
			// 
			// BtnAplicar
			// 
			this.BtnAplicar.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnAplicar.Location = new System.Drawing.Point(82, 182);
			this.BtnAplicar.Name = "BtnAplicar";
			this.BtnAplicar.Size = new System.Drawing.Size(321, 58);
			this.BtnAplicar.TabIndex = 7;
			this.BtnAplicar.Text = "Aplicar como pagado";
			this.BtnAplicar.UseVisualStyleBackColor = true;
			// 
			// FrmContrareciboMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(475, 299);
			this.Controls.Add(this.BtnAplicar);
			this.Controls.Add(this.BtnConsultar);
			this.Controls.Add(this.BtnGenerar);
			this.Name = "FrmContrareciboMenu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmContrareciboMenu";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnGenerar;
		private System.Windows.Forms.Button BtnConsultar;
		private System.Windows.Forms.Button BtnAplicar;
	}
}