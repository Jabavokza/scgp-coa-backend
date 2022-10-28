using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainFooterCommand
    {
        SearchResModel<MasterMaintainFooterSearchResultModel> SearchData(MasterMaintainFooterSearchCriterialModel req);
        MasterMaintainFooterModel CreateData(MasterMaintainFooterModel req);
        MasterMaintainFooterModel GetData(int req);
        MasterMaintainFooterModel UpdateData(MasterMaintainFooterModel req);
        MasterMaintainFooterModel DeleteData(int req);
    }
}
