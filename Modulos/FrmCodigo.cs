using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos
{
	public partial class FrmCodigo : Form
	{
		public FrmCodigo()
		{
			InitializeComponent();
			Icon = new Icon("Imagenes/LOGO_EMPRESA-removebg-preview.ico");
		}

		private string codigo;

		private async void BtnGenerar_Click(object sender, EventArgs e)
		{
			int i = 0;
			BtnGenerar.Enabled = false;
			bool reintentar = true;
			string codigo = "";
			//El while rehace el codigo hasta que salga uno valido
			while (reintentar)
			{
				i++;
				// Llamamos al método para generar el código
				codigo = ClsBarcodeAndQR.GenerarCodigo();

				// Ejecutamos ambas verificaciones en paralelo
				var verificarCodigoTask = ClsConnection.VerificarCodigoGenerado(codigo);
				var verificarEANTask = Task.Run(() => ClsBarcodeAndQR.EsEAN13Valido(codigo));

				// Esperamos a que ambas tareas terminen
				var resultados = await Task.WhenAll(verificarCodigoTask, verificarEANTask);

				// Asignamos los resultados
				bool database = resultados[0]; // Resultado de VerificarCodigoGenerado
				bool ean = resultados[1];      // Resultado de EsEAN13Valido

				// Si los dos son falsos, sale del ciclo
				reintentar = database || ean;
			}
			//Se pone el codigo en el portapapeles
			Clipboard.SetText(codigo);
			this.codigo = codigo;
			//Se llama el codigo para generar la imagen del codigo de barra y el qr
			ClsBarcodeAndQR.CrearCodigoBarraYQR(codigo);
			BtnGenerar.Enabled = true;
			SetMensaje($"Se generaron {i} codigos");
			await Task.Delay(3000);
			SetMensaje("");

		}

		private void FrmCodigo_Paint(object sender, PaintEventArgs e)
		{
			// Crear un rectángulo que cubra todo el formulario
			Rectangle rect = ClientRectangle;

			// Definir los colores del degradado (por ejemplo, de azul a blanco)
			Color color1 = Color.FromArgb(251, 147, 60); //--original
			Color color2 = ColorTranslator.FromHtml("#fdbc3c"); //--original

			// Crear un pincel con un degradado lineal
			if (rect.X > 0 && rect.Y > 0)
			{
				using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.ForwardDiagonal))
				{
					// Dibujar el degradado en el fondo del formulario
					e.Graphics.FillRectangle(brush, rect);
				}
			}
		}

		private void SetMensaje(string text)
		{
			try
			{
				Invoke(new Action(() => { lblMensaje.Text = text; }));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void BtnCopy_Click(object sender, EventArgs e)
		{
			if (codigo != null)
			{
				Clipboard.SetText(codigo);
				SetMensaje("Código copiado!!");
				await Task.Delay(3000);
				SetMensaje("");
			}
			else
			{
				MessageBox.Show("Genera el código primero por favor",
					"La Bajadita - Venta de Frutas Y Verduras",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
		}

		private async void BtnCopyBar_Click(object sender, EventArgs e)
		{
			Image barcode = Image.FromFile("barcode.png");
			Clipboard.SetImage(barcode);
			SetMensaje("Código de barra copiado!!");
			await Task.Delay(3000);
			SetMensaje("");
		}

		private async void BtnCopyQr_Click(object sender, EventArgs e)
		{
			Image qr = Image.FromFile("qr.png");
			Clipboard.SetImage(qr);
			SetMensaje("QR copiado!!");
			await Task.Delay(3000);
			SetMensaje("");
		}
	}
}
