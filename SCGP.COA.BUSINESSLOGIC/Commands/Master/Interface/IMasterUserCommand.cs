using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterUserCommand
    {
        UserCreateResultModel CreateUser(UserModel request);
        SearchResModel<UserSearchResultModel> SearchUser(SearchReqModel<UserSearchCriterialModel> req);
        UserModel GetUser(GetUserDetailQuery req);
        void UpdateUser(UserModel userModel);
        void SetInActive(UserSearchResultModel userModel);
        List<SelectItemModel<int>> GetGroups();
    }
}
