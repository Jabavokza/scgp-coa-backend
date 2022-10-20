using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.CoaSkicPM17Gypsum
{
    [Table("COA_Form")]
    public class COA_Form
    {
        [Key]
        public string? Grade { get; set; }
        public string? Gram { get; set; }
        public int? FormNo { get; set; }
        public string? DescText { get; set; }
        public string? MatSale { get; set; }
    }
}
