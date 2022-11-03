using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Exceptions;
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
    public class MasterMaintainSiamToppanGradeRepository : RepositoryBase<MasterMaintainSiamToppanGrade>, IMasterMaintainSiamToppanGradeRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainSiamToppanGradeRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainSiamToppanGradeSearchResultModel> SearchData()
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE
                            where u.IsActive == true
                            select new MasterMaintainSiamToppanGradeSearchResultModel
                            {
                                SiamToppanGradeId = u.SiamToppanGradeId,
                                Grade = u.Grade,
                                Gram = u.Gram ?? 0,
                                //MaterialSale = u.MaterialSale,
                                SiamToppanNumber = u.SiamToppanNumber,
                                Remark = u.Remark,
                                editData = false
                            };

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainSiamToppanGradeModel CreateData(MasterMaintainSiamToppanGradeModel request)
        {
            try
            {
                MasterMaintainSiamToppanGrade data = new MasterMaintainSiamToppanGrade()
                {
                    Grade = request.Grade.Trim(),
                    Gram = request.Gram,
                    SiamToppanNumber = request.SiamToppanNumber.Trim(),
                    Remark = request.Remark.Trim(),
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainSiamToppanGradeModel()
                {
                    SiamToppanGradeId = data.SiamToppanGradeId,
                    Grade = data.Grade,
                    Gram = data.Gram ?? 0,
                    SiamToppanNumber = data.SiamToppanNumber,
                    Remark = data.Remark,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainSiamToppanGradeModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE
                          where u.SiamToppanGradeId == DataId &&
                                u.IsActive == true
                            select new MasterMaintainSiamToppanGradeModel
                            {
                                SiamToppanGradeId = u.SiamToppanGradeId,
                                Grade = u.Grade,
                                Gram = u.Gram ?? 0,
                                //MaterialSale = u.MaterialSale,
                                SiamToppanNumber = u.SiamToppanNumber,
                                Remark = u.Remark,
                            }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainSiamToppanGradeModel UpdateData(MasterMaintainSiamToppanGradeModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE
                                  where u.SiamToppanGradeId == request.SiamToppanGradeId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.Grade = request.Grade.Trim();
                updateData.Gram = request.Gram;
                updateData.SiamToppanNumber = request.SiamToppanNumber.Trim();
                updateData.Remark = request.Remark.Trim();

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

        public MasterMaintainSiamToppanGradeModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_SIAM_TOPPAN_GRADE
                                  where u.SiamToppanGradeId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainSiamToppanGradeModel() ;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}