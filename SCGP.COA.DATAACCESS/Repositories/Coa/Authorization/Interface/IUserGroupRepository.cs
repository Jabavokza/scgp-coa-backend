using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface
{
    public interface IUserGroupRepository : IRepositoryBase<MASTER_USER_GROUP>
    {
        IQueryable<MASTER_USER_GROUP> FindActiveUserGroup(Guid userId);
    }
}
