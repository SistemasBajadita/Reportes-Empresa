namespace Reportes.Modulos
{
    partial class FrmCodigo
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
			this.label2 = new System.Windows.Forms.Label();
			this.BtnGenerar = new System.Windows.Forms.Button();
			this.BtnCopy = new System.Windows.Forms.Button();
			this.BtnCopyBar = new System.Windows.Forms.Button();
			this.BtnCopyQr = new System.Windows.Forms.Button();
			this.lblMensaje = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(151, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(266, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "Generar Codigo de producto";
			// 
			// BtnGenerar
			// 
			this.BtnGenerar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.BtnGenerar.FlatAppearance.BorderSize = 2;
			this.BtnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGenerar.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnGenerar.Location = new System.Drawing.Point(199, 79);
			this.BtnGenerar.Name = "BtnGenerar";
			this.BtnGenerar.Size = new System.Drawing.Size(172, 50);
			this.BtnGenerar.TabIndex = 5;
			this.BtnGenerar.Text = "Generar";
			this.BtnGenerar.UseVisualStyleBackColor = true;
			this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
			// 
			// BtnCopy
			// 
			this.BtnCopy.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.BtnCopy.FlatAppearance.BorderSize = 2;
			this.BtnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnCopy.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnCopy.Location = new System.Drawing.Point(21, 145);
			this.BtnCopy.Name = "BtnCopy";
			this.BtnCopy.Size = new System.Drawing.Size(172, 50);
			this.BtnCopy.TabIndex = 7;
			this.BtnCopy.Text = "Copiar codigo";
			this.BtnCopy.UseVisualStyleBackColor = true;
			this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
			// 
			// BtnCopyBar
			// 
			this.BtnCopyBar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.BtnCopyBar.FlatAppearance.BorderSize = 2;
			this.BtnCopyBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnCopyBar.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnCopyBar.Location = new System.Drawing.Point(199, 145);
			this.BtnCopyBar.Name = "BtnCopyBar";
			this.BtnCopyBar.Size = new System.Drawing.Size(172, 50);
			this.BtnCopyBar.TabIndex = 8;
			this.BtnCopyBar.Text = "Copiar codigo de barra";
			this.BtnCopyBar.UseVisualStyleBackColor = true;
			this.BtnCopyBar.Click += new System.EventHandler(this.BtnCopyBar_Click);
			// 
			// BtnCopyQr
			// 
			this.BtnCopyQr.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.BtnCopyQr.FlatAppearance.BorderSize = 2;
			this.BtnCopyQr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnCopyQr.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnCopyQr.Location = new System.Drawing.Point(377, 145);
			this.BtnCopyQr.Name = "BtnCopyQr";
			this.BtnCopyQr.Size = new System.Drawing.Size(172, 50);
			this.BtnCopyQr.TabIndex = 9;
			this.BtnCopyQr.Text = "Copiar codigo QR";
			this.BtnCopyQr.UseVisualStyleBackColor = true;
			this.BtnCopyQr.Click += new System.EventHandler(this.BtnCopyQr_Click);
			// 
			// lblMensaje
			// 
			this.lblMensaje.AutoSize = true;
			this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
			this.lblMensaje.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMensaje.Location = new System.Drawing.Point(151, 199);
			this.lblMensaje.Name = "lblMensaje";
			this.lblMensaje.Size = new System.Drawing.Size(0, 20);
			this.lblMensaje.TabIndex = 10;
			// 
			// FrmCodigo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(145)))), ((int)(((byte)(58)))));
			this.ClientSize = new System.Drawing.Size(568, 228);
			this.Controls.Add(this.lblMensaje);
			this.Controls.Add(this.BtnCopyQr);
			this.Controls.Add(this.BtnCopyBar);
			this.Controls.Add(this.BtnCopy);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.BtnGenerar);
			this.Name = "FrmCodigo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Codigo de producto nuevo";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnGenerar;
		private System.Windows.Forms.Button BtnCopy;
		private System.Windows.Forms.Button BtnCopyBar;
		private System.Windows.Forms.Button BtnCopyQr;
		private System.Windows.Forms.Label lblMensaje;
	}
}