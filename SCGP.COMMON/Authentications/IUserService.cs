using SCGP.COA.COMMON.Models;

namespace SCGP.COA.COMMON.Authentications
{
    public interface IUserLocalService
    {
        UserCredential GetUserCredential(string tokne = "");
        string GetAuthToken();
        HttpContext GetHttpContext();



    }
}
