using Hoshin.CrossCutting.ErrorHandler.Interfaces;
using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Hoshin.CrossCutting.ErrorHandler.Implementations
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly ICustomLogger _logger;
        private readonly ICustomLogger _loggerApp;

        public ErrorHandler(ICustomLogger logger, ICustomLogger loggerApp)
        {
            _logger = logger;
            _loggerApp = loggerApp;
        }

        public ErrorHandlerOutput HandleException(Exception exception)
        {
            if (exception is Core.Application.Exceptions.ApplicationException)
            {
                (exception as Core.Application.Exceptions.ApplicationException).HandleException(_loggerApp);
            }
            else
            {
                _logger.LogError(exception.Message);
            }

            //notifications (CrossCutting.Message).

            return new ErrorHandlerOutput(HttpStatusCode.InternalServerError, new
            {                
                exceptiontype = exception.GetType().Name,
                message = exception.Message,
                stacktrace = exception.StackTrace,
                
            });
        }
    }
}
