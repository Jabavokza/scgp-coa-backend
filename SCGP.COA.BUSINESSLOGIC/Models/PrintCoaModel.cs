using SCGP.COA.COMMON.Models;

namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class PrintCoaModel
    {
    }

    public class PrintCoaDataModel
    {
        public string? Grade { get; set; }
        public int? FormNo { get; set; }
    }

    public class PrintCoaExportPdfModel : PrintHtmlModel
    {
    }


}
