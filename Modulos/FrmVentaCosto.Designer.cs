namespace Reportes
{
	partial class FrmVentaCosto
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVentaCosto));
			this.label1 = new System.Windows.Forms.Label();
			this.BtnExcel = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.reporte = new System.Windows.Forms.DataGridView();
			this.BtnCorrerQuery = new System.Windows.Forms.Button();
			this.FechaB = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.FechaA = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
			this.BtnPDF = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.lblTotal = new System.Windows.Forms.Label();
			this.BtnDesglosadoDiario = new System.Windows.Forms.Button();
			this.chkGrafica = new System.Windows.Forms.CheckBox();
			this.chkTienda = new System.Windows.Forms.CheckBox();
			this.chkMayoreo = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.reporte)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label1.Location = new System.Drawing.Point(201, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(611, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "Reporte de venta con costos y margenes de utilidad";
			// 
			// BtnExcel
			// 
			this.BtnExcel.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnExcel.Location = new System.Drawing.Point(593, 539);
			this.BtnExcel.Name = "BtnExcel";
			this.BtnExcel.Size = new System.Drawing.Size(245, 40);
			this.BtnExcel.TabIndex = 16;
			this.BtnExcel.Text = "Ver comparativa por mes";
			this.BtnExcel.UseVisualStyleBackColor = true;
			this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.label4.Location = new System.Drawing.Point(589, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(350, 21);
			this.label4.TabIndex = 15;
			this.label4.Text = "Cargando reporte, por favor espere...";
			this.label4.Visible = false;
			// 
			// reporte
			// 
			this.reporte.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
			this.reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.reporte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.reporte.BackgroundColor = System.Drawing.Color.Linen;
			this.reporte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.reporte.DefaultCellStyle = dataGridViewCellStyle3;
			this.reporte.EnableHeadersVisualStyles = false;
			this.reporte.Location = new System.Drawing.Point(61, 198);
			this.reporte.MultiSelect = false;
			this.reporte.Name = "reporte";
			this.reporte.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.reporte.RowHeadersVisible = false;
			this.reporte.RowHeadersWidth = 51;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(229)))), ((int)(((byte)(116)))));
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
			this.reporte.RowsDefaultCellStyle = dataGridViewCellStyle5;
			this.reporte.RowTemplate.Height = 24;
			this.reporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.reporte.Size = new System.Drawing.Size(965, 304);
			this.reporte.TabIndex = 14;
			this.reporte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reporte_CellClick);
			// 
			// BtnCorrerQuery
			// 
			this.BtnCorrerQuery.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnCorrerQuery.FlatAppearance.BorderSize = 5;
			this.BtnCorrerQuery.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnCorrerQuery.Location = new System.Drawing.Point(439, 139);
			this.BtnCorrerQuery.Name = "BtnCorrerQuery";
			this.BtnCorrerQuery.Size = new System.Drawing.Size(144, 42);
			this.BtnCorrerQuery.TabIndex = 13;
			this.BtnCorrerQuery.Text = "Ver reporte";
			this.BtnCorrerQuery.UseVisualStyleBackColor = true;
			this.BtnCorrerQuery.Click += new System.EventHandler(this.BtnCorrerQuery_Click);
			// 
			// FechaB
			// 
			this.FechaB.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaB.Location = new System.Drawing.Point(649, 86);
			this.FechaB.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaB.Name = "FechaB";
			this.FechaB.Size = new System.Drawing.Size(141, 29);
			this.FechaB.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(623, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 21);
			this.label3.TabIndex = 11;
			this.label3.Text = "a";
			// 
			// FechaA
			// 
			this.FechaA.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaA.Location = new System.Drawing.Point(476, 86);
			this.FechaA.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaA.Name = "FechaA";
			this.FechaA.Size = new System.Drawing.Size(141, 29);
			this.FechaA.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(208, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(241, 21);
			this.label2.TabIndex = 9;
			this.label2.Text = "Ingresa el rango de fecha";
			// 
			// BtnPDF
			// 
			this.BtnPDF.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnPDF.Location = new System.Drawing.Point(390, 539);
			this.BtnPDF.Name = "BtnPDF";
			this.BtnPDF.Size = new System.Drawing.Size(197, 40);
			this.BtnPDF.TabIndex = 17;
			this.BtnPDF.Text = "Mandar a PDF";
			this.BtnPDF.UseVisualStyleBackColor = true;
			this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.label5.Location = new System.Drawing.Point(57, 513);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(207, 21);
			this.label5.TabIndex = 18;
			this.label5.Text = "Tiempo de respuesta:";
			this.label5.Visible = false;
			// 
			// lblTotal
			// 
			this.lblTotal.AutoSize = true;
			this.lblTotal.BackColor = System.Drawing.Color.Transparent;
			this.lblTotal.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.lblTotal.Location = new System.Drawing.Point(57, 164);
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Size = new System.Drawing.Size(0, 21);
			this.lblTotal.TabIndex = 20;
			// 
			// BtnDesglosadoDiario
			// 
			this.BtnDesglosadoDiario.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnDesglosadoDiario.Location = new System.Drawing.Point(844, 539);
			this.BtnDesglosadoDiario.Name = "BtnDesglosadoDiario";
			this.BtnDesglosadoDiario.Size = new System.Drawing.Size(197, 40);
			this.BtnDesglosadoDiario.TabIndex = 21;
			this.BtnDesglosadoDiario.Text = "Desglosado diario";
			this.BtnDesglosadoDiario.UseVisualStyleBackColor = true;
			this.BtnDesglosadoDiario.Click += new System.EventHandler(this.BtnDesglosadoDiario_Click);
			// 
			// chkGrafica
			// 
			this.chkGrafica.AutoSize = true;
			this.chkGrafica.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.chkGrafica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.chkGrafica.Location = new System.Drawing.Point(63, 126);
			this.chkGrafica.Name = "chkGrafica";
			this.chkGrafica.Size = new System.Drawing.Size(318, 25);
			this.chkGrafica.TabIndex = 22;
			this.chkGrafica.Text = "Imprimir grafica en desglosado";
			this.chkGrafica.UseVisualStyleBackColor = true;
			// 
			// chkTienda
			// 
			this.chkTienda.AutoSize = true;
			this.chkTienda.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.chkTienda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.chkTienda.Location = new System.Drawing.Point(593, 508);
			this.chkTienda.Name = "chkTienda";
			this.chkTienda.Size = new System.Drawing.Size(94, 25);
			this.chkTienda.TabIndex = 23;
			this.chkTienda.Text = "Tienda";
			this.chkTienda.UseVisualStyleBackColor = true;
			// 
			// chkMayoreo
			// 
			this.chkMayoreo.AutoSize = true;
			this.chkMayoreo.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.chkMayoreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			this.chkMayoreo.Location = new System.Drawing.Point(693, 508);
			this.chkMayoreo.Name = "chkMayoreo";
			this.chkMayoreo.Size = new System.Drawing.Size(109, 25);
			this.chkMayoreo.TabIndex = 24;
			this.chkMayoreo.Text = "Mayoreo";
			this.chkMayoreo.UseVisualStyleBackColor = true;
			// 
			// FrmVentaCosto
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(1065, 585);
			this.Controls.Add(this.chkMayoreo);
			this.Controls.Add(this.chkTienda);
			this.Controls.Add(this.chkGrafica);
			this.Controls.Add(this.BtnDesglosadoDiario);
			this.Controls.Add(this.lblTotal);
			this.Controls.Add(this.label5);
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
			this.Name = "FrmVentaCosto";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reporte de venta con costo y margenes de utilidad";
			this.Load += new System.EventHandler(this.FrmVentaCosto_Load);
			((System.ComponentModel.ISupportInitialize)(this.reporte)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnExcel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView reporte;
		private System.Windows.Forms.Button BtnCorrerQuery;
		private System.Windows.Forms.DateTimePicker FechaB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker FechaA;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SaveFileDialog guardarArchivo;
		private System.Windows.Forms.Button BtnPDF;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.Button BtnDesglosadoDiario;
		private System.Windows.Forms.CheckBox chkGrafica;
		private System.Windows.Forms.CheckBox chkTienda;
		private System.Windows.Forms.CheckBox chkMayoreo;
	}
}