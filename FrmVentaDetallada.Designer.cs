namespace Reportes
{
	partial class FrmVentaDetallada
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label5 = new System.Windows.Forms.Label();
			this.cbDepartamentos = new System.Windows.Forms.ComboBox();
			this.BtnPDF = new System.Windows.Forms.Button();
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
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(143, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(138, 21);
			this.label5.TabIndex = 52;
			this.label5.Text = "Departamento";
			// 
			// cbDepartamentos
			// 
			this.cbDepartamentos.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbDepartamentos.FormattingEnabled = true;
			this.cbDepartamentos.Location = new System.Drawing.Point(301, 152);
			this.cbDepartamentos.Name = "cbDepartamentos";
			this.cbDepartamentos.Size = new System.Drawing.Size(302, 25);
			this.cbDepartamentos.TabIndex = 51;
			// 
			// BtnPDF
			// 
			this.BtnPDF.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnPDF.Location = new System.Drawing.Point(826, 541);
			this.BtnPDF.Name = "BtnPDF";
			this.BtnPDF.Size = new System.Drawing.Size(197, 30);
			this.BtnPDF.TabIndex = 50;
			this.BtnPDF.Text = "Mandar a PDF";
			this.BtnPDF.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.label4.Location = new System.Drawing.Point(759, 159);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(193, 21);
			this.label4.TabIndex = 49;
			this.label4.Text = "Cargando reporte ...";
			this.label4.Visible = false;
			// 
			// reporte
			// 
			this.reporte.AllowUserToAddRows = false;
			this.reporte.AllowUserToDeleteRows = false;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			dataGridViewCellStyle16.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Navy;
			this.reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
			this.reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.reporte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.reporte.BackgroundColor = System.Drawing.Color.Linen;
			this.reporte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
			this.reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.reporte.DefaultCellStyle = dataGridViewCellStyle18;
			this.reporte.EnableHeadersVisualStyles = false;
			this.reporte.Location = new System.Drawing.Point(37, 215);
			this.reporte.MultiSelect = false;
			this.reporte.Name = "reporte";
			this.reporte.ReadOnly = true;
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
			this.reporte.RowHeadersVisible = false;
			this.reporte.RowHeadersWidth = 51;
			dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(229)))), ((int)(((byte)(116)))));
			dataGridViewCellStyle20.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Navy;
			this.reporte.RowsDefaultCellStyle = dataGridViewCellStyle20;
			this.reporte.RowTemplate.Height = 24;
			this.reporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.reporte.Size = new System.Drawing.Size(986, 311);
			this.reporte.TabIndex = 48;
			// 
			// BtnCorrerQuery
			// 
			this.BtnCorrerQuery.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnCorrerQuery.FlatAppearance.BorderSize = 5;
			this.BtnCorrerQuery.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnCorrerQuery.Location = new System.Drawing.Point(609, 142);
			this.BtnCorrerQuery.Name = "BtnCorrerQuery";
			this.BtnCorrerQuery.Size = new System.Drawing.Size(144, 42);
			this.BtnCorrerQuery.TabIndex = 47;
			this.BtnCorrerQuery.Text = "Ver reporte";
			this.BtnCorrerQuery.UseVisualStyleBackColor = true;
			this.BtnCorrerQuery.Click += new System.EventHandler(this.BtnCorrerQuery_Click);
			// 
			// FechaB
			// 
			this.FechaB.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaB.Location = new System.Drawing.Point(683, 89);
			this.FechaB.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaB.Name = "FechaB";
			this.FechaB.Size = new System.Drawing.Size(141, 29);
			this.FechaB.TabIndex = 46;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(657, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 21);
			this.label3.TabIndex = 45;
			this.label3.Text = "a";
			// 
			// FechaA
			// 
			this.FechaA.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaA.Location = new System.Drawing.Point(510, 89);
			this.FechaA.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaA.Name = "FechaA";
			this.FechaA.Size = new System.Drawing.Size(141, 29);
			this.FechaA.TabIndex = 44;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(247, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(241, 21);
			this.label2.TabIndex = 43;
			this.label2.Text = "Ingresa el rango de fecha";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label1.Location = new System.Drawing.Point(242, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(582, 26);
			this.label1.TabIndex = 42;
			this.label1.Text = "Reporte de utilidad por producto y departamento";
			// 
			// FrmVentaDetallada
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(1056, 591);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbDepartamentos);
			this.Controls.Add(this.BtnPDF);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.reporte);
			this.Controls.Add(this.BtnCorrerQuery);
			this.Controls.Add(this.FechaB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.FechaA);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FrmVentaDetallada";
			this.Text = "FrmVentaDetallada";
			this.Load += new System.EventHandler(this.FrmVentaDetallada_Load);
			((System.ComponentModel.ISupportInitialize)(this.reporte)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbDepartamentos;
		private System.Windows.Forms.Button BtnPDF;
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