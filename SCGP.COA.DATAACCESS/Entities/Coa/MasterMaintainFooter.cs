using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_FOOTER")]
    public partial class MasterMaintainFooter
    {
        [Key]
        public int FooterId { get; set; }
        public string FormName { get; set; } = null!;
        public string? TextTestcondition { get; set; }
        public string? TextApprovedby { get; set; }
        public string? TextPrintedby { get; set; }
        public string? TextTelnumber { get; set; }
        public string? TextAdditional1 { get; set; }
        public string? TextAdditional2 { get; set; }
        public bool IsActive { get; set; }
    }
}
