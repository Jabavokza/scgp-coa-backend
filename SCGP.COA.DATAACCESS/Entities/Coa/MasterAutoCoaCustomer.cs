using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterAutoCoaCustomer
    {
       
        public string CustomerCode { get; set; } = null!;
        public string? ShiptoCode { get; set; }
        public bool AutocoaActive { get; set; }
    }
}
