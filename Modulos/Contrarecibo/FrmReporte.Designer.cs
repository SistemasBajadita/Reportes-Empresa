﻿namespace Reportes.Modulos.Contrarecibo
{
	partial class FrmReporte
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Fecha = new System.Windows.Forms.DateTimePicker();
			this.BtnShow = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(161, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(219, 32);
			this.label2.TabIndex = 44;
			this.label2.Text = "Reporte diario";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(151, 129);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 23);
			this.label1.TabIndex = 45;
			this.label1.Text = "Fecha";
			// 
			// Fecha
			// 
			this.Fecha.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.Fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Fecha.Location = new System.Drawing.Point(242, 125);
			this.Fecha.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.Fecha.Name = "Fecha";
			this.Fecha.Size = new System.Drawing.Size(164, 29);
			this.Fecha.TabIndex = 57;
			// 
			// BtnShow
			// 
			this.BtnShow.Font = new System.Drawing.Font("Lucida Fax", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnShow.Location = new System.Drawing.Point(193, 189);
			this.BtnShow.Name = "BtnShow";
			this.BtnShow.Size = new System.Drawing.Size(174, 52);
			this.BtnShow.TabIndex = 60;
			this.BtnShow.Text = "Ver";
			this.BtnShow.UseVisualStyleBackColor = true;
			this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
			// 
			// FrmReporte
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(556, 275);
			this.Controls.Add(this.BtnShow);
			this.Controls.Add(this.Fecha);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Name = "FrmReporte";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmReporte";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker Fecha;
		private System.Windows.Forms.Button BtnShow;
	}
}