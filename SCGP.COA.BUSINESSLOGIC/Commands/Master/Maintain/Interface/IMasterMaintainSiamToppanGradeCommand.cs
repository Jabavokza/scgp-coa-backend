using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface
{
    public interface IMasterMaintainSiamToppanGradeCommand
    {
        SearchResModel<MasterMaintainSiamToppanGradeSearchResultModel> SearchData();
        MasterMaintainSiamToppanGradeModel CreateData(MasterMaintainSiamToppanGradeModel req);
        MasterMaintainSiamToppanGradeModel GetData(int req);
        MasterMaintainSiamToppanGradeModel UpdateData(MasterMaintainSiamToppanGradeModel req);
        MasterMaintainSiamToppanGradeModel DeleteData(int req);
    }
}
