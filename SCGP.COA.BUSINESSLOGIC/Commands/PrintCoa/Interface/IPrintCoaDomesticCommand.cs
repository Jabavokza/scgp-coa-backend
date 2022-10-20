using Microsoft.AspNetCore.Mvc;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface
{
    public interface IPrintCoaDomesticCommand
    {
        //FileDataModel ExportPdf(ControllerContext controllerContext);
        //FileDataModel ExportExcel();
        public List<FileDataModel> PrintExport(ControllerContext controllerContext, CoaPrintDomesticExecuteModel coaPrintModel);
        public List<FileDataModel> SaveExport(ControllerContext controllerContext, CoaPrintDomesticExecuteModel coaPrintModel);

        //   public List<CoaPrintDomesticDataModel> GetDPNumberDataAsync(CoaPrintDomesticSearchModel param);
        public  Task<List<CoaPrintDomesticDataModel>> GetDPNumberDataAsync(CoaPrintDomesticSearchModel param);
    }
}
