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
    public class MasterMaintainFormRepository : RepositoryBase<MasterMaintainForm>, IMasterMaintainFormRepository
    {
        private readonly ILogger _logger;
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public MasterMaintainFormRepository(DbDataContext db, DbReadDataContext DbRead) : base(db, DbRead)
        {
            _db = db;
            _dbRead = DbRead;
        }

        public IQueryable<MasterMaintainFormSearchResultModel> SearchData(MasterMaintainFormSearchCriterialModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var query = from u in _dbRead.MASTER_MAINTAIN_FORM
                            where u.IsActive == true
                            select new MasterMaintainFormSearchResultModel
                            {
                                FormId = u.FormId,
                                FormName = u.FormName,
                                FormTemplateId = u.FormTemplateId,
                                FormTemplate = u.FormTemplate.FormTemplateName,
                            };

                if (!string.IsNullOrEmpty(request.FormName))
                {
                    query = query.Where(w => w.FormName.Contains(request.FormName));
                }
                if (request.FormTemplateId != null && request.FormTemplateId > 0)
                {
                    query = query.Where(w => w.FormTemplateId == request.FormTemplateId);
                }

                return query;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormModel CreateData(MasterMaintainFormModel request)
        {
            try
            {
                MasterMaintainForm data = new MasterMaintainForm()
                {
                    FormName = request.FormNumber,
                    FormTemplateId = request.FormTemplateId,
                    Property01Id = request.Property1Id,
                    Property02Id = request.Property2Id,
                    Property03Id = request.Property3Id,
                    Property04Id = request.Property4Id,
                    Property05Id = request.Property5Id,
                    Property06Id = request.Property6Id,
                    Property07Id = request.Property7Id,
                    Property08Id = request.Property8Id,
                    Property09Id = request.Property9Id,
                    Property10Id = request.Property10Id,
                    Property11Id = request.Property11Id,
                    Property12Id = request.Property12Id,
                    Property13Id = request.Property13Id,
                    Property14Id = request.Property14Id,
                    Property15Id = request.Property15Id,
                    Property16Id = request.Property16Id,
                    Property17Id = request.Property17Id,
                    Property18Id = request.Property18Id,
                    Property19Id = request.Property19Id,
                    Property20Id = request.Property20Id,
                    IsActive = true,
                };
                _db.MASTER_MAINTAIN_FORM.Add(data);
                _db.SaveChanges();

                var res = new MasterMaintainFormModel()
                {
                    FormId = data.FormId,
                    FormNumber = data.FormName,
                    FormTemplateId = data.FormTemplateId,
                    Property1Id = data.Property01Id,
                    Property2Id = data.Property02Id,
                    Property3Id = data.Property03Id,
                    Property4Id = data.Property04Id,
                    Property5Id = data.Property05Id,
                    Property6Id = data.Property06Id,
                    Property7Id = data.Property07Id,
                    Property8Id = data.Property08Id,
                    Property9Id = data.Property09Id,
                    Property10Id = data.Property10Id,
                    Property11Id = data.Property11Id,
                    Property12Id = data.Property12Id,
                    Property13Id = data.Property13Id,
                    Property14Id = data.Property14Id,
                    Property15Id = data.Property15Id,
                    Property16Id = data.Property16Id,
                    Property17Id = data.Property17Id,
                    Property18Id = data.Property18Id,
                    Property19Id = data.Property19Id,
                    Property20Id = data.Property20Id,
                };

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormModel GetData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var res = (from u in _dbRead.MASTER_MAINTAIN_FORM
                           where u.FormId == DataId &&
                                 u.IsActive == true
                           select new MasterMaintainFormModel
                           {
                               FormNumber = u.FormName,
                               FormTemplateId = u.FormTemplateId,
                               Property1Id = u.Property01Id,
                               Property2Id = u.Property02Id,
                               Property3Id = u.Property03Id,
                               Property4Id = u.Property04Id,
                               Property5Id = u.Property05Id,
                               Property6Id = u.Property06Id,
                               Property7Id = u.Property07Id,
                               Property8Id = u.Property08Id,
                               Property9Id = u.Property09Id,
                               Property10Id = u.Property10Id,
                               Property11Id = u.Property11Id,
                               Property12Id = u.Property12Id,
                               Property13Id = u.Property13Id,
                               Property14Id = u.Property14Id,
                               Property15Id = u.Property15Id,
                               Property16Id = u.Property16Id,
                               Property17Id = u.Property17Id,
                               Property18Id = u.Property18Id,
                               Property19Id = u.Property19Id,
                               Property20Id = u.Property20Id,
                           }).FirstOrDefault();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public MasterMaintainFormModel UpdateData(MasterMaintainFormModel request)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FORM
                                  where u.FormId == request.FormId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.FormName = request.FormNumber;
                updateData.FormTemplateId = request.FormTemplateId;
                updateData.Property01Id = request.Property1Id;
                updateData.Property02Id = request.Property2Id;
                updateData.Property03Id = request.Property3Id;
                updateData.Property04Id = request.Property4Id;
                updateData.Property05Id = request.Property5Id;
                updateData.Property06Id = request.Property6Id;
                updateData.Property07Id = request.Property7Id;
                updateData.Property08Id = request.Property8Id;
                updateData.Property09Id = request.Property9Id;
                updateData.Property10Id = request.Property10Id;
                updateData.Property11Id = request.Property11Id;
                updateData.Property12Id = request.Property12Id;
                updateData.Property13Id = request.Property13Id;
                updateData.Property14Id = request.Property14Id;
                updateData.Property15Id = request.Property15Id;
                updateData.Property16Id = request.Property16Id;
                updateData.Property17Id = request.Property17Id;
                updateData.Property18Id = request.Property18Id;
                updateData.Property19Id = request.Property19Id;
                updateData.Property20Id = request.Property20Id;

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

        public MasterMaintainFormModel DeleteData(int DataId)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var updateData = (from u in _db.MASTER_MAINTAIN_FORM
                                  where u.FormId == DataId &&
                                  u.IsActive == true
                                  select u).FirstOrDefault();

                updateData.IsActive = false;

                _db.SaveChanges();

                return new MasterMaintainFormModel();
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public List<SelectItemModel<int>> GetPropertys()
        {
            try
            {
                var res = (from u in _dbRead.MASTER_MAINTAIN_PROPERTY
                          select new SelectItemModel<int>()
                          {
                              Label = u.PropertyName,
                              Name = u.PropertyName,
                              Value = u.PropertyId
                          }).ToList();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }

        public List<SelectItemModel<int>> GetFormTemplates()
        {
            try
            {
                var res = (from u in _dbRead.MASTER_MAINTAIN_FORM_TEMPLATE
                           select new SelectItemModel<int>()
                           {
                               Label = u.FormTemplateName,
                               Name = u.FormTemplateName,
                               Value = u.FormTemplateId
                           }).ToList();

                return res;
            }
            catch (Exception oEx)
            {
                _logger.LogError(oEx.Message);
                throw;
            }
        }
    }
}