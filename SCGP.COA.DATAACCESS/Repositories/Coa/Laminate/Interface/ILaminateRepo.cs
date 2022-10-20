using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;
using System.Collections;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Laminate.Interface
{
    public interface ILaminateRepo
    {
        public IQueryable<DataLabModel> SearchDisplayLab(DataLabModel request);
        public bool SetExcelToDataTable(List<ConvertingBatchDatum> request);
        public IQueryable<MasterSiamToppanGrade> GetMasterLabData();
        public IQueryable<ConvertingBatchDatum> GetConvertingBatchDat();
    }
}
