using Microsoft.AspNetCore.Identity;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;

namespace SCGP.COA.BUSINESSLOGIC.Services.Interface
{
    public interface IAuthrorizationService
    {
        MASTER_USER Login(TokenQuery request);
        bool AuthActiveDirectory(string server, string userName, string password);
        string RandomString(int length);
        string GenerateJWT(UserCredential user);
        string GenerateRandomPassword(PasswordOptions opts = null);
    }
}
