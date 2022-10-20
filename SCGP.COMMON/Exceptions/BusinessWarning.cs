using System;

namespace SCGP.COA.COMMON.Exceptions
{
    public class BusinessWarning: SystemException
    {
        public BusinessWarning(string msg,Exception ex = null) : base(msg,ex)
        {

        }
    }
}
