using System.Data;

namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class PrintCoaExportTempSP
    {
        public DataTable ExportTextFile { get; set; } =new DataTable();
        public DataTable ExportExcel { get; set; } =new DataTable();
        public DataTable ExportPDF { get; set; } =new DataTable();
    }
}
