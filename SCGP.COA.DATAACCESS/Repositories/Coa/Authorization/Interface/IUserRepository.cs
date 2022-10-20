using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface
{
    public interface IUserRepository : IRepositoryBase<MASTER_USER>
    {
        MASTER_USER FindActiveByUserNameAndDomainAndClient(string userName, string domain);
        MASTER_USER FindActiveByUserId(Guid userId);
        IQueryable<UserSearchResultModel> SearchUser(UserSearchCriterialModel request, bool isAdmin);
    }
}