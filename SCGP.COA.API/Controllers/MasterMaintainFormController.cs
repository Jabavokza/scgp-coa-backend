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
    public class MasterMaintainFormController : ControllerBase
    {
        public IMasterMaintainFormCommand _masterCommand;

        public MasterMaintainFormController(IMasterMaintainFormCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainFormModel>> Create([FromBody] MasterMaintainFormModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainFormModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainFormSearchResultModel>>> Search([FromBody] MasterMaintainFormSearchCriterialModel param)
        {
            var data = _masterCommand.SearchData(param);
            return ResponseResult<SearchResModel<MasterMaintainFormSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<MasterMaintainFormModel>> Get([FromQuery] int FormId)
        {
            var data = _masterCommand.GetData(FormId);
            return ResponseResult<SearchResModel<MasterMaintainFormModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainFormModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainFormModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.MasterDataMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int FormId)
        {
            _masterCommand.DeleteData(FormId);
            return ResponseResult<SearchResModel<MasterMaintainFormModel>>.Success("Delete Success");
        }

        [HttpGet]
        public async Task<ResponseResult<List<SelectItemModel<int>>>> GetPropertys()
        {
            var data = _masterCommand.GetPropertys();
            return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        }

        [HttpGet]
        public async Task<ResponseResult<List<SelectItemModel<int>>>> GetFormTemplates()
        {
            var data = _masterCommand.GetFormTemplates();
            return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        }
    }
}
