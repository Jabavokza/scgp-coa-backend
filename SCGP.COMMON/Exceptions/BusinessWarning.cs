using System;

namespace SCGP.COA.COMMON.Exceptions
{
    public class BusinessWarning: SystemException
    {
        public BusinessWarning(string msg,Exception ex = null) : base(msg,ex)
        {

        }
        //public static List<string> MsgWarning(string msg, Exception ex = null) 
        //{
        //    List<string> list = new List<string>();
        //    return list;
        //}
    }
}
