using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Models
{
    public class CoaPrintExportDataModel
    {   
        public PI pi { get; set; }
    }
    public class PI
    {
        public List<EO>? eo { get; set; }
    }
    public class EO
    {
        public List<string>? dp { get; set; }
    }
}
