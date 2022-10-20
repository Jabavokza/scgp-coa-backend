using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterSiamToppanGrade
    {
        public int SiamToppanGradeId { get; set; }
        public string Grade { get; set; } = null!;
        public decimal? Gram { get; set; }
        public string? MaterialSale { get; set; }
        public string SiamToppanNumber { get; set; } = null!;
        public string? Remarks { get; set; }
    }
}
