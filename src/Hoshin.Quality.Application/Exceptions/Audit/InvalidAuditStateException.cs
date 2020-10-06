using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Exceptions.Audit
{
    public class InvalidAuditStateException : ApplicationException
    {
        public InvalidAuditStateException(string message) : base(message)
        {
            this.Data["StatusCode"] = 403;
        }
        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, "Audit can't be deleted in this state");
        }
    }
}
