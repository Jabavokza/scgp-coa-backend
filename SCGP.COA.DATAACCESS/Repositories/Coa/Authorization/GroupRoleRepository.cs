using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization
{
    [ScopedRegistration]
    public class GroupRoleRepository : RepositoryBase<MASTER_GROUP_ROLE>, IGroupRoleRepository
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public GroupRoleRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }
    }
}
