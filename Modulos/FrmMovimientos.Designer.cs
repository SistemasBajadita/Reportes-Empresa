namespace Reportes
{
	partial class FrmMovimientos
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.cbConceptos = new System.Windows.Forms.ComboBox();
			this.BtnPDF = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.reporte = new System.Windows.Forms.DataGridView();
			this.BtnCorrerQuery = new System.Windows.Forms.Button();
			this.FechaB = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.FechaA = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.cbDepartamentos = new System.Windows.Forms.ComboBox();
			this.cbConceptos2 = new System.Windows.Forms.ComboBox();
			this.BtnPDFDetails = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.reporte2 = new System.Windows.Forms.DataGridView();
			this.BtnCorrerQueryArticulos = new System.Windows.Forms.Button();
			this.FechaB2 = new System.Windows.Forms.DateTimePicker();
			this.label6 = new System.Windows.Forms.Label();
			this.FechaA2 = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
			this.help = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.reporte)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.reporte2)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(859, 612);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.cbConceptos);
			this.tabPage1.Controls.Add(this.BtnPDF);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.reporte);
			this.tabPage1.Controls.Add(this.BtnCorrerQuery);
			this.tabPage1.Controls.Add(this.FechaB);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.FechaA);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 29);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(851, 579);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Movimentos por departamento";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// cbConceptos
			// 
			this.cbConceptos.FormattingEnabled = true;
			this.cbConceptos.Location = new System.Drawing.Point(38, 145);
			this.cbConceptos.Name = "cbConceptos";
			this.cbConceptos.Size = new System.Drawing.Size(306, 28);
			this.cbConceptos.TabIndex = 38;
			this.help.SetToolTip(this.cbConceptos, "Tipo de movimiento");
			// 
			// BtnPDF
			// 
			this.BtnPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnPDF.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnPDF.Location = new System.Drawing.Point(309, 497);
			this.BtnPDF.Name = "BtnPDF";
			this.BtnPDF.Size = new System.Drawing.Size(197, 30);
			this.BtnPDF.TabIndex = 37;
			this.BtnPDF.Text = "Mandar a PDF";
			this.BtnPDF.UseVisualStyleBackColor = true;
			this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(500, 164);
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
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
			this.reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.reporte.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.reporte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.reporte.BackgroundColor = System.Drawing.Color.Linen;
			this.reporte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.reporte.EnableHeadersVisualStyles = false;
			this.reporte.Location = new System.Drawing.Point(15, 220);
			this.reporte.MultiSelect = false;
			this.reporte.Name = "reporte";
			this.reporte.ReadOnly = true;
			this.reporte.RowHeadersVisible = false;
			this.reporte.RowHeadersWidth = 51;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(229)))), ((int)(((byte)(116)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Navy;
			this.reporte.RowsDefaultCellStyle = dataGridViewCellStyle3;
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
			this.BtnCorrerQuery.Location = new System.Drawing.Point(350, 147);
			this.BtnCorrerQuery.Name = "BtnCorrerQuery";
			this.BtnCorrerQuery.Size = new System.Drawing.Size(144, 42);
			this.BtnCorrerQuery.TabIndex = 33;
			this.BtnCorrerQuery.Text = "Ver reporte";
			this.BtnCorrerQuery.UseVisualStyleBackColor = true;
			this.BtnCorrerQuery.Click += new System.EventHandler(this.BtnCorrerQuery_Click);
			// 
			// FechaB
			// 
			this.FechaB.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.FechaB.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaB.Location = new System.Drawing.Point(533, 94);
			this.FechaB.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaB.Name = "FechaB";
			this.FechaB.Size = new System.Drawing.Size(141, 29);
			this.FechaB.TabIndex = 32;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(507, 97);
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
			this.FechaA.Location = new System.Drawing.Point(360, 94);
			this.FechaA.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaA.Name = "FechaA";
			this.FechaA.Size = new System.Drawing.Size(141, 29);
			this.FechaA.TabIndex = 30;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(92, 97);
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
			this.label1.Location = new System.Drawing.Point(174, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(509, 26);
			this.label1.TabIndex = 28;
			this.label1.Text = "Reporte de movimientos por departamento";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.cbDepartamentos);
			this.tabPage2.Controls.Add(this.cbConceptos2);
			this.tabPage2.Controls.Add(this.BtnPDFDetails);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.reporte2);
			this.tabPage2.Controls.Add(this.BtnCorrerQueryArticulos);
			this.tabPage2.Controls.Add(this.FechaB2);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.FechaA2);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Location = new System.Drawing.Point(4, 29);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(851, 579);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Movimientos por articulo";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// cbDepartamentos
			// 
			this.cbDepartamentos.FormattingEnabled = true;
			this.cbDepartamentos.Location = new System.Drawing.Point(48, 193);
			this.cbDepartamentos.Name = "cbDepartamentos";
			this.cbDepartamentos.Size = new System.Drawing.Size(306, 28);
			this.cbDepartamentos.TabIndex = 49;
			this.help.SetToolTip(this.cbDepartamentos, "Departamento");
			// 
			// cbConceptos2
			// 
			this.cbConceptos2.FormattingEnabled = true;
			this.cbConceptos2.Location = new System.Drawing.Point(48, 149);
			this.cbConceptos2.Name = "cbConceptos2";
			this.cbConceptos2.Size = new System.Drawing.Size(306, 28);
			this.cbConceptos2.TabIndex = 48;
			this.help.SetToolTip(this.cbConceptos2, "Tipo de movimiento");
			// 
			// BtnPDFDetails
			// 
			this.BtnPDFDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnPDFDetails.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnPDFDetails.Location = new System.Drawing.Point(628, 538);
			this.BtnPDFDetails.Name = "BtnPDFDetails";
			this.BtnPDFDetails.Size = new System.Drawing.Size(197, 30);
			this.BtnPDFDetails.TabIndex = 47;
			this.BtnPDFDetails.Text = "Mandar a PDF";
			this.BtnPDFDetails.UseVisualStyleBackColor = true;
			this.BtnPDFDetails.Click += new System.EventHandler(this.BtnPDFDetails_Click);
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(624, 182);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(193, 21);
			this.label5.TabIndex = 46;
			this.label5.Text = "Cargando reporte ...";
			this.label5.Visible = false;
			// 
			// reporte2
			// 
			this.reporte2.AllowUserToAddRows = false;
			this.reporte2.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(196)))));
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
			this.reporte2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.reporte2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.reporte2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.reporte2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.reporte2.BackgroundColor = System.Drawing.Color.Linen;
			this.reporte2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.reporte2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.reporte2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.reporte2.EnableHeadersVisualStyles = false;
			this.reporte2.Location = new System.Drawing.Point(17, 244);
			this.reporte2.MultiSelect = false;
			this.reporte2.Name = "reporte2";
			this.reporte2.ReadOnly = true;
			this.reporte2.RowHeadersVisible = false;
			this.reporte2.RowHeadersWidth = 51;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(229)))), ((int)(((byte)(116)))));
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy;
			this.reporte2.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.reporte2.RowTemplate.Height = 24;
			this.reporte2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.reporte2.Size = new System.Drawing.Size(817, 281);
			this.reporte2.TabIndex = 45;
			// 
			// BtnCorrerQueryArticulos
			// 
			this.BtnCorrerQueryArticulos.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnCorrerQueryArticulos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnCorrerQueryArticulos.FlatAppearance.BorderSize = 5;
			this.BtnCorrerQueryArticulos.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnCorrerQueryArticulos.Location = new System.Drawing.Point(436, 171);
			this.BtnCorrerQueryArticulos.Name = "BtnCorrerQueryArticulos";
			this.BtnCorrerQueryArticulos.Size = new System.Drawing.Size(144, 42);
			this.BtnCorrerQueryArticulos.TabIndex = 44;
			this.BtnCorrerQueryArticulos.Text = "Ver reporte";
			this.BtnCorrerQueryArticulos.UseVisualStyleBackColor = true;
			this.BtnCorrerQueryArticulos.Click += new System.EventHandler(this.BtnCorrerQueryArticulos_Click);
			// 
			// FechaB2
			// 
			this.FechaB2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.FechaB2.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaB2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaB2.Location = new System.Drawing.Point(535, 97);
			this.FechaB2.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaB2.Name = "FechaB2";
			this.FechaB2.Size = new System.Drawing.Size(141, 29);
			this.FechaB2.TabIndex = 43;
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(509, 100);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(20, 21);
			this.label6.TabIndex = 42;
			this.label6.Text = "a";
			// 
			// FechaA2
			// 
			this.FechaA2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.FechaA2.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.FechaA2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.FechaA2.Location = new System.Drawing.Point(362, 97);
			this.FechaA2.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
			this.FechaA2.Name = "FechaA2";
			this.FechaA2.Size = new System.Drawing.Size(141, 29);
			this.FechaA2.TabIndex = 41;
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(94, 100);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(241, 21);
			this.label7.TabIndex = 40;
			this.label7.Text = "Ingresa el rango de fecha";
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.label8.Location = new System.Drawing.Point(176, 48);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(438, 26);
			this.label8.TabIndex = 39;
			this.label8.Text = "Reporte de movimientos por articulo";
			// 
			// FrmMovimientos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(883, 636);
			this.Controls.Add(this.tabControl1);
			this.Name = "FrmMovimientos";
			this.Text = "Movimientos de almacen";
			this.Load += new System.EventHandler(this.FrmMovimientos_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMovimientos_Paint);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.reporte)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.reporte2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button BtnPDF;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView reporte;
		private System.Windows.Forms.Button BtnCorrerQuery;
		private System.Windows.Forms.DateTimePicker FechaB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker FechaA;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbConceptos;
		private System.Windows.Forms.SaveFileDialog guardarArchivo;
		private System.Windows.Forms.ComboBox cbConceptos2;
		private System.Windows.Forms.Button BtnPDFDetails;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridView reporte2;
		private System.Windows.Forms.Button BtnCorrerQueryArticulos;
		private System.Windows.Forms.DateTimePicker FechaB2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker FechaA2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ToolTip help;
		private System.Windows.Forms.ComboBox cbDepartamentos;
	}
}