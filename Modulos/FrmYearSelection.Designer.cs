namespace Reportes.Modulos
{
	partial class FrmYearSelection
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
			this.cbYears = new System.Windows.Forms.ComboBox();
			this.BtnGetExcel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label2.Location = new System.Drawing.Point(86, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(212, 26);
			this.label2.TabIndex = 2;
			this.label2.Text = "Seleccione el año";
			// 
			// cbYears
			// 
			this.cbYears.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbYears.FormattingEnabled = true;
			this.cbYears.Location = new System.Drawing.Point(106, 85);
			this.cbYears.Name = "cbYears";
			this.cbYears.Size = new System.Drawing.Size(170, 28);
			this.cbYears.TabIndex = 39;
			// 
			// BtnGetExcel
			// 
			this.BtnGetExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnGetExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnGetExcel.FlatAppearance.BorderSize = 5;
			this.BtnGetExcel.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnGetExcel.Location = new System.Drawing.Point(118, 147);
			this.BtnGetExcel.Name = "BtnGetExcel";
			this.BtnGetExcel.Size = new System.Drawing.Size(144, 42);
			this.BtnGetExcel.TabIndex = 40;
			this.BtnGetExcel.Text = "Abrir excel";
			this.BtnGetExcel.UseVisualStyleBackColor = true;
			this.BtnGetExcel.Click += new System.EventHandler(this.BtnGetExcel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(388, 258);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 41;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(176, 180);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 19);
			this.label1.TabIndex = 42;
			this.label1.Text = "0%";
			// 
			// FrmYearSelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Linen;
			this.ClientSize = new System.Drawing.Size(388, 258);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.BtnGetExcel);
			this.Controls.Add(this.cbYears);
			this.Controls.Add(this.label2);
			this.Name = "FrmYearSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ventas mensuales por departamento";
			this.Load += new System.EventHandler(this.FrmYearSelection_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbYears;
		private System.Windows.Forms.Button BtnGetExcel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
	}
}