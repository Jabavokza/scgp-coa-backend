using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    public class AuthrorizationController : ControllerBase
    {
        private IAuthrorizationCommand _authrorizationCommand;
        public AuthrorizationController(IAuthrorizationCommand authrorizationCommand)
        {
            _authrorizationCommand = authrorizationCommand;
        }

        [HttpPost]
        public ResponseResult<TokenModel> Token([FromBody] TokenQuery model)
        {

            var data = _authrorizationCommand.Token(model);

            return ResponseResult<TokenModel>.Success(data);

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ResponseResult<List<MenuTree>> GetMyMenu()
        {
            var data = _authrorizationCommand.GetMyMenu();

            return ResponseResult<List<MenuTree>>.Success(data);
        }
    }
}
