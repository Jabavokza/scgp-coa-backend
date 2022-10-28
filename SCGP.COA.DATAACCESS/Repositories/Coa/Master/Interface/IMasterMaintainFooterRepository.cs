using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainFooterRepository : IRepositoryBase<MasterMaintainFooter>
    {
        IQueryable<MasterMaintainFooterSearchResultModel> SearchData(MasterMaintainFooterSearchCriterialModel request);
        MasterMaintainFooterModel CreateData(MasterMaintainFooterModel request);
        MasterMaintainFooterModel GetData(int DataId);
        MasterMaintainFooterModel UpdateData(MasterMaintainFooterModel request);
        MasterMaintainFooterModel DeleteData(int DataId);
    }
}