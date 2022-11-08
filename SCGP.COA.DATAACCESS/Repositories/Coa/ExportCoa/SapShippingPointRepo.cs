using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa
{
    [ScopedRegistration]
    public class SapShippingPointRepo : RepositoryBase<ConvertingBatchDatum>,ISapShippingPointRepo
    {
        private new readonly DbDataContext _db;
        private new readonly DbReadDataContext _dbRead;
        public SapShippingPointRepo( DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }
        public IQueryable<SAPShippingPoint> GetShippingPoints()
        {
            try
            {
                var oRes=_db.SAPShippingPoints.AsQueryable();
                return oRes;
            }
            catch (Exception oEx)
            {
                //_logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}
