using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.AutoCoa.Interface
{
    public interface IAutoCoaLogRepo
    {
        public IQueryable<LogCoa> SearchAutoCoa(AutoCoaLogModel request);
    }
}
