using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainHeaderRepository : IRepositoryBase<MasterMaintainHeader>
    {
        IQueryable<MasterMaintainHeaderSearchResultModel> SearchData(MasterMaintainHeaderSearchCriterialModel request);
        MasterMaintainHeaderModel CreateData(MasterMaintainHeaderModel request);
        MasterMaintainHeaderModel GetData(int DataId);
        MasterMaintainHeaderModel UpdateData(MasterMaintainHeaderModel request);
        MasterMaintainHeaderModel DeleteData(int DataId);
    }
}