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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PrintCoaExportController : ControllerBase
    {
        private IPrintCoaExportCommand _printCoaCommand;
        private readonly IConfiguration _configuration;
        public PrintCoaExportController(IConfiguration configuration, IPrintCoaExportCommand printCoaCommand)
        {
            _printCoaCommand = printCoaCommand;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public async Task<ResponseResult<Dictionary<string, Dictionary<string, string[]>>>> SearchAsync([FromBody] CoaPrintExportSearchModel param)
        {
            var result = await _printCoaCommand.GetDPNumberDataAsync(_configuration, param);
            return ResponseResult<Dictionary<string, Dictionary<string, string[]>>>.Success(result);
        }
       
        [HttpPost]
        [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public async Task<ResponseResult<List<FileDataModel>>> Excute([FromBody] CoaPrintExportExecuteModel param)
        {
            var result = await _printCoaCommand.ExcuteData(this.ControllerContext, _configuration, param);
            return ResponseResult<List<FileDataModel>>.Success(result);
        }
    }
}
