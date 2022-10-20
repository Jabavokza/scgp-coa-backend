using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using System;

namespace SCGP.COA.COMMON.Exceptions
{
    public class BusinessException : SystemException
    {
        public List<string>? Errors { get; set; }
        public BusinessException(string msg,Exception? ex = null) : base(msg,ex)
        {

        }
        public BusinessException(List<string> errors) : base(errors.ListToString())
        {
            this.Errors = errors;
        }

        public static BusinessException ResponseResult<T>(ResponseResult<T> result)
        {
            return new BusinessException(result.Error)
            {
                Errors = result.Errors
            };
        }
    }
}
