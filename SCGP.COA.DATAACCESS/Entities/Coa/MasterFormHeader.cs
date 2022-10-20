using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class MasterFormHeader
    {
        public int FormTemplateId { get; set; }
        public string DatetimeFormat { get; set; } = null!;
    }
}
