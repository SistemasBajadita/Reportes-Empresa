namespace Reportes.Modulos.Contrarecibo
{
	partial class FrmAplicarContrarecibo
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
			this.label5 = new System.Windows.Forms.Label();
			this.TxtFolio = new System.Windows.Forms.TextBox();
			this.BtnSeleccionar = new System.Windows.Forms.Button();
			this.lblProveedor = new System.Windows.Forms.Label();
			this.lblMonto = new System.Windows.Forms.Label();
			this.lblFecha = new System.Windows.Forms.Label();
			this.lblEstatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(196, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 23);
			this.label5.TabIndex = 21;
			this.label5.Text = "Folio";
			// 
			// TxtFolio
			// 
			this.TxtFolio.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtFolio.Location = new System.Drawing.Point(258, 43);
			this.TxtFolio.Name = "TxtFolio";
			this.TxtFolio.Size = new System.Drawing.Size(80, 28);
			this.TxtFolio.TabIndex = 20;
			this.TxtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAplicarContrarecibo_KeyDown);
			// 
			// BtnSeleccionar
			// 
			this.BtnSeleccionar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
			this.BtnSeleccionar.FlatAppearance.BorderSize = 5;
			this.BtnSeleccionar.Font = new System.Drawing.Font("Lucida Fax", 10.8F);
			this.BtnSeleccionar.Location = new System.Drawing.Point(138, 227);
			this.BtnSeleccionar.Name = "BtnSeleccionar";
			this.BtnSeleccionar.Size = new System.Drawing.Size(266, 42);
			this.BtnSeleccionar.TabIndex = 27;
			this.BtnSeleccionar.Text = "Aplicar como pagado";
			this.BtnSeleccionar.UseVisualStyleBackColor = true;
			this.BtnSeleccionar.Click += new System.EventHandler(this.BtnSeleccionar_Click);
			// 
			// lblProveedor
			// 
			this.lblProveedor.AutoSize = true;
			this.lblProveedor.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProveedor.Location = new System.Drawing.Point(63, 88);
			this.lblProveedor.Name = "lblProveedor";
			this.lblProveedor.Size = new System.Drawing.Size(109, 23);
			this.lblProveedor.TabIndex = 28;
			this.lblProveedor.Text = "Proveedor:";
			// 
			// lblMonto
			// 
			this.lblMonto.AutoSize = true;
			this.lblMonto.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMonto.Location = new System.Drawing.Point(21, 121);
			this.lblMonto.Name = "lblMonto";
			this.lblMonto.Size = new System.Drawing.Size(151, 23);
			this.lblMonto.TabIndex = 29;
			this.lblMonto.Text = "Monto a pagar:";
			// 
			// lblFecha
			// 
			this.lblFecha.AutoSize = true;
			this.lblFecha.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFecha.Location = new System.Drawing.Point(25, 156);
			this.lblFecha.Name = "lblFecha";
			this.lblFecha.Size = new System.Drawing.Size(147, 23);
			this.lblFecha.TabIndex = 30;
			this.lblFecha.Text = "Fecha de pago:";
			// 
			// lblEstatus
			// 
			this.lblEstatus.AutoSize = true;
			this.lblEstatus.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEstatus.Location = new System.Drawing.Point(196, 189);
			this.lblEstatus.Name = "lblEstatus";
			this.lblEstatus.Size = new System.Drawing.Size(0, 23);
			this.lblEstatus.TabIndex = 31;
			// 
			// FrmAplicarContrarecibo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(532, 319);
			this.Controls.Add(this.lblEstatus);
			this.Controls.Add(this.lblFecha);
			this.Controls.Add(this.lblMonto);
			this.Controls.Add(this.lblProveedor);
			this.Controls.Add(this.BtnSeleccionar);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.TxtFolio);
			this.Name = "FrmAplicarContrarecibo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Aplicar contrarecibo";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox TxtFolio;
		private System.Windows.Forms.Button BtnSeleccionar;
		private System.Windows.Forms.Label lblProveedor;
		private System.Windows.Forms.Label lblMonto;
		private System.Windows.Forms.Label lblFecha;
		private System.Windows.Forms.Label lblEstatus;
	}
}