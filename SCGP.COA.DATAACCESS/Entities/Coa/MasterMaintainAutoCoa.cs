using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_AUTO_COA")]
    public partial class MasterMaintainAutoCoa
    {
        [Key]
        public int AutoCoaId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string? ShipToCode { get; set; }
        public bool AutocoaActive { get; set; }
        public bool IsActive { get; set; }
    }
}
