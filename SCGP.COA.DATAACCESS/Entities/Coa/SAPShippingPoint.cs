using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGP.COA.DATAACCESS.Entities.Coa
{
    public partial class SAPShippingPoint
    {
        [Column(TypeName = "varchar(5)")]
        public string Company_Code { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string Shipping_Point { get; set; } = null!;
        [Column(TypeName = "char(10)")]
        public string InterCom_Status { get; set; } = null!;
    }
}
