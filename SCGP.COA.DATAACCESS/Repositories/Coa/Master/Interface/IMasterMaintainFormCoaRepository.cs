using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainFormCoaRepository : IRepositoryBase<MasterMaintainFormCoa>
    {
        IQueryable<MasterMaintainFormCoaSearchResultModel> SearchData();
        MasterMaintainFormCoaModel CreateData(MasterMaintainFormCoaModel request);
        MasterMaintainFormCoaModel GetData(int DataId);
        MasterMaintainFormCoaModel UpdateData(MasterMaintainFormCoaModel request);
        MasterMaintainFormCoaModel DeleteData(int DataId);
    }
}