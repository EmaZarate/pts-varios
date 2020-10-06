using Hoshin.CrossCutting.ErrorHandler.Implementations;
using Hoshin.CrossCutting.ErrorHandler.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hoshin.WebApi.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IErrorHandler _errorHandler;

        public WebApiExceptionFilterAttribute(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public override void OnException(ExceptionContext exceptionContext)
        {

            ErrorHandlerOutput errorHandlerOutput = _errorHandler.HandleException(exceptionContext.Exception);
            exceptionContext.HttpContext.Response.StatusCode = exceptionContext.Exception.Data["StatusCode"] != null ? (int)exceptionContext.Exception.Data["StatusCode"] : (int)errorHandlerOutput.HttpStatusCode;
            exceptionContext.Result = new JsonResult(errorHandlerOutput.Result);
            exceptionContext.ExceptionHandled = true;
        }
    }
}
