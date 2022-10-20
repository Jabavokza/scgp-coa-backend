using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization
{
    [ScopedRegistration]
    public class UserRepository : RepositoryBase<MASTER_USER>, IUserRepository
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public UserRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public MASTER_USER FindActiveByUserNameAndDomainAndClient(string userName, string domain)
        {
            var query = from u in _db.MASTER_USERS
                        where u.NormalizedUserName == userName && u.Domain == domain && u.ActiveFlag == true
                        select u;
            return query.FirstOrDefault();
        }

        public MASTER_USER FindActiveByUserId(Guid userId)
        {
            var query = _db.MASTER_USERS
                .Where(u => u.UserId == userId && u.ActiveFlag == true);
            return query.FirstOrDefault();
        }

        public IQueryable<UserSearchResultModel> SearchUser(UserSearchCriterialModel request, bool isAdmin)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var query = from u in _dbRead.MASTER_USERS

                        join a in _dbRead.MASTER_USER_GROUPS 
                        on u.UserId equals a.UserId
                        join g in _dbRead.MASTER_GROUPS 

                        on a.GroupId equals g.GroupId into groupLst
                        from gLst in groupLst.DefaultIfEmpty()

                        where (!request.Group.HasValue || a.GroupId == request.Group)

                        select new UserSearchResultModel
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserName = u.UserName,
                            ShortUserName = u.NormalizedUserName,
                            IsAdmin = gLst.IsAdmin
                        };

            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(w => w.UserName.Contains(request.UserName));
            }
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(w => w.FirstName.Contains(request.FirstName));
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                query = query.Where(w => w.LastName.Contains(request.LastName));
            }
            if (!isAdmin)
            {
                query = query.Where(q => q.IsAdmin != true);
            }

            return query;
        }
    }
}