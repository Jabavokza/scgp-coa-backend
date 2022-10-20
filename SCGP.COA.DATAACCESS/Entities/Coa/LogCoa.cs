using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class LogCoa
    {
        public long LogId { get; set; }
        public DateTime LogTimestamp { get; set; }
        public string DocumentType { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
        public string OutputType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Message { get; set; }
    }
}
