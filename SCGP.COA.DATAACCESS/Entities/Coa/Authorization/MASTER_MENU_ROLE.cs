using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_MENU_ROLE")]
    public class MASTER_MENU_ROLE
    {
        public int MenuId { get; private set; }

        public int RoleId { get; private set; }

        [ForeignKey("MenuId")]
        public virtual MASTER_MENU MENU { get; set; }

        [ForeignKey("RoleId")]
        public virtual MASTER_ROLE ROLE { get; set; }

        public static MASTER_MENU_ROLE Create(MASTER_MENU menu, int roleId)
        {
            return new MASTER_MENU_ROLE()
            {
                MENU = menu,
                RoleId = roleId
            };
        }
    }
}
