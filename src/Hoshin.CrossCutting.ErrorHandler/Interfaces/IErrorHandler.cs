using Hoshin.CrossCutting.ErrorHandler.Implementations;
using System;

namespace Hoshin.CrossCutting.ErrorHandler.Interfaces
{
    public interface IErrorHandler
    {
        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        ErrorHandlerOutput HandleException(Exception exception);
    }
}
