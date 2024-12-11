

using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Reports.Services
{
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public string GeneratePdf(string htmlContent, string outputPath)
        {
            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Out = outputPath
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            _converter.Convert(pdfDocument);
            return outputPath;
        }
    }
}
