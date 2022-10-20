using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterFormMappingRule
    {
        public int RuleId { get; set; }
        public int RuleOrder { get; set; }
        public string? Grade { get; set; }
        public decimal? Gram { get; set; }
        public string? MaterialSale { get; set; }
        public string? CustomerCode { get; set; }
        public int? FormPdfId { get; set; }
        public int? FormTextId { get; set; }
        public int? FormExcelId { get; set; }
    }
}
