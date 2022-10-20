using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using SCGP.COA.COMMON.Attributes;
using PdfSharpCore.Drawing;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;

namespace SCGP.COA.BUSINESSLOGIC.Services
{
    [TransientPriorityRegistration]
    public class PdfService : IPdfService
    {
        public void MergeMultiplePDFIntoSinglePDF(List<byte[]> pdfFiles, MemoryStream ms, bool pagePerFile = false)
        {
            var pdfDocument = new PdfDocument();
            var pageFiles = new List<int>();

            #region merge file
            foreach (var pdfFile in pdfFiles)
            {
                //open file
                var pdfms = new MemoryStream(pdfFile);
                var inputPDFDocument = PdfReader.Open(pdfms, PdfDocumentOpenMode.Import);
                pdfDocument.Version = inputPDFDocument.Version;

                foreach (PdfPage page in inputPDFDocument.Pages)
                {
                    pdfDocument.AddPage(page);
                }
                pageFiles.Add(inputPDFDocument.Pages.Count);
            }
            #endregion

            #region paging
            if (pagePerFile)
            {
                var font = new XFont("Tahoma", 8, XFontStyle.Regular);
                // Create variable that store page count  
                var currentPage = 1;
                var totalPageIndex = 0;
                var totalPage = pageFiles[totalPageIndex];

                foreach (PdfPage page in pdfDocument.Pages)
                {
                    var headerRight1 = "Page " + currentPage.ToString() + "/" + totalPage;
                    //var headerRight2 = "PAGE";

                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        var basex = 270; // 389.5;
                        var basey = 10; // 7.5;
                        var mhr1 = gfx.MeasureString(headerRight1, font);
                        //var mhr2 = gfx.MeasureString(headerRight2, font);
                        //var hrdiff = (mhr1.Width - mhr2.Width);

                        var layoutHR1 = new XRect(basex /*X*/ , basey /*Y*/ , page.Width /*Width*/ , font.Height /*Height*/ );
                        //var layoutHR2 = new XRect(basex + hrdiff /*X*/ , basey + font.Height  /*Y*/ , page.Width /*Width*/ , font.Height /*Height*/ );

                        gfx.DrawString(headerRight1, font, XBrushes.Black, layoutHR1, XStringFormats.Center);
                        //gfx.DrawString(headerRight2, font, XBrushes.Black, layoutHR2, XStringFormats.Center);
                    }

                    if (currentPage == totalPage)
                    {
                        totalPageIndex++;
                        currentPage = 1;
                        if (pageFiles.Count >= totalPageIndex + 1)
                            totalPage = pageFiles[totalPageIndex];
                    }
                    else
                    {
                        currentPage++;
                    }
                }
            }
            #endregion

            pdfDocument.Save(ms, false);
        }
    }
}
