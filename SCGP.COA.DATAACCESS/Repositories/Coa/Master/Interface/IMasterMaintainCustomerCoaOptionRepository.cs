using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainCustomerCoaOptionRepository : IRepositoryBase<MasterMaintainCustomerCoaOption>
    {
        IQueryable<MasterMaintainCustomerCoaOptionSearchResultModel> SearchData();
        MasterMaintainCustomerCoaOptionModel CreateData(MasterMaintainCustomerCoaOptionModel request);
        MasterMaintainCustomerCoaOptionModel GetData(int DataId);
        MasterMaintainCustomerCoaOptionModel UpdateData(MasterMaintainCustomerCoaOptionModel request);
        MasterMaintainCustomerCoaOptionModel DeleteData(int DataId);
    }
}