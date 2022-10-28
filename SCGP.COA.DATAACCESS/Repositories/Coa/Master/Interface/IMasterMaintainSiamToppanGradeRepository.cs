using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface
{
    public interface IMasterMaintainSiamToppanGradeRepository : IRepositoryBase<MasterMaintainSiamToppanGrade>
    {
        IQueryable<MasterMaintainSiamToppanGradeSearchResultModel> SearchData();
        MasterMaintainSiamToppanGradeModel CreateData(MasterMaintainSiamToppanGradeModel request);
        MasterMaintainSiamToppanGradeModel GetData(int DataId);
        MasterMaintainSiamToppanGradeModel UpdateData(MasterMaintainSiamToppanGradeModel request);
        MasterMaintainSiamToppanGradeModel DeleteData(int DataId);
    }
}