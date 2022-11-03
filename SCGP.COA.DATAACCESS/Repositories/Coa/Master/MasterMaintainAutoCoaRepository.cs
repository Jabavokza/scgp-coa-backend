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
    public class MasterMaintainAutoCoaRepository : RepositoryBase<MasterMaintainAutoCoa>, IMasterMaintainAutoCoaRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainAutoCoaRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainAutoCoaSearchResultModel> SearchData()
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_AUTO_COA
                            where u.IsActive == true
                            select new MasterMaintainAutoCoaSearchResultModel
                            {
                                AutoCoaId = u.AutoCoaId,
                                CustomerCode = u.CustomerCode,
                                ShipToCode = u.ShipToCode,
                                AutocoaActive = u.AutocoaActive,
                                editData = false,
                            };

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
        public MasterMaintainAutoCoaModel CreateData(MasterMaintainAutoCoaModel request)
        {
            try
            {
                MasterMaintainAutoCoa data = new MasterMaintainAutoCoa()
                {
                    CustomerCode = request.CustomerCode.Trim(),
                    ShipToCode = request.ShipToCode.Trim(),
                    AutocoaActive = request.AutocoaActive,
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_AUTO_COA.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainAutoCoaModel()
                {
                    AutoCoaId = data.AutoCoaId,
                    CustomerCode = data.CustomerCode,
                    ShipToCode = data.ShipToCode,
                    AutocoaActive = data.AutocoaActive,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainAutoCoaModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_AUTO_COA
                           where u.AutoCoaId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainAutoCoaModel
                           {
                               AutoCoaId = u.AutoCoaId,
                               ShipToCode = u.ShipToCode,
                               AutocoaActive = u.AutocoaActive,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainAutoCoaModel UpdateData(MasterMaintainAutoCoaModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_AUTO_COA
                                  where u.AutoCoaId == request.AutoCoaId &&
                                        u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.CustomerCode = request.CustomerCode.Trim();
                updateData.ShipToCode = request.ShipToCode.Trim();
                updateData.AutocoaActive = request.AutocoaActive;

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
        public MasterMaintainAutoCoaModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_AUTO_COA
                                  where u.AutoCoaId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainAutoCoaModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}