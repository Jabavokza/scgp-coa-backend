using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_MAINTAIN_FORM_TEMPLATE")]
    public partial class MasterFormTemplate
    {
        public MasterFormTemplate()
        {
            MasterForms = new HashSet<MasterMaintainForm>();
        }

        public int FormTemplateId { get; set; }
        public string FormTemplateName { get; set; } = null!;

        public virtual ICollection<MasterMaintainForm> MasterForms { get; set; }
    }
}
