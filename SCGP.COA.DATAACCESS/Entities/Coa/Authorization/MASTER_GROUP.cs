using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization
{
    [Table("MASTER_GROUP")]
    public class MASTER_GROUP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; private set; }

        [StringLength(100)]
        public string? GroupName { get; private set; }

        public bool ActiveFlag { get; private set; } = true;

        public bool IsAdmin { get; private set; }

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; private set; }

        public DateTime? UpdatedDate { get; private set; } = DateTime.Now;

        [StringLength(128)]
        public string? UpdatedBy { get; private set; }

        public virtual List<MASTER_GROUP_ROLE> GROUP_ROLEs { get; set; }


        public MASTER_GROUP()
        {
            GROUP_ROLEs = new List<MASTER_GROUP_ROLE>();
        }

        public static MASTER_GROUP Create(string groupName, bool activeFlag, string createdBy)
        {
            return new MASTER_GROUP()
            {
                GroupName = groupName,
                ActiveFlag = activeFlag,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
            };
        }

        public void Update(string groupName, bool activeFlag, string updatedBy)
        {
            GroupName = groupName;
            ActiveFlag = activeFlag;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }

        public void SetInactive(string updatedBy)
        {
            ActiveFlag = false;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
        }
    }
}
