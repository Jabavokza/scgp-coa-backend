using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterAutoCoaCustomer
    {
        public string CustomerCode { get; set; } = null!;
        public string? ShiptoCode { get; set; }
        public bool AutocoaActive { get; set; }
    }
}
