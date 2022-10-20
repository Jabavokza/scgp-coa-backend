using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;
using System.Data;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PrintCoaDomesticController : ControllerBase
    {
        private IPrintCoaDomesticCommand _printCoaDomesticCommand;
        public PrintCoaDomesticController(IPrintCoaDomesticCommand printCoaCommand)
        {
            _printCoaDomesticCommand = printCoaCommand;
        }
        [HttpPost]
        // [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public async Task<ResponseResult<List<CoaPrintDomesticDataModel>>> Search([FromBody] CoaPrintDomesticSearchModel param)
        {
            var result =await _printCoaDomesticCommand.GetDPNumberDataAsync(param);
            return ResponseResult<List<CoaPrintDomesticDataModel>>.Success(result);
        }
        [HttpPost]
        //[Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<FileDataModel>> Print([FromBody] CoaPrintDomesticExecuteModel param) 
        {
            var result = _printCoaDomesticCommand.PrintExport(this.ControllerContext, param);
            return ResponseResult<List<FileDataModel>>.Success(result);
        }

        [HttpPost]

        //[Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<FileDataModel>>Save([FromBody] CoaPrintDomesticExecuteModel param)
        {
            var result = _printCoaDomesticCommand.SaveExport(this.ControllerContext, param);
            return ResponseResult<List<FileDataModel>>.Success(result);
        }

    }
}
