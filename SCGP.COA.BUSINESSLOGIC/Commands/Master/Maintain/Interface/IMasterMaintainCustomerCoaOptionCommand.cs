using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainCustomerCoaOptionCommand
    {
        SearchResModel<MasterMaintainCustomerCoaOptionSearchResultModel> SearchData();
        MasterMaintainCustomerCoaOptionModel CreateData(MasterMaintainCustomerCoaOptionModel req);
        MasterMaintainCustomerCoaOptionModel GetData(int req);
        MasterMaintainCustomerCoaOptionModel UpdateData(MasterMaintainCustomerCoaOptionModel req);
        MasterMaintainCustomerCoaOptionModel DeleteData(int req);
    }
}
