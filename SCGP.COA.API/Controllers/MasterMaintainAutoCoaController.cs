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
    public class MasterMaintainAutoCoaController : ControllerBase
    {
        public IMasterMaintainAutoCoaCommand _masterCommand;

        public MasterMaintainAutoCoaController(IMasterMaintainAutoCoaCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        //[Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<MasterMaintainAutoCoaModel>> Create([FromBody] MasterMaintainAutoCoaModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainAutoCoaModel>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainAutoCoaSearchResultModel>>> Search()
        {
            var data = _masterCommand.SearchData();
            return ResponseResult<SearchResModel<MasterMaintainAutoCoaSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<MasterMaintainAutoCoaModel>> Get([FromQuery] int AutoCoaId)
        {
            var data = _masterCommand.GetData(AutoCoaId);
            return ResponseResult<SearchResModel<MasterMaintainAutoCoaModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainAutoCoaModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainAutoCoaModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int param)
        {
            _masterCommand.DeleteData(param);
            return ResponseResult<SearchResModel<MasterMaintainAutoCoaModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
