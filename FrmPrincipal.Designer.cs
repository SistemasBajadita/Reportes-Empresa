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
            this.Ventas = new System.Windows.Forms.Button();
            this.Compras = new System.Windows.Forms.Button();
            this.TopProductos = new System.Windows.Forms.Button();
            this.HabilitarReportes = new System.Windows.Forms.Button();
            this.HojaConteo = new System.Windows.Forms.Button();
            this.MovAlm = new System.Windows.Forms.Button();
            this.Margenes = new System.Windows.Forms.Button();
            this.Tickets = new System.Windows.Forms.Button();
            this.Negativos = new System.Windows.Forms.Button();
            this.PreciosCocina = new System.Windows.Forms.Button();
            this.DespProveedor = new System.Windows.Forms.Button();
            this.ModificarAccesos = new System.Windows.Forms.MenuStrip();
            this.modificarRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verMovimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tortillas = new System.Windows.Forms.Button();
            this.Codigos = new System.Windows.Forms.Button();
            this.ModificarAccesos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ventas
            // 
            this.Ventas.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Ventas.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Ventas.Location = new System.Drawing.Point(21, 51);
            this.Ventas.Name = "Ventas";
            this.Ventas.Size = new System.Drawing.Size(516, 58);
            this.Ventas.TabIndex = 0;
            this.Ventas.Text = "Ventas con costo";
            this.Ventas.UseVisualStyleBackColor = true;
            this.Ventas.Click += new System.EventHandler(this.BtnVentaCosto_Click);
            // 
            // Compras
            // 
            this.Compras.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Compras.Location = new System.Drawing.Point(543, 115);
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(516, 58);
            this.Compras.TabIndex = 1;
            this.Compras.Text = "Compras por departamento";
            this.Compras.UseVisualStyleBackColor = true;
            this.Compras.Click += new System.EventHandler(this.BtnCompras_Click);
            // 
            // TopProductos
            // 
            this.TopProductos.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.TopProductos.Location = new System.Drawing.Point(21, 179);
            this.TopProductos.Name = "TopProductos";
            this.TopProductos.Size = new System.Drawing.Size(516, 58);
            this.TopProductos.TabIndex = 2;
            this.TopProductos.Text = "Productos mas vendidos";
            this.TopProductos.UseVisualStyleBackColor = true;
            this.TopProductos.Click += new System.EventHandler(this.BtnTopProductos_Click);
            // 
            // HabilitarReportes
            // 
            this.HabilitarReportes.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.HabilitarReportes.Location = new System.Drawing.Point(21, 371);
            this.HabilitarReportes.Name = "HabilitarReportes";
            this.HabilitarReportes.Size = new System.Drawing.Size(516, 58);
            this.HabilitarReportes.TabIndex = 3;
            this.HabilitarReportes.Text = "Habilitar reporte de cortes";
            this.HabilitarReportes.UseVisualStyleBackColor = true;
            this.HabilitarReportes.Click += new System.EventHandler(this.BtnActiveReport_Click);
            // 
            // HojaConteo
            // 
            this.HojaConteo.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.HojaConteo.Location = new System.Drawing.Point(21, 243);
            this.HojaConteo.Name = "HojaConteo";
            this.HojaConteo.Size = new System.Drawing.Size(516, 58);
            this.HojaConteo.TabIndex = 4;
            this.HojaConteo.Text = "Hoja de conteo";
            this.HojaConteo.UseVisualStyleBackColor = true;
            this.HojaConteo.Click += new System.EventHandler(this.BtnCountProducts_Click);
            // 
            // MovAlm
            // 
            this.MovAlm.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.MovAlm.Location = new System.Drawing.Point(543, 179);
            this.MovAlm.Name = "MovAlm";
            this.MovAlm.Size = new System.Drawing.Size(516, 58);
            this.MovAlm.TabIndex = 5;
            this.MovAlm.Text = "Movimientos de almacen";
            this.MovAlm.UseVisualStyleBackColor = true;
            this.MovAlm.Click += new System.EventHandler(this.BtnMovAlm_Click);
            // 
            // Margenes
            // 
            this.Margenes.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margenes.Location = new System.Drawing.Point(543, 51);
            this.Margenes.Name = "Margenes";
            this.Margenes.Size = new System.Drawing.Size(516, 58);
            this.Margenes.TabIndex = 6;
            this.Margenes.Text = "Margenes de articulos por departamento";
            this.Margenes.UseVisualStyleBackColor = true;
            this.Margenes.Click += new System.EventHandler(this.BtnVentasDetalladas_Click);
            // 
            // Tickets
            // 
            this.Tickets.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Tickets.Location = new System.Drawing.Point(543, 243);
            this.Tickets.Name = "Tickets";
            this.Tickets.Size = new System.Drawing.Size(516, 58);
            this.Tickets.TabIndex = 8;
            this.Tickets.Text = "Tickets por chofer";
            this.Tickets.UseVisualStyleBackColor = true;
            this.Tickets.Click += new System.EventHandler(this.BtnTicketChofer_Click);
            // 
            // Negativos
            // 
            this.Negativos.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Negativos.Location = new System.Drawing.Point(21, 115);
            this.Negativos.Name = "Negativos";
            this.Negativos.Size = new System.Drawing.Size(516, 58);
            this.Negativos.TabIndex = 9;
            this.Negativos.Text = "Negativos en inventario";
            this.Negativos.UseVisualStyleBackColor = true;
            this.Negativos.Click += new System.EventHandler(this.BtnNegativos_Click);
            // 
            // PreciosCocina
            // 
            this.PreciosCocina.Enabled = false;
            this.PreciosCocina.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.PreciosCocina.Location = new System.Drawing.Point(21, 307);
            this.PreciosCocina.Name = "PreciosCocina";
            this.PreciosCocina.Size = new System.Drawing.Size(516, 58);
            this.PreciosCocina.TabIndex = 10;
            this.PreciosCocina.Text = "Actualizar precios Cocina";
            this.PreciosCocina.UseVisualStyleBackColor = true;
            this.PreciosCocina.Click += new System.EventHandler(this.BtnCocinaPrecios_Click);
            // 
            // DespProveedor
            // 
            this.DespProveedor.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.DespProveedor.Location = new System.Drawing.Point(543, 307);
            this.DespProveedor.Name = "DespProveedor";
            this.DespProveedor.Size = new System.Drawing.Size(516, 58);
            this.DespProveedor.TabIndex = 11;
            this.DespProveedor.Text = "Desplazamiento por proveedor";
            this.DespProveedor.UseVisualStyleBackColor = true;
            this.DespProveedor.Click += new System.EventHandler(this.button1_Click);
            // 
            // ModificarAccesos
            // 
            this.ModificarAccesos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ModificarAccesos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarRolesToolStripMenuItem,
            this.verMovimientosToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.ModificarAccesos.Location = new System.Drawing.Point(0, 0);
            this.ModificarAccesos.Name = "ModificarAccesos";
            this.ModificarAccesos.Size = new System.Drawing.Size(1083, 28);
            this.ModificarAccesos.TabIndex = 12;
            this.ModificarAccesos.Text = "menuStrip1";
            // 
            // modificarRolesToolStripMenuItem
            // 
            this.modificarRolesToolStripMenuItem.Name = "modificarRolesToolStripMenuItem";
            this.modificarRolesToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.modificarRolesToolStripMenuItem.Text = "Modificar roles por usuario";
            this.modificarRolesToolStripMenuItem.Click += new System.EventHandler(this.modificarRolesToolStripMenuItem_Click);
            // 
            // verMovimientosToolStripMenuItem
            // 
            this.verMovimientosToolStripMenuItem.Name = "verMovimientosToolStripMenuItem";
            this.verMovimientosToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.verMovimientosToolStripMenuItem.Text = "Ver movimientos";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // Tortillas
            // 
            this.Tortillas.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Tortillas.Location = new System.Drawing.Point(543, 371);
            this.Tortillas.Name = "Tortillas";
            this.Tortillas.Size = new System.Drawing.Size(516, 58);
            this.Tortillas.TabIndex = 13;
            this.Tortillas.Text = "Venta de tortillas";
            this.Tortillas.UseVisualStyleBackColor = true;
            this.Tortillas.Click += new System.EventHandler(this.Tortillas_Click);
            // 
            // Codigos
            // 
            this.Codigos.Font = new System.Drawing.Font("Lucida Fax", 13.8F);
            this.Codigos.Location = new System.Drawing.Point(21, 435);
            this.Codigos.Name = "Codigos";
            this.Codigos.Size = new System.Drawing.Size(516, 58);
            this.Codigos.TabIndex = 14;
            this.Codigos.Text = "Generar codigo de producto";
            this.Codigos.UseVisualStyleBackColor = true;
            this.Codigos.Click += new System.EventHandler(this.Codigos_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1083, 506);
            this.Controls.Add(this.Codigos);
            this.Controls.Add(this.Tortillas);
            this.Controls.Add(this.DespProveedor);
            this.Controls.Add(this.PreciosCocina);
            this.Controls.Add(this.Negativos);
            this.Controls.Add(this.Tickets);
            this.Controls.Add(this.Margenes);
            this.Controls.Add(this.MovAlm);
            this.Controls.Add(this.HojaConteo);
            this.Controls.Add(this.HabilitarReportes);
            this.Controls.Add(this.TopProductos);
            this.Controls.Add(this.Compras);
            this.Controls.Add(this.Ventas);
            this.Controls.Add(this.ModificarAccesos);
            this.MainMenuStrip = this.ModificarAccesos;
            this.Name = "FrmPrincipal";
            this.Text = "Reportes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmPrincipal_Paint);
            this.ModificarAccesos.ResumeLayout(false);
            this.ModificarAccesos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Ventas;
		private System.Windows.Forms.Button Compras;
		private System.Windows.Forms.Button TopProductos;
		private System.Windows.Forms.Button HabilitarReportes;
		private System.Windows.Forms.Button HojaConteo;
		private System.Windows.Forms.Button MovAlm;
		private System.Windows.Forms.Button Margenes;
		private System.Windows.Forms.Button Tickets;
		private System.Windows.Forms.Button Negativos;
		private System.Windows.Forms.Button PreciosCocina;
		private System.Windows.Forms.Button DespProveedor;
		private System.Windows.Forms.MenuStrip ModificarAccesos;
		private System.Windows.Forms.ToolStripMenuItem modificarRolesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem verMovimientosToolStripMenuItem;
		private System.Windows.Forms.Button Tortillas;
        private System.Windows.Forms.Button Codigos;
    }
}

