using System;

namespace SCGP.COA.COMMON.Exceptions
{
    public class UnauthorizedActionException : SystemException
    {
        public UnauthorizedActionException(string msg, Exception ex = null) : base(msg,ex)
        {

        }
    }
}
