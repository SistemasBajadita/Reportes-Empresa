namespace Reportes
{
	partial class FrmTopProductos
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTopProductos));
			this.BtnPDF = new System.Windows.Forms.Button();
			this.BtnExcel = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.reporte = new System.Windows.Forms.DataGridView();
			this.BtnCorrerQuery = new System.Windows.Forms.Button();
			this.FechaB = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.FechaA = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.reporte)).BeginInit();
			this.SuspendLayout();
			// 
			// BtnPDF
			// 
			this.BtnPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnPDF.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnPDF.Location = new System.Drawing.Point(339, 466);
			this.BtnPDF.Name = "BtnPDF";
			this.BtnPDF.Size = new System.Drawing.Size(197, 30);
			this.BtnPDF.TabIndex = 37;
			this.BtnPDF.Text = "Mandar a PDF";
			this.BtnPDF.UseVisualStyleBackColor = true;
			// 
			// BtnExcel
			// 
			this.BtnExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnExcel.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnExcel.Location = new System.Drawing.Point(539, 466);
			this.BtnExcel.Name = "BtnExcel";
			this.BtnExcel.Size = new System.Drawing.Size(197, 30);
			this.BtnExcel.TabIndex = 36;
			this.BtnExcel.Text = "Mandar a Excel";
			this.BtnExcel.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.label4.Location = new System.Drawing.Point(535, 155);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(193, 21);
			this.label4.TabIndex = 35;
			this.label4.Text = "Cargando reporte ...";
			this.label4.Visible = false;
			// 
			// reporte
			// 
			this.reporte.AllowUserToAddRows = false;
			this.reporte.AllowUserToDeleteRows = false;
			dataGridViewCellStyle26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			dataGridViewCellStyle26.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle26.ForeColor = System.Drawing.Color.Navy;
			this.reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle26;
			this.reporte.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.reporte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.reporte.BackgroundColor = System.Drawing.Color.Linen;
			this.reporte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle27.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
			this.reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.reporte.DefaultCellStyle = dataGridViewCellStyle28;
			this.reporte.EnableHeadersVisualStyles = false;
			this.reporte.Location = new System.Drawing.Point(45, 189);
			this.reporte.MultiSelect = false;
			this.reporte.Name = "reporte";
			this.reporte.ReadOnly = true;
			dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle29.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
			this.reporte.RowHeadersVisible = false;
			this.reporte.RowHeadersWidth = 51;
			dataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(229)))), ((int)(((byte)(116)))));
			dataGridViewCellStyle30.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle30.ForeColor = System.Drawing.Color.Navy;
			this.reporte.RowsDefaultCellStyle = dataGridViewCellStyle30;
			this.reporte.RowTemplate.Height = 24;
			this.reporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.reporte.Size = new System.Drawing.Size(817, 271);
			this.reporte.TabIndex = 34;
			// 
			// BtnCorrerQuery
			// 
			this.BtnCorrerQuery.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnCorrerQuery.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnCorrerQuery.FlatAppearance.BorderSize = 5;
			this.BtnCorrerQuery.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnCorrerQuery.Location = new System.Drawing.Point(385, 138);
			this.BtnCorrerQuery.Name = "BtnCorrerQuery";
			this.BtnCorrerQuery.Size = new System.Drawing.Size(144, 42);
			this.BtnCorrerQuery.TabIndex = 33;
			this.BtnCorrerQuery.Text = "Ver reporte";
			this.BtnCorrerQuery.UseVisualStyleBackColor = true;
			// 
			// FechaB
			// 
			this.FechaB.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.FechaB.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaB.Location = new System.Drawing.Point(568, 85);
			this.FechaB.Name = "FechaB";
			this.FechaB.Size = new System.Drawing.Size(141, 29);
			this.FechaB.TabIndex = 32;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(542, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 21);
			this.label3.TabIndex = 31;
			this.label3.Text = "a";
			// 
			// FechaA
			// 
			this.FechaA.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.FechaA.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaA.Location = new System.Drawing.Point(395, 85);
			this.FechaA.Name = "FechaA";
			this.FechaA.Size = new System.Drawing.Size(141, 29);
			this.FechaA.TabIndex = 30;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(127, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(241, 21);
			this.label2.TabIndex = 29;
			this.label2.Text = "Ingresa el rango de fecha";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label1.Location = new System.Drawing.Point(151, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(642, 26);
			this.label1.TabIndex = 28;
			this.label1.Text = "Reporte de productos mas vendidos por departamento";
			// 
			// FrmTopProductos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(906, 532);
			this.Controls.Add(this.BtnPDF);
			this.Controls.Add(this.BtnExcel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.reporte);
			this.Controls.Add(this.BtnCorrerQuery);
			this.Controls.Add(this.FechaB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.FechaA);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmTopProductos";
			this.Text = "Top de Productos";
			((System.ComponentModel.ISupportInitialize)(this.reporte)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnPDF;
		private System.Windows.Forms.Button BtnExcel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView reporte;
		private System.Windows.Forms.Button BtnCorrerQuery;
		private System.Windows.Forms.DateTimePicker FechaB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker FechaA;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}