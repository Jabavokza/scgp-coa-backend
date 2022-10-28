using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_FORM_COA")]
    public partial class MasterMaintainFormCoa
    {
        [Key]
        public int FormCoaId { get; set; }
        public int SequenceNo { get; set; }
        public string? Grade { get; set; }
        public decimal? Gram { get; set; }
        public string? MaterialSale { get; set; }
        public string? CustomerCode { get; set; }
        public int? FormPdfId { get; set; }
        public int? FormTextId { get; set; }
        public int? FormExcelId { get; set; }
        public bool IsActive { get; set; }
    }
}
