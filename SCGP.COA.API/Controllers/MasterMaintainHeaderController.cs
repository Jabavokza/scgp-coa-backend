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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MasterMaintainHeaderController : ControllerBase
    {
        public IMasterMaintainHeaderCommand _masterCommand;

        public MasterMaintainHeaderController(IMasterMaintainHeaderCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainHeaderModel>> Create([FromBody] MasterMaintainHeaderModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainHeaderModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainHeaderSearchResultModel>>> Search([FromBody] MasterMaintainHeaderSearchCriterialModel param)
        {
            var data = _masterCommand.SearchData(param);
            return ResponseResult<SearchResModel<MasterMaintainHeaderSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainHeaderModel>> Get([FromQuery] int HeaderId)
        {
            var data = _masterCommand.GetData(HeaderId);
            return ResponseResult<SearchResModel<MasterMaintainHeaderModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainHeaderModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainHeaderModel>>.Success("Update Success");
        }

        [HttpDelete]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int HeaderId)
        {
            _masterCommand.DeleteData(HeaderId);
            return ResponseResult<SearchResModel<MasterMaintainHeaderModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
