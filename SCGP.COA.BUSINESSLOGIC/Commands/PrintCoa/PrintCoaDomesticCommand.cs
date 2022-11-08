using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using CoaSkicPM17GypsumRepo = SCGP.COA.DATAACCESS.Repositories.CoaSkicPM17Gypsum.Interface;
using CoaSkicPM1to3Repo = SCGP.COA.DATAACCESS.Repositories.CoaSkicPM1to3.Interface;
using HTAG = SCGP.COA.COMMON.Contants.HtmlConstant.TAG;
using HPROP = SCGP.COA.COMMON.Contants.HtmlConstant.PROPERTY;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Controllers;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using OfficeOpenXml;
using SCGP.COA.DATAACCESS.Repositories.Coa.Laminate.Interface;
using SCGP.COA.DATAACCESS.Models;
using SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa.Interface;
using SCGP.COA.DATAACCESS.Entities.Coa;
using System.Data;
using SAP_Interface_DeliveryNum;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa;
using SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa.Interfece;
using System.Text;
using ClosedXML.Excel;
using System.Net;

namespace SCGP.COA.BUSINESSLOGIC.Commands.PrintCoa
{
    [TransientRegistration]
    public class PrintCoaDomesticCommand : IPrintCoaDomesticCommand
    {
        private CoaSkicPM17GypsumRepo.ICoaFormRepository _coaSkicPM17Gypsum_coaFormRepository;
        private CoaSkicPM1to3Repo.ICoaFormRepository _coaSkicPM1to3_coaFormRepository;
        private IPdfService _pdfService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private ILaminateRepo _laminateRepo;
        private IExportCoaRepo _exportCoaRepo;
        private List<PrintCoaExportTempSP> _aPrintCoaExportTempSP;
        private ISAPService _sapService;
        private ISapShippingPointRepo _SapShippingPointRepo;
        private string _CustomerId = "";
        public PrintCoaDomesticCommand(CoaSkicPM17GypsumRepo.ICoaFormRepository coaSkicPM17Gypsum_coaFormRepository
            , CoaSkicPM1to3Repo.ICoaFormRepository coaSkicPM1to3_coaFormRepository
            , IPdfService pdfService
            , IWebHostEnvironment environment
            , IFileService fileService
            , ILaminateRepo laminateRepo
            , IExportCoaRepo exportCoaRepo
            , ISAPService sapService,
              ISapShippingPointRepo sapShippingPointRepo)
        {
            _coaSkicPM17Gypsum_coaFormRepository = coaSkicPM17Gypsum_coaFormRepository;
            _coaSkicPM1to3_coaFormRepository = coaSkicPM1to3_coaFormRepository;
            _pdfService = pdfService;
            _environment = environment;
            _fileService = fileService;
            _laminateRepo = laminateRepo;
            _exportCoaRepo = exportCoaRepo;
            _sapService = sapService;
            _SapShippingPointRepo = sapShippingPointRepo;
        }


        public async Task<List<CoaPrintDomesticDataModel>> GetDPNumberDataAsyncForNotConnectSAP(IConfiguration _configuration, CoaPrintDomesticSearchModel param)
        {
            //Mockup Data
            List<CoaPrintDomesticDataModel> oDataModels = new();
            var oTasks = new List<Task>();
            oTasks.Add(Task.Run(async () =>
            {
                oDataModels.Add(new CoaPrintDomesticDataModel { dpNumberId = param.dpNumberStart, dpNumberName = param.dpNumberStart });
            }));
            await Task.WhenAll(oTasks.ToArray());
            return oDataModels;

            ///////////////////////////////////////////////////////////////////////////////
        }

        public async Task<List<CoaPrintDomesticDataModel>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintDomesticSearchModel param)
        {
            try
            {

                List<CoaPrintDomesticDataModel> oDataModels = new();
                var oTasks = new List<Task>();
                oTasks.Add(Task.Run(async () =>
                {
                    var nDpNumberStart = int.Parse(param.dpNumberStart != "" ? param.dpNumberStart! : param.dpNumberEnd!);
                    var nDpNumberEnd = int.Parse(param.dpNumberEnd != "" ? param.dpNumberEnd! : param.dpNumberStart!);
                    var aDpNumber = new List<string>();
                    for (int i = nDpNumberStart; i <= nDpNumberEnd; i++)
                    {
                        foreach (var oItem in _SapShippingPointRepo.GetShippingPoints())
                        {
                            var oReq = new SI_DeliveryInquiry_OSRequest()
                            {
                                MT_DeliveryInquiryReq = new DT_DeliveryInquiryReq
                                {
                                    IV_DELIVERY_NUMBER = i.ToString(),
                                    IV_SHIPPING_POINT = oItem.Shipping_Point,
                                    IV_ORG = oItem.Company_Code
                                }
                            };
                            var oData = await _sapService.CallSAPDeliveryInquiry(oReq);
                            if (oData.MT_DeliveryInquiryRes.ET_LIKP != null && oData.MT_DeliveryInquiryRes.ET_LIKP.Any())
                            {
                                oDataModels.Add(new CoaPrintDomesticDataModel { dpNumberId = i.ToString(), dpNumberName = i.ToString() });
                                break;
                            }
                            else
                                continue;

                        }

                    }
                }));
                await Task.WhenAll(oTasks.ToArray());
                return oDataModels;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<PrintCoaExportTempSP> CALLxSapAsync(IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintDomesticExecuteModel)//#2 MockUp Test because don't connect to  Sap
        {
            try
            {
                List<CoaPrintDomesticDataModel> oDataModels = new();
                List<DTDeliveryInquiryResItems> oDTDeliveryInquiryResItems = new();
                PrintCoaExportTempSP oPrintCoaExportTempSP = new();
                var sDpNumber = coaPrintDomesticExecuteModel.dpNumber;
                var aOption = coaPrintDomesticExecuteModel.option;

                //var aBatchNum = new List<string>();
                try
                {
                    foreach (var oItem in _SapShippingPointRepo.GetShippingPoints())
                    {
                        var oReqData = new DT_DeliveryInquiryReq
                        {
                            IV_DELIVERY_NUMBER = sDpNumber,
                            IV_SHIPPING_POINT = oItem.Shipping_Point,
                            IV_ORG = oItem.Company_Code,
                            IV_ITEM_FLAG = "X"
                        };
                        var oReq = new SI_DeliveryInquiry_OSRequest()
                        {
                            MT_DeliveryInquiryReq = oReqData
                        };
                        var oData = await _sapService.CallSAPDeliveryInquiry(oReq);
                        if (oData.MT_DeliveryInquiryRes.ET_LIKP != null && oData.MT_DeliveryInquiryRes.ET_LIPS != null)
                        {
                            foreach (var x in oData.MT_DeliveryInquiryRes.ET_LIPS)
                            {
                                if (x.LFIMG != 0)
                                {
                                    if (x.CHARG != null)
                                    {
                                        oDTDeliveryInquiryResItems.Add(new DTDeliveryInquiryResItems
                                        {
                                            DeliveryNum = sDpNumber,
                                            MaterialNum = x.MATNR,
                                            BatchNum = x.CHARG
                                        });
                                        _CustomerId = oData.MT_DeliveryInquiryRes.ET_LIKP.Select(s => s.KUNAG).FirstOrDefault()!;
                                    }
                                    else
                                    {
                                        string sMsgError = "Delivery Number [" + sDpNumber + "] Item [" + x.MATNR + "] has not been picked yet.";
                                    }
                                }

                            }
                            break;
                        }
                        else
                            continue;
                    }

                }
                catch (Exception ex)
                {
                }
                var oTasks = new List<Task>();
                oTasks.Add(Task.Run(() =>
                {
                    foreach (var oItem in oDTDeliveryInquiryResItems)
                    {
                        foreach (var sOption in aOption!)
                        {
                            switch (sOption)
                            {
                                case "Text":
                                    oPrintCoaExportTempSP.ExportTextFile.Merge(SET_DataForTextFile(_configuration, oItem.BatchNum));
                                    break;
                                case "Excel":
                                    oPrintCoaExportTempSP.ExportExcel.Merge(SET_DataForExcelFile(_configuration, oItem.BatchNum));
                                    break;
                                case "PDF":
                                    oPrintCoaExportTempSP.ExportPDF.Merge(SET_DataForPDFFile(_configuration, oItem));
                                    break;
                            }
                        }
                    }
                    return Task.CompletedTask;
                }));
                await Task.WhenAll(oTasks.ToArray());
                return oPrintCoaExportTempSP;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FileDataModel>> PrintExport(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel)
        {
            try
            {
                var oData = await CALLxSapAsync(_configuration, coaPrintModel);
                List<FileDataModel> dataModels = new();
                foreach (var option in coaPrintModel.option!)
                {
                    switch (option?.ToString())
                    {
                        case "PDF": dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, coaPrintModel.dpNumber!)); break;
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
        public async Task<List<FileDataModel>> SaveExport(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel)//#1
        {
            try
            {
                //  var oDatax = await CALLxSapAsyncx(_configuration, coaPrintModel.dpNumber!);
                var oData = await CALLxSapAsync(_configuration, coaPrintModel);
                List<FileDataModel> dataModels = new();
                foreach (var option in coaPrintModel.option!)
                {
                    switch (option?.ToString())
                    {
                        case "PDF": dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, coaPrintModel.dpNumber!)); break;
                        case "Excel": dataModels.Add(ExportToExcel(oData.ExportExcel, coaPrintModel.dpNumber!)); break;
                        case "Text": dataModels.Add(ExporTextFile(oData.ExportTextFile, coaPrintModel.dpNumber!)); break;
                    }
                }
                foreach (var sItem in dataModels.Select(e => e.FileName))
                {
                    var sFileName = sItem;
                    var sCustomerId = "0000000011";
                    var sSubFolder = System.IO.Path.Combine(sCustomerId);
                    //await Task.Run(() => UploadFTPFile(_configuration, sFileName, sSubFolder));
                }
                return dataModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<FileDataModel>> ExcuteData(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel)//#1
        {
            try
            {
                //  var oDatax = await CALLxSapAsyncx(_configuration, coaPrintModel.dpNumber!);
                var oData = await CALLxSapAsync(_configuration, coaPrintModel);
                List<FileDataModel> dataModels = new();
                bool bGenPDF = false;
                foreach (var action in coaPrintModel.action!)
                {
                    switch (action?.ToString())
                    {
                        case "Print":
                            foreach (var option in coaPrintModel.option!)
                            {
                                switch (option?.ToString())
                                {
                                    case "PDF": dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, coaPrintModel.dpNumber!)); bGenPDF = true; break;
                                }
                            }
                            ; break;
                        case "Save":
                            foreach (var option in coaPrintModel.option!)
                            {
                                switch (option?.ToString())
                                {
                                    case "PDF": if (!bGenPDF) { dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, coaPrintModel.dpNumber!)); }; break;
                                    case "Excel": dataModels.Add(ExportToExcel(oData.ExportExcel, coaPrintModel.dpNumber!)); break;
                                    case "Text": dataModels.Add(ExporTextFile(oData.ExportTextFile, coaPrintModel.dpNumber!)); break;
                                }
                            }
                            ; break;

                    }
                }
                foreach (var action in coaPrintModel.action!)
                {
                    switch (action?.ToString())
                    {
                        case "Send to E-document":
                            foreach (var sItem in dataModels.Select(e => e.FileName))
                            {
                                var sFileName = sItem;
                                var sCustomerId = _CustomerId;
                                var sSubFolder = System.IO.Path.Combine(sCustomerId);
                                await Task.Run(() => UploadFTPFile(_configuration, sFileName, sSubFolder));
                            }
                            ; break;
                    }
                }
                return dataModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //public List<FileDataModel> DomesticCoa(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintDomesticExecuteModel coaPrintModel)
        //{
        //    try
        //    {
        //        var oData = CALLxSapAsync(_configuration, coaPrintModel.dpNumber!);
        //        List<FileDataModel> dataModels = new();
        //        foreach (var option in coaPrintModel.option!)
        //        {
        //            switch (option?.ToString())
        //            {
        //                case "PDF": dataModels.Add(ExportPdf(controllerContext, oData)); break;
        //                    //  case "Excel":dataModels.Add(ExportExcel()); break;
        //            }
        //        }
        //        return dataModels;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //private FileDataModel ExportPdf_Old(ControllerContext controllerContext, List<PrintCoaExportTempSP> aPrintCoaExportTempSPs) //#x.1
        //{
        //    try
        //    {
        //        // var data1 = _coaSkicPM17Gypsum_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();
        //        //var data2 = _coaSkicPM1to3_coaFormRepository.Read().Select(x => new PrintCoaDataModel { Grade = x.Grade, FormNo = x.FormNo }).ToList();

        //        List<PrintCoaDataSPModel> oPrintCoaDataSPModel = new();
        //        foreach (var xxx in aPrintCoaExportTempSPs)
        //        {
        //            foreach (DataRow xx in xxx.ExportPDF.Rows)
        //            {
        //                oPrintCoaDataSPModel.Add(new PrintCoaDataSPModel
        //                {
        //                    BatchNumber = xx.ToString(),
        //                });
        //            }
        //        }

        //        List<PrintCoaDataModel> data1 = new();
        //        for (int i = 1; i < 5; i++)
        //        {
        //            data1.Add(new PrintCoaDataModel
        //            {
        //                Grade = "AA" + i,
        //                FormNo = i
        //            });
        //        }
        //        var table1 = new PrintCoaExportPdfModel()
        //        {
        //            FileName = "File1",
        //            SheetHtml = GeneratePdfDataTableExport("SkicPM17Gypsum", data1)
        //        };
        //        var fileData1 = GeneratePdf(controllerContext, table1);

        //        var table2 = new PrintCoaExportPdfModel()
        //        {
        //            FileName = "File2",
        //            SheetHtml = GeneratePdfDataTableExport("SkicPM1to3", data1)
        //        };
        //        var fileData2 = GeneratePdf(controllerContext, table2);

        //        var res = new FileDataModel()
        //        {
        //            FileName = "SCGP_COA_PrintExportPDF_.pdf",
        //            FileExtension = ".pdf"
        //        };
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            _pdfService.MergeMultiplePDFIntoSinglePDF(new List<byte[]> { fileData1, fileData2 }, ms, false);
        //            res.FileData = ms.ToArray();
        //            res.FileSize = ms.Length;
        //        }
        //        return res;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        private FileDataModel ExportPdf(ControllerContext controllerContext, DataTable aPrintCoaExportTempSPs, string sDpNumber) //#x.1
        {
            try
            {
                var oTable1 = new PrintCoaExportPdfModel()
                {
                    FileName = "File1",
                    SheetHtml = GeneratePdfDataTableExport("SkicPM17Gypsum", aPrintCoaExportTempSPs)
                };
                var oFileData1 = GeneratePdf(controllerContext, oTable1);
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var res = new FileDataModel()
                {
                    // FileName = "SCGP_COA_PrintExportPDF_" + sDateTime + ".pdf",
                    FileName = sDpNumber + ".pdf",
                    FileExtension = ".pdf"
                };
                var sPath = $"{_environment.ContentRootPath}" + $"{FileConstant.Template.UploadFTP}";
                using (var oWriterExport = new StreamWriter(sPath + res.FileName, false, Encoding.UTF8))
                {
                    oWriterExport.BaseStream.Write(oFileData1, 0, oFileData1.Length);
                    oWriterExport.Flush();
                    oWriterExport.Close();
                }
                res.FileData = oFileData1;
                res.FileSize = oFileData1.Length;
                return res;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private FileDataModel ExportExcel_Old(DataTable oPrintCoaExportTempSPs) //#x.2
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
        private FileDataModel ExportToExcel(DataTable tbl, string sDpNumber)
        {
            try
            {
                var sPath = $"{_environment.ContentRootPath}" + $"{FileConstant.Template.UploadFTP}";
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var oRes = new FileDataModel()
                {
                    //FileName = "SCGP_COA_PrintExportExcel_" + sDateTime + ".xlsx",
                    FileName = sDpNumber + ".xlsx",
                    FileExtension = ".xlsx"
                };

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Users");
                    var nCurrentRow = 1;
                    foreach (DataRow oRow in tbl.Rows)
                    {
                        var nCurrentColm = 1;
                        foreach (DataColumn oColm in tbl.Columns)
                        {
                            if (nCurrentRow == 1)
                            {
                                worksheet.Cell(nCurrentRow, nCurrentColm).Value = oColm.ColumnName.ToString();
                            }
                            else
                            {
                                worksheet.Cell(nCurrentRow, nCurrentColm).Value = oRow[oColm.ColumnName].ToString();
                            }
                            nCurrentColm++;
                        }
                        nCurrentRow++;
                    }

                    using var ms = new MemoryStream();
                    workbook.SaveAs(sPath + oRes.FileName);
                    workbook.SaveAs(ms);
                    oRes.FileSize = ms.Length;
                    oRes.FileData = ms.ToArray();
                    return oRes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        private FileDataModel ExporTextFile(DataTable oPrintCoaExportTempSPs, string sDpNumber) //#x.3
        {
            try
            {
                string sPath = $"{_environment.ContentRootPath}" + $"{FileConstant.Template.UploadFTP}";
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var res = new FileDataModel()
                {
                    //FileName = "SCGP_COA_PrintExportText_" + sDateTime + ".txt",
                    FileName = sDpNumber + ".txt",
                    FileExtension = ".txt"
                };
                // var data1 = _laminateRepo.GetConvertingBatchDat().ToList();
                var ms = WriteTextFileWithDb(oPrintCoaExportTempSPs, res.FileName);
                res.FileSize = ms.Length;
                res.FileData = ms;
                if (res.FileSize > 0)
                {
                    using (var oWriterExport = new StreamWriter(sPath + res.FileName, false, Encoding.GetEncoding(874)))
                    {
                        oWriterExport.BaseStream.Write(ms, 0, ms.Length);
                        oWriterExport.Flush();
                        oWriterExport.Close();
                    }
                }

                return res;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private string GeneratePdfDataTableExport(string source, DataTable oData)
        {
            try
            {
                //Update 28/10/2022 12:48:28
                string html = "";
                var center = new HTMLPropertyModel(HPROP.CLASS, "center");
                html += HTMLUtil.OpenTag(HTAG.TABLE, null, null);

                html += HTMLUtil.OpenTag(HTAG.THERD, null, null);
                html += HTMLUtil.OpenTag(HTAG.TR, null);

                html += HTMLUtil.SetTag(HTAG.TD, "Source", null, center);
                foreach (DataColumn oColumn in oData.Columns)
                {
                    //Add the Data rows.
                    html += HTMLUtil.SetTag(HTAG.TD, oColumn.ColumnName, null, center);
                }
                html += HTMLUtil.CloseTag(HTAG.TR);
                html += HTMLUtil.CloseTag(HTAG.THERD);



                html += HTMLUtil.OpenTag(HTAG.TBODY, null, null);
                html += HTMLUtil.OpenTag(HTAG.TR, null);
                html += HTMLUtil.SetTag(HTAG.TD, source, null, center);
                foreach (DataRow oRow in oData.Rows)
                {
                    foreach (DataColumn oColumn in oData.Columns)
                    {
                        html += HTMLUtil.SetTag(HTAG.TD, oRow[oColumn.ColumnName].ToString()!, null, center);
                    }
                    html += "\r\n";
                }
                html += HTMLUtil.CloseTag(HTAG.TR);
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
        private static byte[] WriteTextFile(List<ConvertingBatchDatum> convertingBatchData, string fileName)
        {
            var tStr = "";
            tStr += "Batch,Grade,Gram,ProductionDate,FilmThickness,Porosity,UploadedDatetime";
            tStr += "\r\n";
            foreach (var oRow in convertingBatchData)
            {
                tStr += oRow.Batch.Trim() + "," +
                oRow.Grade.Trim() + "," +
                oRow.Gram + "," +
                oRow.ProductionDate + "," +
                oRow.FilmThickness + "," +
                oRow.Porosity + "," +
                oRow.UploadedDatetime + "\r\n";
            }
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tStr);
            return byteArray;
        }
        private static byte[] WriteTextFileWithDb(DataTable oDbBatchData, string fileName)
        {
            StringBuilder oStr = new();
            try
            {
                foreach (DataRow oRow in oDbBatchData.Rows)
                {
                    foreach (DataColumn oColumn in oDbBatchData.Columns)
                    {
                        //Add the Data rows.
                        var sData = oRow[oColumn.ColumnName].ToString();
                        if (sData != "")
                        {
                            oStr.Append(oRow[oColumn.ColumnName].ToString());
                        }

                    }
                    //Add new line.
                    var sStr = oStr.Length > 0 ? "\r\n" : "";
                    oStr.Append(sStr);
                }
                if (oStr != null && oStr.Length > 0) oStr.Length--;//ลบ "\r\n" ตัวสุดท้ายออก
            }
            catch (Exception ex)
            {
                throw;
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(oStr.ToString());
            return byteArray;
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

        private DataTable SET_DataForTextFile(IConfiguration _configuration, string? sBatchNum)//#3.1
        {
            try
            {
                return CallSP_BatchData(_configuration, sBatchNum);
                // return CallSP_BatchDataTest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DataTable SET_DataForExcelFile(IConfiguration _configuration, string? sBatchNum)//#3.2
        {
            try
            {
                var oBatchData = CallSP_BatchData(_configuration, sBatchNum);
                var oLabData = CallSP_LabData(_configuration, sBatchNum);

                if (oLabData.Rows.Count > 0)
                {
                    foreach (DataRow oRow in oLabData.Select("PropertyName in('BW','CAL','RC','CMT','Brightness','Whiteness','QI')"))
                    {
                        var sColumnName = oRow[0].ToString();
                        var sValue = oRow[1].ToString();
                        DataColumn newColumn = new(sColumnName, typeof(string))
                        {
                            DefaultValue = sValue
                        };
                        oBatchData.Columns.Add(newColumn);
                    }
                }
                return oBatchData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private DataTable SET_DataForPDFFile(IConfiguration _configuration, DTDeliveryInquiryResItems dTDeliveryInquiryResItems)//#3.2
        {
            try
            {
                //  var oBatchData=  CallSP_BatchDataTest();
                var oBatchData = CallSP_BatchData(_configuration, dTDeliveryInquiryResItems.BatchNum);
                //  var oLabData = CallSP_LabData(_configuration, dTDeliveryInquiryResItems.BatchNum);
                // var oGradeData = CallSP_GradeData(_configuration, dTDeliveryInquiryResItems.MaterialNum);
                //if (oLabData.Rows.Count > 0)
                //{
                //    foreach (DataRow oRow in oLabData.Select("PropertyName in('BW','CAL','RC','CMT','Brightness','Whiteness','QI')"))
                //    {
                //        var sColumnName = oRow[0].ToString();
                //        var sValue = oRow[1].ToString();
                //        DataColumn newColumn = new(sColumnName, typeof(string))
                //        {
                //            DefaultValue = sValue
                //        };
                //        oBatchData.Columns.Add(newColumn);
                //    }
                //}
                //if (oGradeData.Rows.Count > 0)
                //{
                //    foreach (DataRow oRow in oGradeData.Rows)
                //    {
                //        var sColumnName = oRow[0].ToString();
                //        var sValue = oRow[1].ToString();
                //        DataColumn newColumn = new(sColumnName, typeof(string))
                //        {
                //            DefaultValue = sValue
                //        };
                //        oBatchData.Columns.Add(newColumn);
                //    }
                //}
                return oBatchData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private DataTable CallSP_BatchData(IConfiguration _configuration, string? sBatchNum)
        {
            try
            {
                var oPrintCoaExportTempSP = new PrintCoaExportTempSP();
                var sDbDataContext = _configuration.GetConnectionString("CoaSkicPM1to2Context");
                var sParaName = "@BATCH_NUMBER";
                var sSPName = "sp_COA_GET_BATCH_DATA";
                var oResData = SqlConnectDb.SP_CallSP(sDbDataContext, sSPName, sParaName, sBatchNum);
                if (oResData.Rows.Count > 0)
                    return oResData;
                else
                    return new DataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }//#4.1
        private DataTable CallSP_BatchDataTest()
        {
            try
            {

                DataTable table = new DataTable();
                table.Columns.Add("Dosage", typeof(int));
                table.Columns.Add("Drug", typeof(string));
                table.Columns.Add("Patient", typeof(string));
                table.Columns.Add("Date", typeof(DateTime));

                // Here we add five DataRows.
                table.Rows.Add(25, "Indocin", "David", DateTime.Now);
                table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
                table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
                table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
                table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }//#4.1
        private DataTable CallSP_LabData(IConfiguration _configuration, string? tBatchNum) //#4.2
        {
            try
            {
                var oPrintCoaExportTempSP = new PrintCoaExportTempSP();
                var sDbDataContext = _configuration.GetConnectionString("CoaSkicPM1to3Context");
                var sParaName = "@BATCH_NUMBER";
                var sSPName = "sp_COA_GET_REEL_LAB_DATA";
                var oResData = SqlConnectDb.SP_CallSP(sDbDataContext, sSPName, sParaName, tBatchNum);
                if (oResData.Rows.Count > 0)
                    return oResData;
                else
                    return new DataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DataTable CallSP_GradeData(IConfiguration _configuration, string? tMaterialNum) //#4.2
        {
            try
            {
                var oPrintCoaExportTempSP = new PrintCoaExportTempSP();
                var sDbDataContext = _configuration.GetConnectionString("CoaSkicPM1to3Context");
                var sParaName = "@Material_Number";
                var sSPName = "sp_COA_GET_GRADE_SPECIFICATIONS";
                var oResData = SqlConnectDb.SP_CallSP(sDbDataContext, sSPName, sParaName, tMaterialNum);
                if (oResData.Rows.Count > 0)
                    return oResData;
                else
                    return new DataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task cUploadSFTPFileAsync(string sPathFileClient, string sPathFileServer)
        {
            try
            {
                var sHost = "10.28.59.121";
                var sUsername = "dpelectronic";
                var sPassword = "SCGPaper2021";
                //var sPort = "21";
                //var nPort = int.Parse(sPort);
                //using SftpClient client = new(sHost, nPort, sUsername, sPassword);
                //client.Connect();
                //if (client.IsConnected)
                //{
                //    client.ChangeDirectory(sPathFileServer);
                //    using FileStream oFs = new(sPathFileClient, FileMode.Open);
                //    client.BufferSize = 4 * 1024;
                //    client.UploadFile(oFs, Path.GetFileName(sPathFileClient));
                //}
                //client.Disconnect();


                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sHost);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(sUsername, sPassword);

                // Copy the contents of the file to the request stream.
                using (FileStream fileStream = File.Open(sPathFileClient, FileMode.Open, FileAccess.Read))
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await fileStream.CopyToAsync(requestStream);
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private string UploadFTPFile(IConfiguration _configuration, string sFileName, string sSubFolder1 = "")
        {
            try
            {
                var sHost = _configuration.GetValue<string>("AppSettings:Host");
                var sUsername = _configuration.GetValue<string>("AppSettings:Username");
                var sPassword = _configuration.GetValue<string>("AppSettings:Password");
                var sUploadDirectory = _configuration.GetValue<string>("AppSettings:PathServer");
                var sLocalDirectory = _configuration.GetValue<string>("AppSettings:PathLocal");

                var sLocalPath = $"{_environment.ContentRootPath}" + $"{sLocalDirectory}" + sFileName;
                var sSubFolder2 = DateTime.Now.ToString("yyyyMMdd");
                var sServerPath = sHost + sUploadDirectory + "/" + sSubFolder1;

                var bCreatePath = true;
                try
                {
                    var bIsExist = CheckDirectoryExist(sServerPath, sUsername, sPassword);
                    if (!bIsExist)
                        bCreatePath = CreateDirector(sServerPath, sUsername, sPassword);

                    sServerPath += "/" + sSubFolder2;

                    bIsExist = CheckDirectoryExist(sServerPath, sUsername, sPassword);
                    if (!bIsExist)
                        bCreatePath = CreateDirector(sServerPath, sUsername, sPassword);

                    var sUploadUrl = sServerPath + "/" + sFileName;
                    if (bCreatePath)
                        return UploadToFTP(sUploadUrl, sLocalPath, sUsername, sPassword);
                    else
                        return "DirPath isn't found!!";
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private bool CheckDirectoryExist(string sDirPath, string sUsername, string sPassword)
        {
            bool bIsexist = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sDirPath);
                request.Credentials = new NetworkCredential(sUsername, sPassword);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    bIsexist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return bIsexist;
        }
        private bool CreateDirector(string sDirPath, string sUsername, string sPassword)
        {
            bool bCreatePath = false;
            try
            {
                WebRequest oReq1 = WebRequest.Create(sDirPath);
                oReq1.Method = WebRequestMethods.Ftp.MakeDirectory;
                oReq1.Proxy = new WebProxy();
                oReq1.Credentials = new NetworkCredential(sUsername, sPassword);
                using (var resp = (FtpWebResponse)oReq1.GetResponse())
                {
                    if (resp.StatusCode == FtpStatusCode.PathnameCreated)
                    {
                        bCreatePath = true;
                    }
                    else
                        bCreatePath = false;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return bCreatePath;
        }
        private string UploadToFTP(string sUploadUrl, string sFileName, string sUsername, string sPassword)
        {
            try
            {
                string sResMsg;
                FtpWebRequest oReq = (FtpWebRequest)WebRequest.Create(sUploadUrl);
                oReq.Proxy = null;
                oReq.Method = WebRequestMethods.Ftp.UploadFile;
                oReq.Credentials = new NetworkCredential(sUsername, sPassword);
                oReq.UseBinary = true;
                oReq.UsePassive = true;
                byte[] data = File.ReadAllBytes(sFileName);
                oReq.ContentLength = data.Length;
                using (var stream = oReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                using (var res = (FtpWebResponse)oReq.GetResponse())
                {
                    sResMsg = res.StatusDescription!;

                }
                return sResMsg;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
    }
}
