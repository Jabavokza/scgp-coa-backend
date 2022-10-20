using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SCGP.COA.COMMON.Models
{
    public class FileDataModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FilePathOriginal { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileData { get; set; }
        public long? FileSize { get; set; }

        public FileDataModel() { }
        public FileDataModel(string _filePath)
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                FilePath = _filePath;
                FileName = Path.GetFileName(_filePath);
                FileExtension = Path.GetExtension(_filePath);
            }

        }
        public FileDataModel(string name, string type, byte[] data, long? size)
        {
            FileName = name;
            FileExtension = type;
            FileData = data;
            FileSize = size;
        }

        public string GetFileName()
        {
            string name = FileName;
            if (!string.IsNullOrEmpty(FileExtension))
            {
                name = name.Replace(FileExtension, "");
            }
            return name;
        }
    }
}
