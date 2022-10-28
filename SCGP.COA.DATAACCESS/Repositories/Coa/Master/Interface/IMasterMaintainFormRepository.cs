using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainFormRepository : IRepositoryBase<MasterMaintainForm>
    {
        IQueryable<MasterMaintainFormSearchResultModel> SearchData(MasterMaintainFormSearchCriterialModel request);
        MasterMaintainFormModel CreateData(MasterMaintainFormModel request);
        MasterMaintainFormModel GetData(int DataId);
        MasterMaintainFormModel UpdateData(MasterMaintainFormModel request);
        MasterMaintainFormModel DeleteData(int DataId);
        List<SelectItemModel<int>> GetPropertys();
        List<SelectItemModel<int>> GetFormTemplates();

    }
}