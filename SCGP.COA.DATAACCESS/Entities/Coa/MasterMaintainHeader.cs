using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_HEADER")]
    public partial class MasterMaintainHeader
    {
        [Key]
        public int HeaderId { get; set; }
        public string FormName { get; set; } = null!;
        public string DatetimeFormat { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
