using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_USER_GROUP")]
    public class MASTER_USER_GROUP
    {
        [Key]
        [Column(Order = 1)]
        public Guid UserId { get; private set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public int GroupId { get; private set; }

        public static MASTER_USER_GROUP Create(Guid userId,int groupId)
        {
            return new MASTER_USER_GROUP()
            {
                UserId = userId,
                GroupId = groupId
            };
        }
    }
}
