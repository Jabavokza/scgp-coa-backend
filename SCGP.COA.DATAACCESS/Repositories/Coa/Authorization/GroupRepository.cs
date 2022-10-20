using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System.Diagnostics;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization
{
    [ScopedRegistration]
    public class GroupRepository : RepositoryBase<MASTER_GROUP>, IGroupRepository
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public GroupRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<GroupSearchResultModel> SearchGroup(GroupSearchCriterialModel search, bool isAdmin)
        {
            var query = from r in _dbRead.MASTER_GROUPS
                        select r;
            if (!string.IsNullOrEmpty(search.GroupName))
                query = query.Where(q => q.GroupName.Contains(search.GroupName));
            if (!isAdmin)
                query = query.Where(q => !q.IsAdmin);

            var res = query.Select(x => new GroupSearchResultModel()
            {
                GroupId = x.GroupId,
                GroupName = x.GroupName,
            });

            return res;
        }

    }
}
