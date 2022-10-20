using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using SCGP.COA.BUSINESSLOGIC.Commands.Laminate.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Authentications;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.DATAACCESS.Repositories.Coa.Laminate.Interface;
using System.Collections.Generic;
using System.Linq;
namespace SCGP.COA.BUSINESSLOGIC.Commands.Laminate
{
    [TransientRegistration]
    public class LaminateCommand : ILaminateCommand
    {
        private ILaminateRepo _laminateRepo;
        private IUserLocalService _userService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        public LaminateCommand(ILaminateRepo comman, IUserLocalService userService, IWebHostEnvironment environment
            , IFileService fileService)
        {
            _laminateRepo = comman;
            _userService = userService;
            _environment = environment;
            _fileService = fileService;
        }
        public SearchResModel<DataLabModel> SearchDisplayLab(SearchReqModel<DataLabModel> req)
        {
            var query = _laminateRepo.SearchDisplayLab(req.Criteria);
            var res = new SearchResModel<DataLabModel>()
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
        public List<MasterLabDataModel> GetMasterLabData()
        {
            List<MasterLabDataModel> list = new();
            var res = _laminateRepo.GetMasterLabData();
            foreach (var item in res)
            {
                list.Add(new MasterLabDataModel
                {
                    grade = item.Grade,
                    gram = item.Gram,
                });
            }         
            return list;
        }
        public List<DataLabModel> SetExcelToDataTable(List<DataLabModel> dataLabModels)
        {
            try
            {
                var list = new List<ConvertingBatchDatum>();
                foreach (DataLabModel items in dataLabModels)
                {

                    var data = new ConvertingBatchDatum
                    {
                        Batch = items.batchNo,
                        Grade = items.grade,
                        Gram = (decimal)items.gram,
                        ProductionDate = items.productionDate,
                        FilmThickness = items.filmThickness,
                        Porosity = items.porosity,
                        UploadedDatetime = items.uploadDate
                    };
                    list.Add(data);
                }
                var result = _laminateRepo.SetExcelToDataTable(list);
                if (result)
                {
                    return dataLabModels;
                }
                else
                    return new List<DataLabModel>();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public FileDataModel PrintExportExcel()
        {
            var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
            var res = new FileDataModel()
            {
                FileName = "SCGP_COA_PrintExport_"+ sDateTime + ".xlsx",
                FileExtension = ".xlsx"
            };
            //var data1 = _coaSkicPM17Gypsum_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();
            //var data2 = _coaSkicPM1to3_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();

            var data1 = _laminateRepo.GetConvertingBatchDat().ToList();
            string path = $"{_environment.ContentRootPath}" +
                         $"{FileConstant.Template.Location}" +
                         $"{FileConstant.Template.PrintCoaExport}";
            var template = _fileService.CloneExcelFileToMemoryStream(path);

            using (var package = new ExcelPackage(template))
            {
                var row = 2;
                var ms = new MemoryStream();
                var worksheet = package.Workbook.Worksheets["Sheet1"];

                foreach (var item in data1)
                {
                    int col = 0;
                    worksheet.Cells[row, ++col].Value = item.Batch;
                    worksheet.Cells[row, ++col].Value = item.Grade;
                    worksheet.Cells[row, ++col].Value = item.Gram;
                    worksheet.Cells[row, ++col].Value = item.ProductionDate;
                    worksheet.Cells[row, ++col].Value = item.FilmThickness;
                    worksheet.Cells[row, ++col].Value = item.Porosity;
                    worksheet.Cells[row, ++col].Value = item.UploadedDatetime;
                    row++;
                }
                //foreach (var item in data2)
                //{
                //    int col = 0;
                //    worksheet.Cells[row, ++col].Value = "SkicPM1to3";
                //    worksheet.Cells[row, ++col].Value = item.Grade;
                //    worksheet.Cells[row, ++col].Value = item.FormNo;
                //    row++;
                //}
                package.SaveAs(ms);
                res.FileSize = ms.Length;
                res.FileData = ms.ToArray();
            }
            return res;
        }
    }
}
