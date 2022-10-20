using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SCGP.COA.COMMON.Authentications
{
    public interface ITokenManager
    {
        string GetClaim(ClaimStore key, string token);
        List<string> GetClaimList(ClaimStore key, string token);
        string ValidateToken(string token);
        ClaimsPrincipal GetPrincipal(string token);
    }
}
