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
			this.SuspendLayout();
			// 
			// BtnVentaCosto
			// 
			this.BtnVentaCosto.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnVentaCosto.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
			this.BtnVentaCosto.Location = new System.Drawing.Point(54, 42);
			this.BtnVentaCosto.Name = "BtnVentaCosto";
			this.BtnVentaCosto.Size = new System.Drawing.Size(257, 58);
			this.BtnVentaCosto.TabIndex = 0;
			this.BtnVentaCosto.Text = "Ventas con costo";
			this.BtnVentaCosto.UseVisualStyleBackColor = true;
			this.BtnVentaCosto.Click += new System.EventHandler(this.BtnVentaCosto_Click);
			// 
			// FrmPrincipal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(363, 260);
			this.Controls.Add(this.BtnVentaCosto);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmPrincipal";
			this.Text = "Reportes";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnVentaCosto;
	}
}

