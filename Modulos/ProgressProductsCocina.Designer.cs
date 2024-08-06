namespace Reportes.Modulos
{
	partial class ProgressProductsCocina
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
			this.Progreso = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Progreso
			// 
			this.Progreso.Location = new System.Drawing.Point(56, 107);
			this.Progreso.Name = "Progreso";
			this.Progreso.Size = new System.Drawing.Size(486, 34);
			this.Progreso.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label1.Location = new System.Drawing.Point(97, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(398, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "Verificando precios de productos";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(66, 159);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 20);
			this.label2.TabIndex = 2;
			// 
			// ProgressProductsCocina
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 255);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Progreso);
			this.Name = "ProgressProductsCocina";
			this.Text = "La Bajadita ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar Progreso;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}