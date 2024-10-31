namespace Reportes
{
	partial class FrmConteo
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbDepartamento = new System.Windows.Forms.RadioButton();
            this.rbCategoria = new System.Windows.Forms.RadioButton();
            this.cbOP = new System.Windows.Forms.ComboBox();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(596, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "Selecciona el departamento o la categoria para la hoja de conteo";
            // 
            // rbDepartamento
            // 
            this.rbDepartamento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbDepartamento.AutoSize = true;
            this.rbDepartamento.BackColor = System.Drawing.Color.Transparent;
            this.rbDepartamento.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDepartamento.Location = new System.Drawing.Point(239, 72);
            this.rbDepartamento.Name = "rbDepartamento";
            this.rbDepartamento.Size = new System.Drawing.Size(136, 21);
            this.rbDepartamento.TabIndex = 21;
            this.rbDepartamento.TabStop = true;
            this.rbDepartamento.Text = "Departamento";
            this.rbDepartamento.UseVisualStyleBackColor = false;
            this.rbDepartamento.EnabledChanged += new System.EventHandler(this.ChangeRadios);
            // 
            // rbCategoria
            // 
            this.rbCategoria.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbCategoria.AutoSize = true;
            this.rbCategoria.BackColor = System.Drawing.Color.Transparent;
            this.rbCategoria.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCategoria.Location = new System.Drawing.Point(397, 72);
            this.rbCategoria.Name = "rbCategoria";
            this.rbCategoria.Size = new System.Drawing.Size(100, 21);
            this.rbCategoria.TabIndex = 22;
            this.rbCategoria.TabStop = true;
            this.rbCategoria.Text = "Categoria";
            this.rbCategoria.UseVisualStyleBackColor = false;
            this.rbCategoria.CheckedChanged += new System.EventHandler(this.ChangeRadios);
            this.rbCategoria.EnabledChanged += new System.EventHandler(this.ChangeRadios);
            // 
            // cbOP
            // 
            this.cbOP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbOP.Font = new System.Drawing.Font("Lucida Fax", 9F);
            this.cbOP.FormattingEnabled = true;
            this.cbOP.Location = new System.Drawing.Point(211, 108);
            this.cbOP.Name = "cbOP";
            this.cbOP.Size = new System.Drawing.Size(316, 25);
            this.cbOP.TabIndex = 23;
            // 
            // BtnPDF
            // 
            this.BtnPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnPDF.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
            this.BtnPDF.Location = new System.Drawing.Point(267, 147);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(197, 30);
            this.BtnPDF.TabIndex = 38;
            this.BtnPDF.Text = "Guadar hoja";
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(187, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 21);
            this.label2.TabIndex = 39;
            this.label2.Text = "Cargando elementos por favor espere";
            this.label2.Visible = false;
            // 
            // FrmConteo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(717, 207);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnPDF);
            this.Controls.Add(this.cbOP);
            this.Controls.Add(this.rbCategoria);
            this.Controls.Add(this.rbDepartamento);
            this.Controls.Add(this.label1);
            this.Name = "FrmConteo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hojas de conteo";
            this.Load += new System.EventHandler(this.FrmConteo_Load);
            this.EnabledChanged += new System.EventHandler(this.ChangeRadios);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rbDepartamento;
		private System.Windows.Forms.RadioButton rbCategoria;
		private System.Windows.Forms.ComboBox cbOP;
		private System.Windows.Forms.Button BtnPDF;
		private System.Windows.Forms.SaveFileDialog guardarArchivo;
		private System.Windows.Forms.Label label2;
	}
}