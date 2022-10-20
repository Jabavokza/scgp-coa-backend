using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SCGP.COA.BUSINESSLOGIC.Services
{
    [TransientPriorityRegistration]
    public class FileService : IFileService
    {

        public async Task<FileDataModel> DownloadAppFile(string path)
        {
            if (Extension.IsLinux) path = path.Replace('\\', '/');
            var result = new FileDataModel(path);
            var file = await File.ReadAllBytesAsync(path);
            result.FileData = file;
            result.FileSize = file.Length;
            return result;
        }

        public MemoryStream CloneExcelFileToMemoryStream(string filePath)
        {
            try
            {
                if (Extension.IsLinux) filePath = filePath.Replace('\\', '/');
                MemoryStream output = new MemoryStream();
                using (FileStream templateExcel = File.OpenRead(filePath))
                {
                    using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(templateExcel))
                    {
                        package.SaveAs(output);
                    }
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
