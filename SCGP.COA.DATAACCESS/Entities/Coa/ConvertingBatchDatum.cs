using System;
using System.Collections.Generic;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class ConvertingBatchDatum
    {
        public string Batch { get; set; } = null!;
        public DateTime UploadedDatetime { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string Grade { get; set; } = null!;
        public decimal Gram { get; set; }
        public double? FilmThickness { get; set; }
        public double? Porosity { get; set; }
    }
}
