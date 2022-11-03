using SAP_Interface_DeliveryNum;
using SCGP.COA.COMMON.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SCGP.COA.BUSINESSLOGIC.Services.Interface
{
    public interface ISAPService
    {
        Task<SI_DeliveryInquiry_OSResponse> CallSAPDeliveryInquiry(SI_DeliveryInquiry_OSRequest request);
    }
}
