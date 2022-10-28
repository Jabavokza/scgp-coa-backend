using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainFormCoaCommand
    {
        SearchResModel<MasterMaintainFormCoaSearchResultModel> SearchData();
        MasterMaintainFormCoaModel CreateData(MasterMaintainFormCoaModel req);
        MasterMaintainFormCoaModel GetData(int req);
        MasterMaintainFormCoaModel UpdateData(MasterMaintainFormCoaModel req);
        MasterMaintainFormCoaModel DeleteData(int req);
    }
}
