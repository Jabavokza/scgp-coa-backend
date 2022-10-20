using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface
{
    public interface IGroupRepository : IRepositoryBase<MASTER_GROUP>
    {
        IQueryable<GroupSearchResultModel> SearchGroup(GroupSearchCriterialModel search, bool isAdmin);
    }
}
