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
    public class MasterMaintainHeaderRepository : RepositoryBase<MasterMaintainHeader>, IMasterMaintainHeaderRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainHeaderRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainHeaderSearchResultModel> SearchData(MasterMaintainHeaderSearchCriterialModel request)
        {
            try
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_HEADER
                            where u.IsActive == true
                            select new MasterMaintainHeaderSearchResultModel
                            {
                                HeaderId = u.HeaderId,
                                FormNumber = u.FormName,
                                PrintedDateTimeFormat = u.DatetimeFormat,
                            };

                if (!string.IsNullOrEmpty(request.PrintedDateTimeFormat))
                {
                    query = query.Where(w => w.PrintedDateTimeFormat.Contains(request.PrintedDateTimeFormat));
                }
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
        public MasterMaintainHeaderModel CreateData(MasterMaintainHeaderModel request)
        {
            try
            {
                MasterMaintainHeader data = new MasterMaintainHeader()
                {
                    DatetimeFormat = request.DatetimeFormat,
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_HEADER.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainHeaderModel()
                {
                    HeaderId = data.HeaderId,
                    DatetimeFormat = data.DatetimeFormat,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainHeaderModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_HEADER
                           where u.HeaderId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainHeaderModel
                           {
                               HeaderId = u.HeaderId,
                               DatetimeFormat = u.DatetimeFormat,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainHeaderModel UpdateData(MasterMaintainHeaderModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_HEADER
                                  where u.HeaderId == request.HeaderId &&
                                        u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.DatetimeFormat = request.DatetimeFormat;

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

        public MasterMaintainHeaderModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_HEADER
                                  where u.HeaderId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainHeaderModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}