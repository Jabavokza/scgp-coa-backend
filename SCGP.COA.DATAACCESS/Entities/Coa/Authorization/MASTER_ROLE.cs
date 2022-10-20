using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_ROLE")]
    public class MASTER_ROLE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; private set; }

        [StringLength(255)]
        public string RoleName { get; private set; }

        public bool ActiveFlag { get; private set; } = true;

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; private set; }

        public DateTime? UpdatedDate { get; private set; } = DateTime.Now;

        [StringLength(128)]
        public string? UpdatedBy { get; private set; }

        public static MASTER_ROLE Create(int appId, string createdBy)
        {
            return new MASTER_ROLE()
            {
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now
            };
        }

        public void Update(string roleName, bool activeFlag, string updatedBy)
        {
            RoleName = roleName;
            ActiveFlag = activeFlag;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }
    }
}
