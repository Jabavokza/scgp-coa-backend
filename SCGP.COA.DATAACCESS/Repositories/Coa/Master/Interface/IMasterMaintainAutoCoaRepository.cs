using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainAutoCoaRepository : IRepositoryBase<MasterMaintainAutoCoa>
    {
        IQueryable<MasterMaintainAutoCoaSearchResultModel> SearchData();
        MasterMaintainAutoCoaModel CreateData(MasterMaintainAutoCoaModel request);
        MasterMaintainAutoCoaModel GetData(int DataId);
        MasterMaintainAutoCoaModel UpdateData(MasterMaintainAutoCoaModel request);
        MasterMaintainAutoCoaModel DeleteData(int DataId);
    }
}