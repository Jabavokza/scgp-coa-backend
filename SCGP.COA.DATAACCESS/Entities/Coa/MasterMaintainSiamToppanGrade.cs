using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_SIAM_TOPPAN_GRADE")]
    public partial class MasterMaintainSiamToppanGrade
    {
        [Key]
        public int SiamToppanGradeId { get; set; }
        public string Grade { get; set; } = null!;
        public decimal? Gram { get; set; }
        //public string? MaterialSale { get; set; }
        public string SiamToppanNumber { get; set; } = null!;
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
