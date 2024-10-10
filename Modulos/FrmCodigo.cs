using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos
{
    public partial class FrmCodigo : Form
    {
        public FrmCodigo()
        {
            InitializeComponent();
        }

        private async void BtnGenerar_Click(object sender, EventArgs e)
        {
            BtnGenerar.Enabled = false;
            bool reintentar = true;
            Random rnd = new Random();
            string codigo = "";
            //El while rehace el codigo hasta que salga uno valido
            while (reintentar)
            {
                //llamanos al metodo para generar el codigo
                codigo = ClsBarcodeAndQR.GenerarCodigo();

                //se verifica que el codigo no exista en la base de datos (regresa false si no existe)
                bool database = await ClsConnection.VerificarCodigoGenerado(codigo);

                //se verifica que el codigo sea valido (regresa false si el codigo es valido)
                bool ean = ClsBarcodeAndQR.EsEAN13Valido(codigo);

                //si los dos son falsos, sale del ciclo
                reintentar = database || ean;
            }
            //Se pone el codigo en el portapapeles
            Clipboard.SetText(codigo);
            //Se llama el codigo para generar la imagen del codigo de barra y el qr
            ClsBarcodeAndQR.CrearCodigoBarraYQR(codigo);
            BtnGenerar.Enabled = true;
        }

        private void FrmCodigo_Paint(object sender, PaintEventArgs e)
        {
            // Crear un rectángulo que cubra todo el formulario
            Rectangle rect = ClientRectangle;

            // Definir los colores del degradado (por ejemplo, de azul a blanco)
            Color color1 = Color.FromArgb(251, 147, 60); //--original
            Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original

            // Crear un pincel con un degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.ForwardDiagonal))
            {
                // Dibujar el degradado en el fondo del formulario
                e.Graphics.FillRectangle(brush, rect);
            }
        }
    }
}
