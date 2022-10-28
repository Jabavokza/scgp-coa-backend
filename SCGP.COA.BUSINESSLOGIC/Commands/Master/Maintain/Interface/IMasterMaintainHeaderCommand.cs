using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainHeaderCommand
    {
        SearchResModel<MasterMaintainHeaderSearchResultModel> SearchData(MasterMaintainHeaderSearchCriterialModel req);
        MasterMaintainHeaderModel CreateData(MasterMaintainHeaderModel req);
        MasterMaintainHeaderModel GetData(int req);
        MasterMaintainHeaderModel UpdateData(MasterMaintainHeaderModel req);
        MasterMaintainHeaderModel DeleteData(int req);
    }
}
