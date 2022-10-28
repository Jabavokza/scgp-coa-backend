using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    [Table("MASTER_DATABASE")]
    public partial class MasterDatabase
    {
        [Key]
        public int DatabaseId { get; set; }
        public string APP { get; set; }
        public string MACHINE { get; set; }
        public string DB_HOST { get; set; }
        public string DB_NAME { get; set; }
        public string DB_UID { get; set; } 
        public string DB_PWD { get; set; }     
    }
}
