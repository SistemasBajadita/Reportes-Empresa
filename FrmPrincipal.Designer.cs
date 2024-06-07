namespace Reportes
{
	partial class FrmPrincipal
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
			this.BtnVentaCosto = new System.Windows.Forms.Button();
			this.BtnCompras = new System.Windows.Forms.Button();
			this.BtnTopProductos = new System.Windows.Forms.Button();
			this.BtnActiveReport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnVentaCosto
			// 
			this.BtnVentaCosto.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnVentaCosto.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnVentaCosto.Location = new System.Drawing.Point(81, 55);
			this.BtnVentaCosto.Name = "BtnVentaCosto";
			this.BtnVentaCosto.Size = new System.Drawing.Size(431, 58);
			this.BtnVentaCosto.TabIndex = 0;
			this.BtnVentaCosto.Text = "Ventas con costo";
			this.BtnVentaCosto.UseVisualStyleBackColor = true;
			this.BtnVentaCosto.Click += new System.EventHandler(this.BtnVentaCosto_Click);
			// 
			// BtnCompras
			// 
			this.BtnCompras.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnCompras.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnCompras.Location = new System.Drawing.Point(80, 116);
			this.BtnCompras.Name = "BtnCompras";
			this.BtnCompras.Size = new System.Drawing.Size(431, 58);
			this.BtnCompras.TabIndex = 1;
			this.BtnCompras.Text = "Compras por departamento";
			this.BtnCompras.UseVisualStyleBackColor = true;
			this.BtnCompras.Click += new System.EventHandler(this.BtnCompras_Click);
			// 
			// BtnTopProductos
			// 
			this.BtnTopProductos.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnTopProductos.Enabled = false;
			this.BtnTopProductos.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnTopProductos.Location = new System.Drawing.Point(81, 178);
			this.BtnTopProductos.Name = "BtnTopProductos";
			this.BtnTopProductos.Size = new System.Drawing.Size(431, 58);
			this.BtnTopProductos.TabIndex = 2;
			this.BtnTopProductos.Text = "Productos mas vendidos";
			this.BtnTopProductos.UseVisualStyleBackColor = true;
			this.BtnTopProductos.Click += new System.EventHandler(this.BtnTopProductos_Click);
			// 
			// BtnActiveReport
			// 
			this.BtnActiveReport.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnActiveReport.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnActiveReport.Location = new System.Drawing.Point(80, 242);
			this.BtnActiveReport.Name = "BtnActiveReport";
			this.BtnActiveReport.Size = new System.Drawing.Size(431, 58);
			this.BtnActiveReport.TabIndex = 3;
			this.BtnActiveReport.Text = "Habilitar reporte de cortes";
			this.BtnActiveReport.UseVisualStyleBackColor = true;
			this.BtnActiveReport.Click += new System.EventHandler(this.BtnActiveReport_Click);
			// 
			// FrmPrincipal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(568, 345);
			this.Controls.Add(this.BtnActiveReport);
			this.Controls.Add(this.BtnTopProductos);
			this.Controls.Add(this.BtnCompras);
			this.Controls.Add(this.BtnVentaCosto);
			this.Name = "FrmPrincipal";
			this.Text = "Reportes";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnVentaCosto;
		private System.Windows.Forms.Button BtnCompras;
		private System.Windows.Forms.Button BtnTopProductos;
		private System.Windows.Forms.Button BtnActiveReport;
	}
}

