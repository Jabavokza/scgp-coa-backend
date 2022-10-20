using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterFormFooter
    {
        public int FormId { get; set; }
        public string? TextTestcondition { get; set; }
        public string? TextApprovedby { get; set; }
        public string? TextPrintedby { get; set; }
        public string? TextTelnumber { get; set; }
        public string? TextAdditional1 { get; set; }
        public string? TextAdditional2 { get; set; }
    }
}
