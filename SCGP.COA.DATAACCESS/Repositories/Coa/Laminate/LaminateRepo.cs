using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Repositories.Coa.Laminate.Interface;
using SCGP.COA.COMMON.Models;
using System.Diagnostics;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Laminate
{
    [ScopedRegistration]
    public class LaminateRepo : RepositoryBase<ConvertingBatchDatum>, ILaminateRepo
    {
        private readonly ILogger _logger;
        private readonly DbDataContext _db;
        private readonly DbReadDataContext _dbRead;
        public LaminateRepo(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }
        public IQueryable<DataLabModel> SearchDisplayLab(DataLabModel request)
        {
            //  Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                var query = _db.ConvertingBatchData.AsQueryable();

                if (!string.IsNullOrEmpty(request.batchNo))
                {
                    query = query.Where(w => w.Batch.Contains(request.batchNo));
                }
                if (!string.IsNullOrEmpty(request.grade))
                {
                    query = query.Where(w => w.Grade.Contains(request.grade));
                }
                if (request.gram.HasValue)
                {
                    query = query.Where(w => w.Gram == request.gram);
                }
                if (request.productionDate.HasValue)
                {
                    query = query.Where(w => w.ProductionDate!.Value.Year == request.productionDate.Value.Year && w.ProductionDate!.Value.Month == request.productionDate.Value.Month && w.ProductionDate!.Value.Date == request.productionDate.Value.Date);
                }
                return query.Select(s => new DataLabModel
                {
                    batchNo = s.Batch,
                    grade = s.Grade,
                    gram = s.Gram,
                    productionDate = s.ProductionDate,
                    filmThickness = s.FilmThickness,
                    porosity = s.Porosity,
                    uploadDate = s.UploadedDatetime
                });
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }

        }
        public bool SetExcelToDataTable(List<ConvertingBatchDatum> request)
        {
            //  Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                _db.ConvertingBatchData.AddRange(request);
                _db.SaveChanges();
                return true;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
        public IQueryable<MasterSiamToppanGrade> GetMasterLabData()
        {
            var res = _db.MasterSiamToppanGrades.AsQueryable();
            return res;
        }
        public IQueryable<ConvertingBatchDatum> GetConvertingBatchDat()
        {
            var res = _db.ConvertingBatchData.AsQueryable();
            return res;
        }
    }
}
