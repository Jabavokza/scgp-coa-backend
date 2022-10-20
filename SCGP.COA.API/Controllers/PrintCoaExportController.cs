using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa;
using SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Nodes;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PrintCoaExportController : ControllerBase
    {
        private IPrintCoaExportCommand _printCoaCommand;
        public PrintCoaExportController(IPrintCoaExportCommand printCoaCommand)
        {
            _printCoaCommand = printCoaCommand;
        }

        [HttpPost]
        // [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<Dictionary<string, Dictionary<string, string[]>>> Search([FromBody] CoaPrintDomesticSearchModel param)
        {
            var o = new Dictionary<string, string[]>();
            o.Add("EO-001", new string[] { "DP-001", "DP-002", "DP-003" });
            o.Add("EO-002", new string[] { "DP-004", "DP-005", "DP-006" });
            o.Add("EO-003", new string[] { "DP-007", "DP-008", "DP-009" });

            var PI = new Dictionary<string, Dictionary<string, string[]>>();
            PI.Add("PO-001", o);

            return ResponseResult<CoaPrintExportDataModel>.Success(PI);
        }
        [HttpPost]
        //[Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<FileDataModel>> Print([FromBody] CoaPrintExportExecuteModel param)
        {
            var result = _printCoaCommand.PrintExport(this.ControllerContext, param);
            return ResponseResult<List<FileDataModel>>.Success(result);
        }

        [HttpPost]
        //[Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<FileDataModel>> Save([FromBody] CoaPrintExportExecuteModel param)
        {
            var result = _printCoaCommand.SaveExport(this.ControllerContext, param);
            return ResponseResult<List<FileDataModel>>.Success(result);
        }
    }
}
