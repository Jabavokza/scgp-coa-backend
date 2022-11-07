using Microsoft.AspNetCore.Mvc;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface
{
    public interface IPrintCoaExportCommand
    {
        // public Task<List<CoaPrintExportDataModel>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintExportSearchModel param);
        public Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsyncForNotConnectSAP(IConfiguration _configuration, CoaPrintExportSearchModel param);
        public Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintExportSearchModel param);
        public Task<List<FileDataModel>> ExcuteData(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintExportExecuteModel coaPrintModel);
    }
}
