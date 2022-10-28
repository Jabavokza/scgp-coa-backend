using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainAutoCoaCommand
    {
        SearchResModel<MasterMaintainAutoCoaSearchResultModel> SearchData();
        MasterMaintainAutoCoaModel CreateData(MasterMaintainAutoCoaModel req);
        MasterMaintainAutoCoaModel GetData(int req);
        MasterMaintainAutoCoaModel UpdateData(MasterMaintainAutoCoaModel req);
        MasterMaintainAutoCoaModel DeleteData(int req);
    }
}
