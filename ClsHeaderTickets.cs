using System;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Reportes
{
	public class ClsHeaderTickets : PdfPageEventHelper
	{
		private Image _headerImage;
		private string _fecha;
		private string _titulo;
		private string fechaA;
		private string fechaB;

		public ClsHeaderTickets(string imagePath, string titulo, string fechaA, string fechaB)
		{
			_headerImage = Image.GetInstance(imagePath);
			_headerImage.ScaleToFit(160f, 110f); // Ajusta el tamaño de la imagen si es necesario
			_headerImage.SetAbsolutePosition(10, PageSize.A4.Height - _headerImage.ScaledHeight + 10);
			_titulo = titulo;
			_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			this.fechaA = fechaA;
			this.fechaB = fechaB;
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

			iTextSharp.text.Font titleFontDoc = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
			Paragraph title = new Paragraph($"{_titulo}", titleFontDoc)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Fecha
			Paragraph fecha = new Paragraph($"Periodo: {fechaA} a {fechaB}", FontFactory.GetFont(FontFactory.HELVETICA, 12))
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(fecha);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Número de página
			Paragraph pageNumber = new Paragraph($"Página {writer.PageNumber}", FontFactory.GetFont(FontFactory.HELVETICA, 12))
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(pageNumber);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Ajustar la posición del encabezado
			float yPos = document.PageSize.Height - 30; // Ajusta este valor según sea necesario
			headerTable.WriteSelectedRows(0, -1, document.LeftMargin, yPos, cb);
		}
	}
}
