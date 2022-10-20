using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_GROUP_ROLE")]
    public class MASTER_GROUP_ROLE
    {
        [Key]
        public int GroupId { get; private set; }

        [Key]
        public int RoleId { get; private set; }

        [ForeignKey("GroupId")]
        public virtual MASTER_GROUP GROUP { get; set; }

        [ForeignKey("RoleId")]
        public virtual MASTER_ROLE ROLE { get; set; }

        public static MASTER_GROUP_ROLE Create(int roleId, MASTER_GROUP group)
        {
            return new MASTER_GROUP_ROLE
            {
                RoleId = roleId,
                GroupId = group.GroupId,
            };
        }
    }
}
