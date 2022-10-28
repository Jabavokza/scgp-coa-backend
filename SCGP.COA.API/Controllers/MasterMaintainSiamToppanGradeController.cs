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
    public class MasterMaintainSiamToppanGradeController : ControllerBase
    {
        public IMasterMaintainSiamToppanGradeCommand _masterCommand;

        public MasterMaintainSiamToppanGradeController(IMasterMaintainSiamToppanGradeCommand masterCommand)
        {
            _masterCommand = masterCommand;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<MasterMaintainSiamToppanGradeModel>> Create([FromBody] MasterMaintainSiamToppanGradeModel param)
        {
            var data = _masterCommand.CreateData(param);
            return ResponseResult<MasterMaintainSiamToppanGradeModel>.Success(data);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<SearchResModel<MasterMaintainSiamToppanGradeSearchResultModel>>> Search()
        {
            var data = _masterCommand.SearchData();
            return ResponseResult<SearchResModel<MasterMaintainSiamToppanGradeSearchResultModel>>.Success(data);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.UserView)]
        public async Task<ResponseResult<MasterMaintainSiamToppanGradeModel>> Get([FromQuery] int SiamToppanGradeId)
        {
            var data = _masterCommand.GetData(SiamToppanGradeId);
            return ResponseResult<SearchResModel<MasterMaintainSiamToppanGradeModel>>.Success(data);
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Update([FromBody] MasterMaintainSiamToppanGradeModel param)
        {
            _masterCommand.UpdateData(param);
            return ResponseResult<SearchResModel<MasterMaintainSiamToppanGradeModel>>.Success("Update Success");
        }

        [HttpPut]
        [Authorize(Roles = RoleConstant.UserMaintain)]
        public async Task<ResponseResult<string>> Delete([FromQuery] int SiamToppanGradeId)
        {
            _masterCommand.DeleteData(SiamToppanGradeId);
            return ResponseResult<SearchResModel<MasterMaintainSiamToppanGradeModel>>.Success("Delete Success");
        }

        //[HttpGet]
        //public async Task<ResponseResult<List<SelectItemModel<int>>>> Groups()
        //{
        //    var data = _masterCommand.GetGroups();
        //    return ResponseResult<SearchResModel<List<SelectItemModel<int>>>>.Success(data);
        //}
    }
}
