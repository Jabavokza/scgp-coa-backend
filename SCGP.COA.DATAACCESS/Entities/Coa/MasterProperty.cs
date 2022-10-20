using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterProperty
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string OutputName { get; set; } = null!;
        public string OutputFormat { get; set; } = null!;
    }
}
