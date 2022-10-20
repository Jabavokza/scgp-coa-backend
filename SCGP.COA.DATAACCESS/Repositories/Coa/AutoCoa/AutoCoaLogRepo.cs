using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.AutoCoa.Interface;
using System.Diagnostics;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.AutoCoa
{
    [ScopedRegistration]
    public class AutoCoaLogRepo : RepositoryBase<LogCoa>, IAutoCoaLogRepo
    {
        private readonly ILogger _logger;
        private DbDataContext _db;
        private DbReadDataContext _dbRead;
        public AutoCoaLogRepo(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }
        public IQueryable<LogCoa> SearchAutoCoa(AutoCoaLogModel request)
        {
            try
            {
                var query = _db.LogCoas.AsQueryable();

                if (request.jobTimestampFrom.HasValue)
                {
                    query = query.Where(w => w.LogTimestamp >= request.jobTimestampFrom);
                }              
                if (request.jobTimestampTo.HasValue)
                {
                    var jobto = request.jobTimestampTo!.Value.AddDays(1);
                    query = query.Where(w => w.LogTimestamp <= jobto);
                }
                if (!string.IsNullOrEmpty(request.documentNumber))
                {
                    query = query.Where(w => w.DocumentNumber == request.documentNumber);
                }
                if (request.documentType !=null && request.documentType.Length > 0)
                {
                    query = query.Where(w => request.documentType.Contains(w.DocumentType));
                }
                if (request.outputType != null && request.outputType.Length > 0)
                {
                    query = query.Where(w => request.outputType.Contains(w.OutputType));
                }
                return query.AsQueryable();
            }
            catch (Exception oEx)
            {
               // _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}
