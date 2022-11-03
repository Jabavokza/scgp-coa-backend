using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupController : ControllerBase
    {
        public IMasterGroupCommand _command;

        public GroupController(IMasterGroupCommand command)
        {
            _command = command;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.GroupMaintain)]
        public async Task<ResponseResult<GroupCreateResultModel>> Create([FromBody] GroupDetailModel param)
        {
            var data = _command.CreateGroup(param);
            return ResponseResult<GroupCreateResultModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.GroupView)]
        public async Task<ResponseResult<SearchResModel<GroupSearchResultModel>>> Search([FromBody] SearchReqModel<GroupSearchCriterialModel> param)
        {
            var data = _command.SearchGroup(param);
            return ResponseResult<SearchResModel<GroupSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.GroupView)]
        public async Task<ResponseResult<GroupDetailModel>> Get([FromQuery] GetGroupDetailQuery param)
        {
            var data = _command.GetGroup(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.GroupMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] GroupDetailModel param)
        {
            _command.UpdateGroup(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.GroupMaintain)]
        public async Task<ResponseResult<string>> Delete([FromBody] GroupSearchResultModel param)
        {
            _command.SetInActive(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success("Set InActive Success");
        }

        [HttpGet]
        public async Task<ResponseResult<List<MenuRoleModel>>> GetMenuRoles()
        {
            var data = _command.GetMenuRole();
            return ResponseResult<SearchResModel<List<MenuRoleModel>>>.Success(data);
        }

    }
}
