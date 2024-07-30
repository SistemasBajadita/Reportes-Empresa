namespace Reportes
{
	partial class FrmTickets
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
			this.BtnPrintReport = new System.Windows.Forms.Button();
			this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbVendedor = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.BtnAllTickets = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// BtnPrintReport
			// 
			this.BtnPrintReport.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnPrintReport.Location = new System.Drawing.Point(90, 225);
			this.BtnPrintReport.Name = "BtnPrintReport";
			this.BtnPrintReport.Size = new System.Drawing.Size(313, 71);
			this.BtnPrintReport.TabIndex = 13;
			this.BtnPrintReport.Text = "Imprimir venta de chofer";
			this.BtnPrintReport.UseVisualStyleBackColor = true;
			this.BtnPrintReport.Click += new System.EventHandler(this.BtnPrintReport_Click);
			// 
			// dateTimePicker4
			// 
			this.dateTimePicker4.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker4.Location = new System.Drawing.Point(625, 135);
			this.dateTimePicker4.Name = "dateTimePicker4";
			this.dateTimePicker4.Size = new System.Drawing.Size(123, 28);
			this.dateTimePicker4.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(598, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(21, 23);
			this.label4.TabIndex = 11;
			this.label4.Text = "a";
			// 
			// dateTimePicker3
			// 
			this.dateTimePicker3.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker3.Location = new System.Drawing.Point(469, 135);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.Size = new System.Drawing.Size(123, 28);
			this.dateTimePicker3.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(398, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Fecha";
			// 
			// cmbVendedor
			// 
			this.cmbVendedor.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbVendedor.FormattingEnabled = true;
			this.cmbVendedor.Location = new System.Drawing.Point(130, 135);
			this.cmbVendedor.Name = "cmbVendedor";
			this.cmbVendedor.Size = new System.Drawing.Size(251, 29);
			this.cmbVendedor.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(27, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Vendedor";
			// 
			// BtnAllTickets
			// 
			this.BtnAllTickets.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAllTickets.Location = new System.Drawing.Point(423, 225);
			this.BtnAllTickets.Name = "BtnAllTickets";
			this.BtnAllTickets.Size = new System.Drawing.Size(313, 71);
			this.BtnAllTickets.TabIndex = 14;
			this.BtnAllTickets.Text = "Imprimir todas las ventas";
			this.BtnAllTickets.UseVisualStyleBackColor = true;
			this.BtnAllTickets.Click += new System.EventHandler(this.BtnAllTickets_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(1, 1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(794, 448);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			// 
			// FrmTickets
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Linen;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.BtnAllTickets);
			this.Controls.Add(this.BtnPrintReport);
			this.Controls.Add(this.dateTimePicker4);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePicker3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmbVendedor);
			this.Controls.Add(this.label2);
			this.Name = "FrmTickets";
			this.Text = "Tickets por chofer";
			this.Load += new System.EventHandler(this.FrmTickets_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnPrintReport;
		private System.Windows.Forms.DateTimePicker dateTimePicker4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePicker3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cmbVendedor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnAllTickets;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}