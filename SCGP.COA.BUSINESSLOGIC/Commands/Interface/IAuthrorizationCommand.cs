using SCGP.COA.BUSINESSLOGIC.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Interface
{
    public interface IAuthrorizationCommand
    {
        TokenModel Token(TokenQuery request);
        List<MenuTree> GetMyMenu();
    }
}
