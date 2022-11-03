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
    public class UserController : ControllerBase
    {
        public IMasterUserCommand _masterUserCommand;

        public UserController(IMasterUserCommand masterUserCommand)
        {
            _masterUserCommand = masterUserCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<UserCreateResultModel>> Create([FromBody] UserModel param)
        {
            var data = _masterUserCommand.CreateUser(param);
            return ResponseResult<UserCreateResultModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<SearchResModel<UserSearchResultModel>>> Search([FromBody] SearchReqModel<UserSearchCriterialModel> param)
        {
            var data = _masterUserCommand.SearchUser(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<UserModel>> Get([FromQuery] GetUserDetailQuery param)
        {
            var data = _masterUserCommand.GetUser(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] UserModel param)
        {
            _masterUserCommand.UpdateUser(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Delete([FromBody] UserSearchResultModel param)
        {
            _masterUserCommand.SetInActive(param);
            return ResponseResult<SearchResModel<UserSearchResultModel>>.Success("Set InActive Success");
        }

        [HttpGet]
        public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        {
            var data = _masterUserCommand.GetGroups();
            return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        }

    }
}
