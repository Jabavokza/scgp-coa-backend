using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_FORM")]
    public partial class MasterMaintainForm
    {
        public int FormId { get; set; }
        public string FormName { get; set; } = null!;
        public int FormTemplateId { get; set; }
        public int? Property01Id { get; set; }
        public int? Property02Id { get; set; }
        public int? Property03Id { get; set; }
        public int? Property04Id { get; set; }
        public int? Property05Id { get; set; }
        public int? Property06Id { get; set; }
        public int? Property07Id { get; set; }
        public int? Property08Id { get; set; }
        public int? Property09Id { get; set; }
        public int? Property10Id { get; set; }
        public int? Property11Id { get; set; }
        public int? Property12Id { get; set; }
        public int? Property13Id { get; set; }
        public int? Property14Id { get; set; }
        public int? Property15Id { get; set; }
        public int? Property16Id { get; set; }
        public int? Property17Id { get; set; }
        public int? Property18Id { get; set; }
        public int? Property19Id { get; set; }
        public int? Property20Id { get; set; }
        public bool? IsActive { get; set; }

        public virtual MasterFormTemplate FormTemplate { get; set; } = null!;
        public virtual MasterMaintainProperty? Property01 { get; set; }
        public virtual MasterMaintainProperty? Property02 { get; set; }
        public virtual MasterMaintainProperty? Property03 { get; set; }
        public virtual MasterMaintainProperty? Property04 { get; set; }
        public virtual MasterMaintainProperty? Property05 { get; set; }
        public virtual MasterMaintainProperty? Property06 { get; set; }
        public virtual MasterMaintainProperty? Property07 { get; set; }
        public virtual MasterMaintainProperty? Property08 { get; set; }
        public virtual MasterMaintainProperty? Property09 { get; set; }
        public virtual MasterMaintainProperty? Property10 { get; set; }
        public virtual MasterMaintainProperty? Property11 { get; set; }
        public virtual MasterMaintainProperty? Property12 { get; set; }
        public virtual MasterMaintainProperty? Property13 { get; set; }
        public virtual MasterMaintainProperty? Property14 { get; set; }
        public virtual MasterMaintainProperty? Property15 { get; set; }
        public virtual MasterMaintainProperty? Property16 { get; set; }
        public virtual MasterMaintainProperty? Property17 { get; set; }
        public virtual MasterMaintainProperty? Property18 { get; set; }
        public virtual MasterMaintainProperty? Property19 { get; set; }
        public virtual MasterMaintainProperty? Property20 { get; set; }
    }
}
