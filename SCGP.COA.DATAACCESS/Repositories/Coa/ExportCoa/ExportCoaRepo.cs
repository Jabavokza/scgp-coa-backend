using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa
{
    [ScopedRegistration]
    public class ExportCoaRepo : RepositoryBase<ConvertingBatchDatum>,IExportCoaRepo
    {
        private new readonly DbDataContext _db;
        private new readonly DbReadDataContext _dbRead;
        public ExportCoaRepo( DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }
        public bool SetDtBatchToDtTblRepo(List<ConvertingBatchDatum> request)
        {
            try
            {
                _db.ConvertingBatchData.AddRange(request);
                _db.SaveChanges();
                return true;
            }
            catch (Exception oEx)
            {
                //_logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}
