using Microsoft.AspNetCore.Mvc;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface
{
    public interface IPrintCoaDomesticCommand
    {

        public Task<List<FileDataModel>> PrintExport(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel);
        public Task<List<FileDataModel>> SaveExport(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel);
        public Task<List<FileDataModel>> ExcuteData(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel);

        //   public List<CoaPrintDomesticDataModel> GetDPNumberDataAsync(CoaPrintDomesticSearchModel param);
        public Task<List<CoaPrintDomesticDataModel>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintDomesticSearchModel param);
    }
}
