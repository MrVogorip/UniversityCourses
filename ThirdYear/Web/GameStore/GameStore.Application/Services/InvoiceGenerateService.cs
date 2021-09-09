using System.IO;
using System.Linq;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace GameStore.Application.Services
{
    public class InvoiceGenerateService : IInvoiceGenerateService
    {
        private PdfDocument _pdfDocument;
        private PdfPage _pdfPage;
        private PdfStandardFont _font;

        public MemoryStream GenerateInPdf(Order order)
        {
            _pdfDocument = new PdfDocument();
            _pdfDocument.PageSettings.Margins.All = 50;
            _font = new PdfStandardFont(PdfFontFamily.TimesRoman, 18);
            _pdfPage = _pdfDocument.Pages.Add();

            string orderId = order.Id.Substring(0, 5);
            DrawOnInvoice($"Order №{orderId}", new PointF(0, 0));
            DrawOnInvoice($"Total price: {order.TotalPrice}", new PointF(0, 50));
            DrawOnInvoice("Games:", new PointF(0, 100));

            var grid = new PdfGrid
            {
                DataSource = order.OrderDetails.Select(x => new { x.Game.Name, x.Quantity, x.Discount, x.Price }),
            };
            grid.Draw(_pdfPage, new PointF(0, 150));

            var stream = new MemoryStream();
            _pdfDocument.Save(stream);
            stream.Position = 0;

            return stream;
        }

        private void DrawOnInvoice(string text, PointF point)
        {
            _pdfPage.Graphics.DrawString(text, _font, PdfBrushes.Black, point);
        }
    }
}
