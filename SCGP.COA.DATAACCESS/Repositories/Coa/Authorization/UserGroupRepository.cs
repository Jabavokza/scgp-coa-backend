using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization
{
    [ScopedRegistration]
    public class UserGroupRepository : RepositoryBase<MASTER_USER_GROUP>, IUserGroupRepository
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public UserGroupRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MASTER_USER_GROUP> FindActiveUserGroup(Guid userId)
        {
            var query = from ug in _dbRead.MASTER_USER_GROUPS

                        join g in _dbRead.MASTER_GROUPS
                        on ug.GroupId equals g.GroupId

                        where ug.UserId == userId
                        && g.ActiveFlag

                        select ug;

            return query;
        }
    }
}
