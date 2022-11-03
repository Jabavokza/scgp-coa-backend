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
    public class MasterMaintainFooterController : ControllerBase
    {
        public IMasterMaintainFooterCommand _masterCommand;

        public MasterMaintainFooterController(IMasterMaintainFooterCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainFooterModel>> Create([FromBody] MasterMaintainFooterModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainFooterModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainFooterSearchResultModel>>> Search([FromBody] MasterMaintainFooterSearchCriterialModel param)
        {
            var data = _masterCommand.SearchData(param);
            return ResponseResult<SearchResModel<MasterMaintainFooterSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainFooterModel>> Get([FromQuery] int FooterId)
        {
            var data = _masterCommand.GetData(FooterId);
            return ResponseResult<SearchResModel<MasterMaintainFooterModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainFooterModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainFooterModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int FooterId)
        {
            _masterCommand.DeleteData(FooterId);
            return ResponseResult<SearchResModel<MasterMaintainFooterModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
