using iTextSharp.text;
using iTextSharp.text.pdf;

namespace quickLog
{
    internal class Helpers
    {


        public class FooterHandler : PdfPageEventHelper
        {
            private readonly iTextSharp.text.Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                Phrase footer = new Phrase("Created using quickLog https://github.com/gustavoparedes/QuickLog", footerFont);

                // Obtener las dimensiones de la página
                float x = document.PageSize.GetRight(50); // 50 puntos desde el margen derecho
                float y = document.PageSize.GetBottom(30); // 30 puntos desde el margen inferior

                // Agregar el pie de página
                ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, footer, x, y, 0);
            }
        }


    }
}
