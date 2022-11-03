using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.AutoCoa.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    public class AutoCoaController : ControllerBase
    {
       private IAutoCoaCommand _autoCoaCommand;

        public AutoCoaController(IAutoCoaCommand AutoCoaCommand)
        {
          _autoCoaCommand = AutoCoaCommand;
        }
        [HttpPost]
        [Authorize(Roles = RoleConstant.UserView)]
        public ResponseResult<SearchResModel<LogCoa>> Search([FromBody] SearchReqModel<AutoCoaLogModel> param)
        {
            var data = _autoCoaCommand.SearchAutoCoa(param);
            return ResponseResult<SearchResModel<LogCoa>>.Success(data);
        }
        //[HttpPost]
        //[Authorize(Roles = RoleConstant.UserView)]
        //public bool Search([FromBody] SearchReqModel<AutoCoaLogModel> param)
        //{
        //   var data = _autoCoaCommand.SearchAutoCoa(param);
        //    return true;
        //}
    }
}
