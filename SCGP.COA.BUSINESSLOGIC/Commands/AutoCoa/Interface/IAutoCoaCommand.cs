using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.BUSINESSLOGIC.Commands.AutoCoa.Interface
{
    public interface IAutoCoaCommand
    {
        public SearchResModel<LogCoa> SearchAutoCoa(SearchReqModel<AutoCoaLogModel> req);
    }
}
