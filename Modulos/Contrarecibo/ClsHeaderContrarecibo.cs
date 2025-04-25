using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Modulos.Contrarecibo
{
	internal class ClsHeaderContrarecibo : PdfPageEventHelper
	{
		private Image _headerImage;
		private string _fecha;
		private string _titulo;

		public ClsHeaderContrarecibo(string imagePath, string titulo)
		{
			_headerImage = Image.GetInstance(imagePath);
			_headerImage.ScaleToFit(160f, 110f); // Ajusta el tamaño de la imagen si es necesario
			_headerImage.SetAbsolutePosition(479, PageSize.A4.Height - _headerImage.ScaledHeight + 10);
			_titulo = titulo;
			_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}

		public ClsHeaderContrarecibo(string imagePath, float width, float height, string departamento)
		{
			_headerImage = Image.GetInstance(imagePath);
			_headerImage.ScaleToFit(width, height); // Ajusta el tamaño de la imagen si es necesario
			_headerImage.SetAbsolutePosition(10, PageSize.A4.Height - _headerImage.ScaledHeight - 10);
			_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}

		public override void OnEndPage(PdfWriter writer, Document document)
		{
			PdfContentByte cb = writer.DirectContent;
			cb.AddImage(_headerImage);

			// Añadir encabezado
			PdfPTable headerTable = new PdfPTable(1)
			{
				TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
			};
			headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

			PdfPCell cell;

			// Título del documento
			iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
			Paragraph title = new Paragraph($"CURLANGO RAMOS CHRISTIAN YARELY", titleFont)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title)
			{
				Border = Rectangle.NO_BORDER,
				HorizontalAlignment = Element.ALIGN_LEFT
			};
			headerTable.AddCell(cell);

			titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
			title = new Paragraph($"R.F.C CURC890920PW1", titleFont)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title)
			{
				Border = Rectangle.NO_BORDER,
				HorizontalAlignment = Element.ALIGN_LEFT
			};
			headerTable.AddCell(cell);

			titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
			title = new Paragraph($"AVENIDA DE LOS MAESTROS NO 42 LOCAL 10", titleFont)
			{
				Alignment = Element.ALIGN_LEFT
			};
			cell = new PdfPCell(title)
			{
				Border = Rectangle.NO_BORDER,
				HorizontalAlignment = Element.ALIGN_LEFT
			};
			headerTable.AddCell(cell);

			titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
			title = new Paragraph($"COLONIA JARDINES DEL BOSQUE C.P. 84063 H. NOGALES, SONORA", titleFont)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title)
			{
				Border = Rectangle.NO_BORDER,
				HorizontalAlignment = Element.ALIGN_LEFT
			};
			headerTable.AddCell(cell);

			titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
			title = new Paragraph($"PÁGINA {document.PageNumber}", titleFont)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title)
			{
				Border = Rectangle.NO_BORDER,
				HorizontalAlignment = Element.ALIGN_CENTER
			};
			headerTable.AddCell(cell);

			_ = document.PageSize.Height - _headerImage.ScaledHeight - 30; // Ajusta este valor según sea necesario
			headerTable.WriteSelectedRows(0, -1, document.LeftMargin, 830, cb);
		}
	}
}
