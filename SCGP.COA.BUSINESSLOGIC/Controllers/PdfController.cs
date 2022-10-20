using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SCGP.COA.BUSINESSLOGIC.Models;

namespace SCGP.COA.BUSINESSLOGIC.Controllers
{
    public class PdfController : Controller
    {
        [HttpPost]
        public byte[] PrintCoaExport(PrintCoaExportPdfModel data)
        {

            var view = new ViewAsPdf("~/Views/Pdf/PrintCoaExport.cshtml", data)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(3, 5, 8, 5),
                IsJavaScriptDisabled = false,
            };

            var result = view.BuildFile(this.ControllerContext);
            byte[] applicationPDFData = result.Result;
            return applicationPDFData;
        }
    }
}
