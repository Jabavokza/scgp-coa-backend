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
    public class PrintCoaDataSPModel
    {
        public string? BatchNumber { get; set; }
        public DateTime? ProductionDate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Length { get; set; }
        public string ? Property { get; set; }
        public decimal? PropertyValue { get; set; }
    }
    public class PrintCoaExportPdfModel : PrintHtmlModel
    {
    }


}
