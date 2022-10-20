using System.Collections;

namespace SCGP.COA.DATAACCESS.Models
{
    public class AutoCoaLogModel
    {
        public DateTime? jobTimestampTo { get; set; }
        public DateTime? jobTimestampFrom { get; set; }
        public string[]? documentType { get; set; } 
        public string? documentNumber { get; set; }
        public string[]? outputType { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
    }
}
