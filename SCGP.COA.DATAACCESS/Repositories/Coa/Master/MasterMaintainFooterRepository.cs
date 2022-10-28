using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Infrastructures;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Master.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.Authorization
{
    [ScopedRegistration]
    public class MasterMaintainFooterRepository : RepositoryBase<MasterMaintainFooter>, IMasterMaintainFooterRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainFooterRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainFooterSearchResultModel> SearchData(MasterMaintainFooterSearchCriterialModel request)
        {
            try
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_FOOTER
                            where u.IsActive == true
                            select new MasterMaintainFooterSearchResultModel
                            {
                                FooterId = u.FooterId,
                                FormNumber = u.FormName,
                            };

                if (!string.IsNullOrEmpty(request.FormNumber))
                {
                    query = query.Where(w => w.FormNumber.Contains(request.FormNumber));
                }

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
        public MasterMaintainFooterModel CreateData(MasterMaintainFooterModel request)
        {
            try
            {
                MasterMaintainFooter data = new()
                {
                    FormName = request.FormNumber,
                    TextTestcondition = request.TextTestcondition,
                    TextApprovedby = request.TextApprovedby,
                    TextPrintedby = request.TextPrintedby,
                    TextTelnumber = request.TextTelnumber,
                    TextAdditional1 = request.TextAdditional1,
                    TextAdditional2 = request.TextAdditional2,
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_FOOTER.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainFooterModel()
                {
                    FooterId = data.FooterId,
                    FormNumber = data.FormName,
                    TextTestcondition = data.TextTestcondition,
                    TextApprovedby = data.TextApprovedby,
                    TextPrintedby = data.TextPrintedby,
                    TextTelnumber = data.TextTelnumber,
                    TextAdditional1 = data.TextAdditional1,
                    TextAdditional2 = data.TextAdditional2,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFooterModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_FOOTER
                           where u.FooterId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainFooterModel
                           {
                               FooterId = u.FooterId,
                               FormNumber = u.FormName,
                               TextTestcondition = u.TextTestcondition,
                               TextApprovedby = u.TextApprovedby,
                               TextPrintedby = u.TextPrintedby,
                               TextTelnumber = u.TextTelnumber,
                               TextAdditional1 = u.TextAdditional1,
                               TextAdditional2 = u.TextAdditional2,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFooterModel UpdateData(MasterMaintainFooterModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FOOTER
                                  where u.FooterId == request.FooterId &&
                                        u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.FormName = request.FormNumber;
                updateData.TextTestcondition = request.TextTestcondition;
                updateData.TextApprovedby = request.TextApprovedby;
                updateData.TextPrintedby = request.TextPrintedby;
                updateData.TextTelnumber = request.TextTelnumber;
                updateData.TextAdditional1 = request.TextAdditional1;
                updateData.TextAdditional2 = request.TextAdditional2;

                _db.SaveChanges();

                var res = request;

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
        public MasterMaintainFooterModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FOOTER
                                  where u.FooterId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainFooterModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}