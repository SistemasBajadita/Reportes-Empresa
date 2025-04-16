using iTextSharp.text.pdf;
using iTextSharp.text;
using System;

namespace Reportes
{
	// Esta clase es para poder insertar imágenes y encabezados en el PDF
	public class ClsHeader : PdfPageEventHelper
	{
		private Image _headerImage;
		private string _departamento;
		private string _fecha;
		private string _titulo;

		public ClsHeader(string imagePath, string departamento, string titulo)
		{
			_headerImage = Image.GetInstance(imagePath);
			_headerImage.ScaleToFit(160f, 110f); // Ajusta el tamaño de la imagen si es necesario
			_headerImage.SetAbsolutePosition(10, PageSize.A4.Height - _headerImage.ScaledHeight + 10);
			_departamento = departamento;
			_titulo = titulo;
			_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}

		public ClsHeader(string imagePath, float width, float height, string departamento)
		{
			_headerImage = Image.GetInstance(imagePath);
			_headerImage.ScaleToFit(width, height); // Ajusta el tamaño de la imagen si es necesario
			_headerImage.SetAbsolutePosition(10, PageSize.A4.Height - _headerImage.ScaledHeight - 10);
			_departamento = departamento;
			_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}

		public override void OnEndPage(PdfWriter writer, Document document)
		{
			PdfContentByte cb = writer.DirectContent;
			cb.AddImage(_headerImage);

			// Añadir encabezado
			PdfPTable headerTable = new PdfPTable(1);
			headerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
			headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

			PdfPCell cell;

			// Título del documento
			iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
			Paragraph title = new Paragraph($"{_titulo}", titleFont)
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(title);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Fecha
			Paragraph fecha = new Paragraph($"Fecha: {_fecha}", FontFactory.GetFont(FontFactory.HELVETICA, 12))
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(fecha);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Departamento
			Paragraph departamento = new Paragraph($"{_departamento}", FontFactory.GetFont(FontFactory.HELVETICA, 12))
			{
				Alignment = Element.ALIGN_CENTER
			};
			cell = new PdfPCell(departamento);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			Paragraph pageNumber = new Paragraph($"Página {writer.PageNumber}", FontFactory.GetFont(FontFactory.HELVETICA, 12))
			{
				Alignment = Element.ALIGN_CENTER
			};

			cell = new PdfPCell(pageNumber);
			cell.Border = Rectangle.NO_BORDER;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerTable.AddCell(cell);

			// Ajustar la posición del encabezado
			float yPos = document.PageSize.Height - _headerImage.ScaledHeight - 30; // Ajusta este valor según sea necesario
			headerTable.WriteSelectedRows(0, -1, document.LeftMargin, 830, cb);
		}
	}
}
