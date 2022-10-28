using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainFormCommand
    {
        SearchResModel<MasterMaintainFormSearchResultModel> SearchData(MasterMaintainFormSearchCriterialModel req);
        MasterMaintainFormModel CreateData(MasterMaintainFormModel req);
        MasterMaintainFormModel GetData(int req);
        MasterMaintainFormModel UpdateData(MasterMaintainFormModel req);
        MasterMaintainFormModel DeleteData(int req);
        List<SelectItemModel<int>> GetPropertys();
        List<SelectItemModel<int>>GetFormTemplates();
    }
}
