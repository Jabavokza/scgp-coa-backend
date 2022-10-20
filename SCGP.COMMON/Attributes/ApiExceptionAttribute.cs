using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCGP.COA.COMMON.Exceptions;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;

using System;
using System.Net;

namespace SCGP.COA.COMMON.Attributes
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            var msg = string.Empty;
            msg = actionExecutedContext.Exception.GetMessageError();

            actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            actionExecutedContext.HttpContext.Response.ContentType = "application/json";
            var errorModel = ResponseResult<bool?>.Fail(msg);
            if (actionExecutedContext.Exception != null)
            {

                if (actionExecutedContext.Exception.GetType() == typeof(UnauthorizedAccessException))
                {
                    actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    actionExecutedContext.Result = new JsonResult(msg);
                }
                else if (actionExecutedContext.Exception.GetType() == (typeof(Exception)))
                {
                    actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    actionExecutedContext.Result = new JsonResult(errorModel);
                }
                else if (actionExecutedContext.Exception.GetType() == (typeof(BusinessException)))
                {
                    var m = (BusinessException)actionExecutedContext.Exception;

                    if (m.Errors.AnyAndNotNull())
                    {
                        var error = ResponseResult<List<string>>.Fail(m.Errors);
                        actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        actionExecutedContext.Result = new JsonResult(error);
                    }
                    else
                    {
                        actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        actionExecutedContext.Result = new JsonResult(errorModel);
                    }

                }
                else if (actionExecutedContext.Exception.GetType() == typeof(UnauthorizedActionException))
                {
                    errorModel.Status = (int)HttpStatusCode.NotAcceptable;
                    actionExecutedContext.Result = new JsonResult(errorModel);
                }
                else if (actionExecutedContext.Exception.GetType() == (typeof(BusinessWarning)))
                {
                    errorModel.Status = (int)HttpStatusCode.PreconditionRequired;
                    actionExecutedContext.Result = new JsonResult(errorModel);
                }
                else
                {
                    actionExecutedContext.Result = new JsonResult(errorModel);
                }
                base.OnException(actionExecutedContext);
            }
        }
    }
}
