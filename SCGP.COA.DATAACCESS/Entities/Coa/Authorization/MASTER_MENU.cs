using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_MENU")]
    public class MASTER_MENU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }


        [StringLength(255)]
        public string? Action { get; set; }

        public int Level { get; set; }

        public int? ParentMenu { get; set; }

        [StringLength(100)]
        public string? Icon { get; set; }


        public bool ActiveFlag { get; private set; } = true;

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; private set; }

        public DateTime? UpdatedDate { get; private set; } = DateTime.Now;

        [StringLength(128)]
        public string? UpdatedBy { get; private set; }

        public virtual List<MASTER_MENU_ROLE> Roles { get; set; }

        public static MASTER_MENU Create(int appId, string createdBy)
        {
            return new MASTER_MENU()
            {
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                Roles = new List<MASTER_MENU_ROLE>()
            };
        }

        public void Update(string menuName, string action, int level, int? parent, string icon
            , bool activeFlag, string updatedBy)
        {
            MenuName = menuName;
            Action = action;
            Level = level;
            ParentMenu = parent;
            Icon = icon;
            ActiveFlag = activeFlag;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }
    }
}
