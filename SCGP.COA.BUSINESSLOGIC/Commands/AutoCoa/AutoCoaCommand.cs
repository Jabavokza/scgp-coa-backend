using SCGP.COA.BUSINESSLOGIC.Commands.AutoCoa.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.AutoCoa.Interface;

namespace SCGP.COA.BUSINESSLOGIC.Commands.AutoCoa
{
    [TransientRegistration]
    public class AutoCoaCommand : IAutoCoaCommand
    {
        private IAutoCoaLogRepo _autoCoaLogRepo;
        public AutoCoaCommand(IAutoCoaLogRepo autoCoaLogRepo)
        {
            _autoCoaLogRepo = autoCoaLogRepo;
        }
        public SearchResModel<LogCoa> SearchAutoCoa(SearchReqModel<AutoCoaLogModel> req)
        {
              var query = _autoCoaLogRepo.SearchAutoCoa(req.Criteria);
            //List<LogCoa> query = new();
            //string[] aDocumentType = { "Delivery", "EO", "PI" };
            //string[] aOutputType = { "PDF", "Excel", "Text" };
            //for (int i = 1; i < 20; i++)
            //{
            //    query.Add(new LogCoa
            //    {
            //        LogTimestamp = DateTime.Now,
            //        DocumentType = "DocType-0" + i,
            //        DocumentNumber = "Delivery",
            //        OutputType = "Excel",
            //        Status = "Sta-0" + i,
            //        Message = "Msg-0" + i,
            //    });
            //}
            var res = new SearchResModel<LogCoa>()
            {
                PageIndex = req.PageIndex ?? 1,
                PageSize = req.PageSize ?? 10
            };
            var totalRecord = query.Count();
            var data = query.Page(res.PageSize, res.PageIndex).ToList();

            res.TotalRecord = totalRecord;
            res.Result = data;

            return res;
        }
    }
}
