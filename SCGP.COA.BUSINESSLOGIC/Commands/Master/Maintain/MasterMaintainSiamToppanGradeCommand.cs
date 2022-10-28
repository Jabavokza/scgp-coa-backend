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
    public class MasterMaintainSiamToppanGradeCommand : IMasterMaintainSiamToppanGradeCommand
    {
        private IMasterMaintainSiamToppanGradeRepository _masterRepository;
        public MasterMaintainSiamToppanGradeCommand(IMasterMaintainSiamToppanGradeRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public SearchResModel<MasterMaintainSiamToppanGradeSearchResultModel> SearchData()
        {
            var query = _masterRepository.SearchData();

            var res = new SearchResModel<MasterMaintainSiamToppanGradeSearchResultModel>()
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

        public MasterMaintainSiamToppanGradeModel CreateData(MasterMaintainSiamToppanGradeModel req)
        {
            var res = _masterRepository.CreateData(req);
            return res;
        }

        public MasterMaintainSiamToppanGradeModel GetData(int req)
        {
            var res = _masterRepository.GetData(req);
            return res;
        }

        public MasterMaintainSiamToppanGradeModel UpdateData(MasterMaintainSiamToppanGradeModel req)
        {
            var res = _masterRepository.UpdateData(req);
            return res;
        }
        public MasterMaintainSiamToppanGradeModel DeleteData(int req)
        {
            var res = _masterRepository.DeleteData(req);
            return res;
        }
    }
}
