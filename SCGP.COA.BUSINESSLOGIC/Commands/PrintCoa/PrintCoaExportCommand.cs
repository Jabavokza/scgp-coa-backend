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
using ClosedXML.Excel;
using SCGP.COA.BUSINESSLOGIC.Services;
using System.Net;
using System.Text;
using PdfSharpCore.Pdf;

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
        private ISAPService _sapService;
        private string _CustomerId = "";
        private ISapShippingPointRepo _SapShippingPointRepo;
        public PrintCoaExportCommand(CoaSkicPM17GypsumRepo.ICoaFormRepository coaSkicPM17Gypsum_coaFormRepository
            , CoaSkicPM1to3Repo.ICoaFormRepository coaSkicPM1to3_coaFormRepository
            , IPdfService pdfService
            , IWebHostEnvironment environment
            , IFileService fileService
            , ILaminateRepo laminateRepo
            , IExportCoaRepo exportCoaRepo
            , ISapShippingPointRepo sapShippingPointRepo
            , ISAPService sapService)
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

        public async Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsyncForNotConnectSAP(IConfiguration _configuration, CoaPrintExportSearchModel param)
        {
            //Mockup Data
            List<CoaPrintExportDataModel> oDataModels = new();
            var PI = new Dictionary<string, Dictionary<string, string[]>>();
            var oTasks = new List<Task>();
            oTasks.Add(Task.Run(async () =>
            {
                var nDpNumberStart = int.Parse(param.dpNumberStart != "" ? param.dpNumberStart! : param.dpNumberEnd!);
                var nDpNumberEnd = int.Parse(param.dpNumberEnd != "" ? param.dpNumberEnd! : param.dpNumberStart!);
                var aDpNumber = new List<string>();
                for (int i = nDpNumberStart; i <= nDpNumberEnd; i++)
                {
                    aDpNumber.Add(i.ToString());
                }
                string[] mylist = aDpNumber.ToArray();
                var o = new Dictionary<string, string[]>();
                o.Add(param.eoNumber!, mylist);
                PI.Add(param.piNumber!, o);
            }));
            await Task.WhenAll(oTasks.ToArray());
            return PI;
        }

        public async Task<Dictionary<string, Dictionary<string, string[]>>> GetDPNumberDataAsync(IConfiguration _configuration, CoaPrintExportSearchModel param)
        {
            try
            {
                List<CoaPrintExportDataModel> oDataModels = new();
                var PI = new Dictionary<string, Dictionary<string, string[]>>();
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
                                aDpNumber.Add(i.ToString());
                            }
                            else
                                continue;
                        }
                    }
                    string[] mylist = aDpNumber.ToArray();
                    var o = new Dictionary<string, string[]>();
                    o.Add(param.eoNumber!, mylist);
                    PI.Add(param.piNumber!, o);
                }));
                await Task.WhenAll(oTasks.ToArray());
                return PI;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FileDataModel>> ExcuteData(ControllerContext controllerContext, IConfiguration _configuration, CoaPrintExportExecuteModel coaPrintModel)//#1
        {
            try
            {
                //  var oDatax = await CALLxSapAsyncx(_configuration, coaPrintModel.dpNumber!);

                List<FileDataModel> dataModels = new();
                foreach (var sDpNumber in coaPrintModel.dpNumber!)
                {
                    bool bGenPDF = false;
                    var oData = await CALLxSapAsync(_configuration, coaPrintModel, sDpNumber);
                    foreach (var action in coaPrintModel.action!)
                    {
                        switch (action?.ToString())
                        {
                            case "Print":
                                foreach (var option in coaPrintModel.option!)
                                {
                                    switch (option?.ToString())
                                    {
                                        case "PDF": dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, sDpNumber)); bGenPDF = true; break;
                                    }
                                }
                                ; break;
                            case "Save":
                                foreach (var option in coaPrintModel.option!)
                                {
                                    switch (option?.ToString())
                                    {
                                        case "PDF": if (!bGenPDF) { dataModels.Add(ExportPdf(controllerContext, oData.ExportPDF, sDpNumber)); }; break;
                                        case "Excel": dataModels.Add(ExportToExcel(oData.ExportExcel, sDpNumber)); break;
                                        case "Text": dataModels.Add(ExporTextFile(oData.ExportTextFile, sDpNumber)); break;
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
                                    var sSubFolder = Path.Combine(sCustomerId);
                                    await Task.Run(() => UploadFTPFile(_configuration, sFileName, sSubFolder));
                                }
                                ; break;
                        }
                    }
                }
                return dataModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<PrintCoaExportTempSP> CALLxSapAsync(IConfiguration _configuration, CoaPrintExportExecuteModel coaPrintExportExecuteModel, string sDpNumber)//#2 MockUp Test because don't connect to  Sap
        {
            try
            {
                List<CoaPrintExportDataModel> oDataModels = new();
                List<DTDeliveryInquiryResItems> oDTDeliveryInquiryResItems = new();
                PrintCoaExportTempSP oPrintCoaExportTempSP = new();
                var aOption = coaPrintExportExecuteModel.option;
                try
                {
                    foreach (var oItem in _SapShippingPointRepo.GetShippingPoints())
                    {
                        var oReq = new SI_DeliveryInquiry_OSRequest()
                        {
                            MT_DeliveryInquiryReq = new DT_DeliveryInquiryReq
                            {
                                IV_DELIVERY_NUMBER = sDpNumber,
                                IV_SHIPPING_POINT = oItem.Shipping_Point,
                                IV_ORG = oItem.Company_Code,
                                IV_ITEM_FLAG = "X"
                            }
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
                                            BatchNum = x.CHARG,
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
                        var oDTDeliveryInquiryResItems = new DTDeliveryInquiryResItems
                        {
                            DeliveryNum = oItem.DeliveryNum,
                            MaterialNum = oItem.MaterialNum,
                            BatchNum = oItem.BatchNum,
                        };
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
                                    oPrintCoaExportTempSP.ExportPDF.Merge(SET_DataForPDFFile(_configuration, oDTDeliveryInquiryResItems));
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
        private FileDataModel ExportToExcel(DataTable tbl, string sDpNumber)
        {
            try
            {
                string sPath = $"{_environment.ContentRootPath}" + $"{FileConstant.Template.UploadFTP}";
                var sDateTime = DateTime.Now.ToString("ddMMyyhhmmss");
                var res = new FileDataModel()
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
                    workbook.SaveAs(sPath + res.FileName);
                    workbook.SaveAs(ms);
                    res.FileSize = ms.Length;
                    res.FileData = ms.ToArray();
                    return res;
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
                    FileName = sDpNumber + ".txt",
                    FileExtension = ".txt"
                };
                var sMs = WriteTextFileWithDb(oPrintCoaExportTempSPs, res.FileName);
                res.FileSize = sMs.Length;
                res.FileData = sMs;
                if (res.FileSize > 0)
                {
                    using (var oWriterExport = new StreamWriter(sPath + res.FileName, false, Encoding.GetEncoding(874)))
                    {
                        oWriterExport.BaseStream.Write(sMs, 0, sMs.Length);
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
