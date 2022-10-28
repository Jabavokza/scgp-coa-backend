using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_CUSTOMER_COA_OPTION")]
    public partial class MasterMaintainCustomerCoaOption
    {
        [Key]
        public int CustomerCoaOptionId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public bool DefaultOutputPdf { get; set; }
        public bool DefaultOutputText { get; set; }
        public bool DefaultOutputExcel { get; set; }
        public bool DefaultOutputDp { get; set; }
        public bool DefaultOutputDpBarcode { get; set; }
        public string CoaFooterText { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
