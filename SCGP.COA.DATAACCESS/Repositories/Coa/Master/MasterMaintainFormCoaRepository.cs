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
    public class MasterMaintainFormCoaRepository : RepositoryBase<MasterMaintainFormCoa>, IMasterMaintainFormCoaRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainFormCoaRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainFormCoaSearchResultModel> SearchData()
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_FORM_COA
                            where u.IsActive == true
                            select new MasterMaintainFormCoaSearchResultModel
                            {
                                FormCoaId = u.FormCoaId,
                                SequenceNo = u.SequenceNo,
                                Grade = u.Grade,
                                Gram = u.Gram,
                                MaterialSale = u.MaterialSale,
                                CustomerCode = u.CustomerCode,
                                FormPdfId = u.FormPdfId,
                                FormTextId = u.FormTextId,
                                FormExcelId = u.FormExcelId,
                            };

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormCoaModel CreateData(MasterMaintainFormCoaModel request)
        {
            try
            {
                MasterMaintainFormCoa data = new MasterMaintainFormCoa()
                {
                    SequenceNo = request.SequenceNo,
                    Grade = request.Grade,
                    Gram = request.Gram,
                    MaterialSale = request.MaterialSale,
                    CustomerCode = request.CustomerCode,
                    FormPdfId = request.FormPdfId,
                    FormTextId = request.FormTextId,
                    FormExcelId = request.FormExcelId,
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_FORM_COA.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainFormCoaModel()
                {
                    FormCoaId = data.FormCoaId,
                    SequenceNo = data.SequenceNo,
                    Grade = data.Grade,
                    Gram = data.Gram,
                    MaterialSale = data.MaterialSale,
                    CustomerCode = data.CustomerCode,
                    FormPdfId = data.FormPdfId,
                    FormTextId = data.FormTextId,
                    FormExcelId = data.FormExcelId,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormCoaModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_FORM_COA
                           where u.FormCoaId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainFormCoaModel
                           {
                               FormCoaId = u.FormCoaId,
                               SequenceNo = u.SequenceNo,
                               Grade = u.Grade,
                               Gram = u.Gram,
                               MaterialSale = u.MaterialSale,
                               CustomerCode = u.CustomerCode,
                               FormPdfId = u.FormPdfId,
                               FormTextId = u.FormTextId,
                               FormExcelId = u.FormExcelId,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormCoaModel UpdateData(MasterMaintainFormCoaModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FORM_COA
                                  where u.FormCoaId == request.FormCoaId &&
                                        u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.SequenceNo = request.SequenceNo;
                updateData.Grade = request.Grade;
                updateData.Gram = request.Gram;
                updateData.MaterialSale = request.MaterialSale;
                updateData.CustomerCode = request.CustomerCode;
                updateData.FormPdfId = request.FormPdfId;
                updateData.FormTextId = request.FormTextId;
                updateData.FormExcelId = request.FormExcelId;

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

        public MasterMaintainFormCoaModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FORM_COA
                                  where u.FormCoaId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainFormCoaModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}