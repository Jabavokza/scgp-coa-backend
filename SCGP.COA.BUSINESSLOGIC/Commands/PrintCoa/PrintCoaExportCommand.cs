using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using CoaSkicPM17GypsumRepo = SCGP.COA.DATAACCESS.Repositories.CoaSkicPM17Gypsum.Interface;
using CoaSkicPM1to3Repo = SCGP.COA.DATAACCESS.Repositories.CoaSkicPM1to3.Interface;
using HTAG = SCGP.COA.COMMON.Contants.HtmlConstant.TAG;
using HSTYLE = SCGP.COA.COMMON.Contants.HtmlConstant.STYLE;
using HPROP = SCGP.COA.COMMON.Contants.HtmlConstant.PROPERTY;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using SCGP.COA.BUSINESSLOGIC.Controllers;
using SCGP.COA.BUSINESSLOGIC.Models;
using System;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using OfficeOpenXml;
using SCGP.COA.DATAACCESS.Repositories.Coa.Laminate.Interface;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface;
using SAP_Interface_DeliveryNum;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa;
using SCGP.COA.DATAACCESS.Entities.Coa;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece;
using System.Data;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa
{
    [TransientRegistration]
    public class PrintCoaExportCommand : IPrintCoaExportCommand
    {
        private CoaSkicPM17GypsumRepo.ICoaFormRepository _coaSkicPM17Gypsum_coaFormRepository;
        private CoaSkicPM1to3Repo.ICoaFormRepository _coaSkicPM1to3_coaFormRepository;
        private IPdfService _pdfService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private ILaminateRepo _laminateRepo;
        private IExportCoaRepo _exportCoaRepo;
        public PrintCoaExportCommand(CoaSkicPM17GypsumRepo.ICoaFormRepository coaSkicPM17Gypsum_coaFormRepository
            , CoaSkicPM1to3Repo.ICoaFormRepository coaSkicPM1to3_coaFormRepository
            , IPdfService pdfService
            , IWebHostEnvironment environment
            , IFileService fileService
            , ILaminateRepo laminateRepo
            , IExportCoaRepo exportCoaRepo)
        {
            _coaSkicPM17Gypsum_coaFormRepository = coaSkicPM17Gypsum_coaFormRepository;
            _coaSkicPM1to3_coaFormRepository = coaSkicPM1to3_coaFormRepository;
            _pdfService = pdfService;
            _environment = environment;
            _fileService = fileService;
            _laminateRepo = laminateRepo;
            _exportCoaRepo = exportCoaRepo;
        }

        public async Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintExportSearchModel param)
        {
            try
            {
                var MockData = true;
                if (!MockData)
                {
                    var oClient1 = new SI_DeliveryInquiry_OSClient(SI_DeliveryInquiry_OSClient.EndpointConfiguration.HTTPS_Port);
                    oClient1.ClientCredentials.UserName.UserName = "...";
                    oClient1.ClientCredentials.UserName.Password = "...";
                    oClient1.OpenAsync().Wait();
                    List<CoaPrintDomesticDataModel> oDataModels = new();
                    List<DTDeliveryInquiryResItems> oDTDeliveryInquiryResItems = new();
                    var oTasks = new List<Task>();
                    oTasks.Add(Task.Run(async () =>
                    {

                        var aDpNumberStart = param.dpNumberStart!.Split('-');
                        var nDpNumberStart = int.Parse(aDpNumberStart[1]);
                        var nLength = aDpNumberStart[1].Length;
                        var aDpNumberEnd = param.dpNumberEnd!.Split('-');
                        var nDpNumberEnd = int.Parse(aDpNumberEnd[1]);

                        var sPrefig = aDpNumberStart[0] + "-";
                        var aDpNumber = new List<string>();
                        var nDpNumber = (nDpNumberEnd + 1) - nDpNumberStart;
                        var sDigit = "";
                        for (int j = 1; j < nLength; j++) { sDigit += "0"; }
                        for (int i = 0; i < nDpNumber; i++)
                        {
                            switch (nDpNumberStart + i)
                            {
                                case < 10: aDpNumber.Add(sPrefig + sDigit + (nDpNumberStart + i).ToString()); break;
                                case < 100: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 1) + (nDpNumberStart + i).ToString()); break;
                                case < 1000: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 2) + (nDpNumberStart + i).ToString()); break;
                                case < 10000: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 3) + (nDpNumberStart + i).ToString()); break;
                                case < 100000: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 4) + (nDpNumberStart + i).ToString()); break;
                                case < 1000000: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 5) + (nDpNumberStart + i).ToString()); break;
                                case < 10000000: aDpNumber.Add(sPrefig + sDigit.Remove(sDigit.Length - 6) + (nDpNumberStart + i).ToString()); break;
                            }
                        }
                        foreach (var sDpNumber in aDpNumber)
                        {
                            var oReqData = new DT_DeliveryInquiryReq();
                            //oReqData.PI = param.piNumber;
                            //oReqData.eo - param.eoNumber;
                            oReqData.IV_DELIVERY_NUMBER = sDpNumber;
                            var oResData = await oClient1.SI_DeliveryInquiry_OSAsync(oReqData);
                            var oData = oResData.MT_DeliveryInquiryRes;
                            if (oData.ET_LIKP.Length > 0)
                            {
                                oDTDeliveryInquiryResItems.Add(new DTDeliveryInquiryResItems
                                {
                                    DeliveryNum = sDpNumber,
                                    MaterialNum = oData.ET_LIPS.Select(x => x.MATNR).ToString(),
                                    BatchNum = oData.ET_LIPS.Select(x => x.CHARG).ToString(),
                                });
                                CallStoreProcedure(_configuration, oData.ET_LIPS.Select(x => x.CHARG).ToString());
                                oDataModels.Add(new CoaPrintDomesticDataModel { dpNumberId = sDpNumber, dpNumberName = sDpNumber });
                            }
                            else
                                continue;
                        }
                        oClient1.Close();
                    }));
                    await Task.WhenAll(oTasks.ToArray());
                    return  new Dictionary<string, Dictionary<string, string[]>>(); ;
                }
                else
                {
                    // Mock Data -----------------------
                    var o = new Dictionary<string, string[]>();
                    o.Add("EO-001", new string[] { "DP-001", "DP-002", "DP-003" });
                    o.Add("EO-002", new string[] { "DP-004", "DP-005", "DP-006" });
                    o.Add("EO-003", new string[] { "DP-007", "DP-008", "DP-009" });

                    var PI = new Dictionary<string, Dictionary<string, string[]>>();
                    PI.Add("PO-001", o);
                    return PI;
                    //----------------------------------
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CallStoreProcedure(IConfiguration _configuration, string? tBatch)
        {
            try
            {
                var sDbDataContext = _configuration.GetConnectionString("DbDataContext");
                var sParaName = "@BATCH_NUMBER";
                var sSPName = "@sp_COA_GET_BATCH_DATA";
                var oResData = SqlConnectDb.SP_CallSP(sDbDataContext, sSPName, sParaName, tBatch);
                if (oResData.Rows.Count > 0)
                {
                    SetDtBatchToDtTbl(oResData);
                }
                else
                {
                    // “ไม่พบข้อมูล Batch Number ["+tBatch+"] ใน database PLS”.
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SetDtBatchToDtTbl(DataTable oResData)
        {
            try
            {
                var list = new List<ConvertingBatchDatum>();
                foreach (DataRow oRow in oResData.Rows)
                {
                    list.Add(new ConvertingBatchDatum
                    {
                        Batch = oRow["BATCH"].ToString()!,
                        Grade = oRow["GRADE"].ToString()!,
                        Gram = (decimal)oRow["GRAM"],
                        ProductionDate = DateTime.Parse(oRow["PRODUCTION_DATE"].ToString()!),
                        FilmThickness = (double)oRow["FILM_THICKNESS"],
                        Porosity = (double)oRow["POROSITY"],
                        UploadedDatetime = DateTime.Parse(oRow["UPLOADED_DATETIME"].ToString()!)
                    });
                }
                var result = _exportCoaRepo.SetDtBatchToDtTblRepo(list);
                //if (result)
                //{
                //    return dataLabModels;
                //}
                //else
                //    return new List<DataLabModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FileDataModel> PrintExport(ControllerContext controllerContext, CoaPrintExportExecuteModel coaPrintModel)
        {
            try
            {
                List<FileDataModel> dataModels = new();
                foreach (var option in coaPrintModel.option!)
                {
                    switch (option?.ToString())
                    {
                        case "PDF": dataModels.Add(ExportPdf(controllerContext)); break;
                            //  case "Excel":dataModels.Add(ExportExcel()); break;
                    }
                }
                return dataModels;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FileDataModel> SaveExport(ControllerContext controllerContext, CoaPrintExportExecuteModel coaPrintModel)
        {
            try
            {
                List<FileDataModel> dataModels = new();
                foreach (var option in coaPrintModel.option!)
                {
                    switch (option?.ToString())
                    {
                        case "PDF": dataModels.Add(ExportPdf(controllerContext)); break;
                        case "Excel": dataModels.Add(ExportExcel()); break;
                        case "Text": dataModels.Add(ExporTextFile()); break;
                    }
                }
                return dataModels;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private FileDataModel ExportPdf(ControllerContext controllerContext)
        {
            try
            {
                //var data1 = _coaSkicPM17Gypsum_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();
                //var data2 = _coaSkicPM1to3_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();

                List<PrintCoaDataModel> data1 = new();
                for (int i = 1; i < 5; i++)
                {
                    data1.Add(new PrintCoaDataModel
                    {
                        Grade = "AA" + i,
                        FormNo = i
                    });
                }
                var table1 = new PrintCoaExportPdfModel()
                {
                    FileName = "File1",
                    SheetHtml = GeneratePdfDataTableExport("SkicPM17Gypsum", data1)
                };
                var fileData1 = GeneratePdf(controllerContext, table1);
                var table2 = new PrintCoaExportPdfModel()
                {
                    FileName = "File2",
                    SheetHtml = GeneratePdfDataTableExport("SkicPM1to3", data1)
                };
                var fileData2 = GeneratePdf(controllerContext, table2);
                var res = new FileDataModel()
                {
                    FileName = "SCGP_COA_PrintExportPDF_.pdf",
                    FileExtension = ".pdf"
                };
                using (MemoryStream ms = new MemoryStream())
                {
                    _pdfService.MergeMultiplePDFIntoSinglePDF(new List<byte[]> { fileData1, fileData2 }, ms, false);
                    res.FileData = ms.ToArray();
                    res.FileSize = ms.Length;
                }
                return res;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private string GeneratePdfDataTableExport(string source, List<PrintCoaDataModel> data)
        {
            try
            {
                string html = "";
                var center = new HTMLPropertyModel(HPROP.CLASS, "center");
                html += HTMLUtil.OpenTag(HTAG.TABLE, null, null);

                html += HTMLUtil.OpenTag(HTAG.THERD, null, null);
                html += HTMLUtil.OpenTag(HTAG.TR, null);
                html += HTMLUtil.SetTag(HTAG.TD, "Source", null, center);
                html += HTMLUtil.SetTag(HTAG.TD, "Grade", null, center);
                html += HTMLUtil.SetTag(HTAG.TD, "FormNo", null, center);
                html += HTMLUtil.CloseTag(HTAG.TR);
                html += HTMLUtil.CloseTag(HTAG.THERD);
                html += HTMLUtil.OpenTag(HTAG.TBODY, null, null);

                foreach (var td in data)
                {
                    html += HTMLUtil.OpenTag(HTAG.TR, null);
                    html += HTMLUtil.SetTag(HTAG.TD, source, null, center);
                    html += HTMLUtil.SetTag(HTAG.TD, td.Grade, null, center);
                    html += HTMLUtil.SetTag(HTAG.TD, td.FormNo?.ToString() ?? "", null, center);
                    html += HTMLUtil.CloseTag(HTAG.TR);
                }
                html += HTMLUtil.CloseTag(HTAG.TBODY);
                html += HTMLUtil.CloseTag(HTAG.TABLE);
                return html;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private byte[] GeneratePdf(ControllerContext controllerContext, PrintCoaExportPdfModel data)
        {
            try
            {
                var pdfController = new PdfController();
                var route = new RouteData();
                route.Values.Add("action", "PrintCoaExport");
                route.Values.Add("controller", "Pdf");
                if (controllerContext == null) controllerContext = new ControllerContext();
                controllerContext.RouteData = route;
                pdfController.ControllerContext = controllerContext;
                var actionPDF = pdfController.PrintCoaExport(data);

                return actionPDF;

            }
            catch (Exception)
            {

                throw;
            }

        }
        private FileDataModel ExportExcel()
        {
            try
            {
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var res = new FileDataModel()
                {
                    FileName = "SCGP_COA_PrintExportExcel_" + sDateTime + ".xlsx",
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
            catch (Exception)
            {

                throw;
            }

        }
        private FileDataModel ExporTextFile()
        {
            try
            {
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var res = new FileDataModel()
                {
                    FileName = "SCGP_COA_PrintExport_" + sDateTime + ".xlsx",
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
