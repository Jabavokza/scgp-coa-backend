using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_PROPERTY")]
    public partial class MasterMaintainProperty
    {
        public MasterMaintainProperty()
        {
            MasterFormProperty01s = new HashSet<MasterMaintainForm>();
            MasterFormProperty02s = new HashSet<MasterMaintainForm>();
            MasterFormProperty03s = new HashSet<MasterMaintainForm>();
            MasterFormProperty04s = new HashSet<MasterMaintainForm>();
            MasterFormProperty05s = new HashSet<MasterMaintainForm>();
            MasterFormProperty06s = new HashSet<MasterMaintainForm>();
            MasterFormProperty07s = new HashSet<MasterMaintainForm>();
            MasterFormProperty08s = new HashSet<MasterMaintainForm>();
            MasterFormProperty09s = new HashSet<MasterMaintainForm>();
            MasterFormProperty10s = new HashSet<MasterMaintainForm>();
            MasterFormProperty11s = new HashSet<MasterMaintainForm>();
            MasterFormProperty12s = new HashSet<MasterMaintainForm>();
            MasterFormProperty13s = new HashSet<MasterMaintainForm>();
            MasterFormProperty14s = new HashSet<MasterMaintainForm>();
            MasterFormProperty15s = new HashSet<MasterMaintainForm>();
            MasterFormProperty16s = new HashSet<MasterMaintainForm>();
            MasterFormProperty17s = new HashSet<MasterMaintainForm>();
            MasterFormProperty18s = new HashSet<MasterMaintainForm>();
            MasterFormProperty19s = new HashSet<MasterMaintainForm>();
            MasterFormProperty20s = new HashSet<MasterMaintainForm>();
        }

        [Key]
        public int PropertyId { get; set; }
        public string PropertyName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string OutputName { get; set; } = null!;
        public string OutputFormat { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<MasterMaintainForm> MasterFormProperty01s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty02s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty03s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty04s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty05s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty06s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty07s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty08s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty09s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty10s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty11s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty12s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty13s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty14s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty15s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty16s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty17s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty18s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty19s { get; set; }
        public virtual ICollection<MasterMaintainForm> MasterFormProperty20s { get; set; }
    }
}
