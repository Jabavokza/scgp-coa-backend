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
    public class MasterMaintainCustomerCoaOptionController : ControllerBase
    {
        private IMasterMaintainCustomerCoaOptionCommand _masterCommand;

        public MasterMaintainCustomerCoaOptionController(IMasterMaintainCustomerCoaOptionCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainCustomerCoaOptionModel>> Create([FromBody] MasterMaintainCustomerCoaOptionModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainCustomerCoaOptionModel>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainCustomerCoaOptionSearchResultModel>>> Search()
        {
            var data = _masterCommand.SearchData();
            return ResponseResult<SearchResModel<MasterMaintainCustomerCoaOptionSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainCustomerCoaOptionModel>> Get([FromQuery] int CustomerCoaOptionId)
        {
            var data = _masterCommand.GetData(CustomerCoaOptionId);
            return ResponseResult<SearchResModel<MasterMaintainCustomerCoaOptionModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainCustomerCoaOptionModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainCustomerCoaOptionModel>>.Success("Update Success");
        }

        [HttpDelete]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int CustomerCoaOptionId)
        {
            _masterCommand.DeleteData(CustomerCoaOptionId);
            return ResponseResult<SearchResModel<MasterMaintainCustomerCoaOptionModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
