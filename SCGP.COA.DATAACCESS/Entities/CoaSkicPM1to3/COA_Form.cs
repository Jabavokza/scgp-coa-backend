using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.CoaSkicPM1to3
{
    [Table("COA_Form")]
    public class COA_Form
    {
        public string? Grade { get; set; }
        public string? Gram { get; set; }
        public int? FormNo { get; set; }
        public string? DescText { get; set; }
    }
}
