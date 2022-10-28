using SCGP.COA.BUSINESSLOGIC.Commands.Master.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Authentications;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Exceptions;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface;
using System.Collections.Generic;

namespace SCGP.COA.BUSINESSLOGIC.Commands.Master
{
    [TransientRegistration]
    public class MasterMaintainFormCommand : IMasterMaintainFormCommand
    {
        private IMasterMaintainFormRepository _masterRepository;
        public MasterMaintainFormCommand(IMasterMaintainFormRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public SearchResModel<MasterMaintainFormSearchResultModel> SearchData(MasterMaintainFormSearchCriterialModel req)
        {
            var query = _masterRepository.SearchData(req);

            var res = new SearchResModel<MasterMaintainFormSearchResultModel>()
            {
                PageIndex = 1,
                PageSize = 10
            };
            var totalRecord = query.Count();
            var data = query.Page(res.PageSize, res.PageIndex).ToList();

            res.TotalRecord = totalRecord;
            res.Result = data;

            return res;
        }

        public MasterMaintainFormModel CreateData(MasterMaintainFormModel req)
        {
            var res = _masterRepository.CreateData(req);
            return res;
        }

        public MasterMaintainFormModel GetData(int req)
        {
            var res = _masterRepository.GetData(req);
            return res;
        }

        public MasterMaintainFormModel UpdateData(MasterMaintainFormModel req)
        {
            var res = _masterRepository.UpdateData(req);
            return res;
        }
        public MasterMaintainFormModel DeleteData(int req)
        {
            var res = _masterRepository.DeleteData(req);
            return res;
        }
        public List<SelectItemModel<int>> GetPropertys()
        {
            var res = _masterRepository.GetPropertys();
            return res;
        }
        public List<SelectItemModel<int>> GetFormTemplates()
        {
            var res = _masterRepository.GetFormTemplates();
            return res;
        }
    }
}
