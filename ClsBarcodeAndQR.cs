using System;
using System.Drawing;
using ZXing;

namespace Reportes
{
	public class ClsBarcodeAndQR
	{
		public static bool EsEAN13Valido(string codigoBarras)
		{
			// Verificar que tenga 13 dígitos
			if (codigoBarras.Length != 13 || !long.TryParse(codigoBarras, out _))
			{
				return false;
			}

			// Obtener los primeros 12 dígitos y el dígito de control (último dígito)
			string primeros12Digitos = codigoBarras.Substring(0, 12);
			int digitoControl = int.Parse(codigoBarras.Substring(12, 1));

			// Calcular el dígito de control a partir de los primeros 12 dígitos
			int sumaImpares = 0;
			int sumaPares = 0;

			for (int i = 0; i < primeros12Digitos.Length; i++)
			{
				int digito = int.Parse(primeros12Digitos[i].ToString());

				if ((i + 1) % 2 == 0) // Posiciones pares
				{
					sumaPares += digito;
				}
				else // Posiciones impares
				{
					sumaImpares += digito;
				}
			}

			// Calcular el total y el complemento a 10
			int total = sumaImpares + (sumaPares * 3);
			int digitoCalculado = (10 - (total % 10)) % 10;

			// Verificar si el dígito calculado coincide con el dígito de control
			return !(digitoCalculado == digitoControl);
		}

		public static string GenerarCodigo()
		{
			Random rnd = new Random();
			string codigo = "750";
			for (int i = 0; i < 10; i++)
			{
				codigo = codigo + rnd.Next(0, 10);
			}

			return codigo;
		}

		public static void CrearCodigoBarraYQR(string codigo)
		{
			var bar = new BarcodeWriter();
			bar.Options = new ZXing.Common.EncodingOptions()
			{
				Width = 300,
				Height = 100,
				GS1Format = true
			};

			bar.Format = BarcodeFormat.EAN_13;

			Bitmap barra = bar.Write(codigo);

			barra.Save("barcode.png");

			bar.Format = BarcodeFormat.QR_CODE;
			bar.Options = new ZXing.Common.EncodingOptions
			{
				Width = 400,
				Height = 400,
				Margin = 1 // Opcional, reduce el margen
			};

			// Generar el QR
			Bitmap qr = bar.Write(codigo);

			// Ruta de la imagen del logotipo
			string logoPath = "Imagenes/LOGO_EMPRESA-removebg-preview.png";

			// Combinación del QR con el logotipo
			Bitmap qrConLogo = IncrustarLogo(qr, logoPath);

			// Guardar el QR con el logotipo
			qrConLogo.Save("qr.png");


			//Process.Start("barcode.png");
			//Process.Start("qr.png");
		}

		private static Bitmap IncrustarLogo(Bitmap qr, string logoPath)
		{
			using (Graphics graphics = Graphics.FromImage(qr))
			{
				// Cargar el logotipo
				Bitmap logo = new Bitmap(logoPath);

				// Calcular el tamaño y la posición del logotipo
				int logoSize = qr.Width / 3; // El logotipo será un 20% del tamaño del QR
				int xPosition = (qr.Width - logoSize) / 2;
				int yPosition = (qr.Height - logoSize) / 2;

				// Redimensionar el logotipo
				Bitmap resizedLogo = new Bitmap(logo, new Size(logoSize, logoSize));

				// Dibujar el logotipo sobre el QR
				graphics.DrawImage(resizedLogo, xPosition, yPosition, logoSize, logoSize);
			}

			return qr;
		}
	}
}
