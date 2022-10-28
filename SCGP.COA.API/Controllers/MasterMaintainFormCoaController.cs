using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MasterMaintainFormCoaController : ControllerBase
    {
        public IMasterMaintainFormCoaCommand _masterCommand;

        public MasterMaintainFormCoaController(IMasterMaintainFormCoaCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        //[Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<MasterMaintainFormCoaModel>> Create([FromBody] MasterMaintainFormCoaModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainFormCoaModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainFormCoaSearchResultModel>>> Search()
        {
            var data = _masterCommand.SearchData();
            return ResponseResult<SearchResModel<MasterMaintainFormCoaSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<MasterMaintainFormCoaModel>> Get([FromQuery] int FormCoaId)
        {
            var data = _masterCommand.GetData(FormCoaId);
            return ResponseResult<SearchResModel<MasterMaintainFormCoaModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainFormCoaModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainFormCoaModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int FormCoaId)
        {
            _masterCommand.DeleteData(FormCoaId);
            return ResponseResult<SearchResModel<MasterMaintainFormCoaModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
