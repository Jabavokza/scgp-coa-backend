using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterGroupCommand
    {
        GroupCreateResultModel CreateGroup(GroupDetailModel groupModel);
        SearchResModel<GroupSearchResultModel> SearchGroup(SearchReqModel<GroupSearchCriterialModel> req);
        GroupDetailModel GetGroup(GetGroupDetailQuery req);
        void UpdateGroup(GroupDetailModel groupModel);
        void SetInActive(GroupSearchResultModel userModel);
        List<MenuRoleModel> GetMenuRole();
    }
}
