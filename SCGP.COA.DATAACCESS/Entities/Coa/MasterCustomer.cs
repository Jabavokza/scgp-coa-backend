using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterCustomer
    {
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public bool DefaultOutputPdf { get; set; }
        public bool DefaultOutputText { get; set; }
        public bool DefaultOutputExcel { get; set; }
        public bool DefaultOutputDp { get; set; }
        public bool DefaultOutputDpBarcode { get; set; }
        public string CoaFooterText { get; set; } = null!;
    }
}
