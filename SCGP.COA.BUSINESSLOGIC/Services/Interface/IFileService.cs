using SCGP.COA.COMMON.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SCGP.COA.BUSINESSLOGIC.Services.Interface
{
    public interface IFileService
    {
        Task<FileDataModel> DownloadAppFile(string path);
        MemoryStream CloneExcelFileToMemoryStream(string filePath);
    }
}
