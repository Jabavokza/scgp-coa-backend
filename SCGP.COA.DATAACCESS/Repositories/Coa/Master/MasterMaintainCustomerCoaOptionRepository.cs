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
    public class MasterMaintainCustomerCoaOptionRepository : RepositoryBase<MasterMaintainCustomerCoaOption>, IMasterMaintainCustomerCoaOptionRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainCustomerCoaOptionRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainCustomerCoaOptionSearchResultModel> SearchData()
        {
            try
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _db.MASTER_MAINTAIN_CUSTOMER_COA_OPTION
                            where u.IsActive == true
                            select new MasterMaintainCustomerCoaOptionSearchResultModel
                            {
                                CustomerCoaOptionId = u.CustomerCoaOptionId,
                                CustomerCode = u.CustomerCode,
                                CustomerName = u.CustomerName,
                                Pdf = u.DefaultOutputPdf,
                                Text = u.DefaultOutputText,
                                Excel = u.DefaultOutputExcel,
                                Dp = u.DefaultOutputDp,
                                DpBarcode = u.DefaultOutputDpBarcode,
                                AdditionFooterText = u.CoaFooterText,
                                EditData = false,
                            };

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
        public MasterMaintainCustomerCoaOptionModel CreateData(MasterMaintainCustomerCoaOptionModel request)
        {
            try
            {
                MasterMaintainCustomerCoaOption data = new MasterMaintainCustomerCoaOption()
                {
                    CustomerCode = request.CustomerCode.Trim(),
                    CustomerName = request.CustomerName.Trim(),
                    DefaultOutputPdf = request.Pdf,
                    DefaultOutputText = request.Text,
                    DefaultOutputExcel = request.Excel,
                    DefaultOutputDp = request.Dp,
                    DefaultOutputDpBarcode = request.DpBarcode,
                    CoaFooterText = request.AdditionFooterText.Trim(),
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_CUSTOMER_COA_OPTION.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainCustomerCoaOptionModel()
                {
                    CustomerCoaOptionId = data.CustomerCoaOptionId,
                    CustomerName = data.CustomerName,
                    Pdf = data.DefaultOutputPdf,
                    Text = data.DefaultOutputText,
                    Excel = data.DefaultOutputExcel,
                    Dp = data.DefaultOutputDp,
                    DpBarcode = data.DefaultOutputDpBarcode,
                    AdditionFooterText = data.CoaFooterText,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainCustomerCoaOptionModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_CUSTOMER_COA_OPTION
                           where u.CustomerCoaOptionId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainCustomerCoaOptionModel
                           {
                               CustomerCoaOptionId = u.CustomerCoaOptionId,
                               CustomerName = u.CustomerName,
                               Pdf = u.DefaultOutputPdf,
                               Text = u.DefaultOutputText,
                               Excel = u.DefaultOutputExcel,
                               Dp = u.DefaultOutputDp,
                               DpBarcode = u.DefaultOutputDpBarcode,
                               AdditionFooterText = u.CoaFooterText,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainCustomerCoaOptionModel UpdateData(MasterMaintainCustomerCoaOptionModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_CUSTOMER_COA_OPTION
                                  where u.CustomerCoaOptionId == request.CustomerCoaOptionId &&
                                        u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.CustomerCode = request.CustomerCode.Trim();
                updateData.CustomerName = request.CustomerName.Trim();
                updateData.DefaultOutputPdf = request.Pdf;
                updateData.DefaultOutputText = request.Text;
                updateData.DefaultOutputExcel = request.Excel;
                updateData.DefaultOutputDp = request.Dp;
                updateData.DefaultOutputDpBarcode = request.DpBarcode;
                updateData.CoaFooterText = request.AdditionFooterText.Trim();

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
        public MasterMaintainCustomerCoaOptionModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_CUSTOMER_COA_OPTION
                                  where u.CustomerCoaOptionId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainCustomerCoaOptionModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}