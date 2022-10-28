using Microsoft.AspNetCore.Mvc;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface
{
    public interface IPrintCoaExportCommand
    {
        //FileDataModel ExportPdf(ControllerContext controllerContext);
        //FileDataModel ExportExcel();
        public List<FileDataModel> PrintExport(ControllerContext controllerContext, CoaPrintExportExecuteModel coaPrintModel);
        public List<FileDataModel> SaveExport(ControllerContext controllerContext, CoaPrintExportExecuteModel coaPrintModel);
        public Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintExportSearchModel param);
    }
}
