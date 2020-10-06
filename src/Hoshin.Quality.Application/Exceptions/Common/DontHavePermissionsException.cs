using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Exceptions.Common
{
    class DontHavePermissionsException : ApplicationException
    {
        public DontHavePermissionsException(string message) : base(message) {
            this.Data["StatusCode"] = 403;
        }
        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, "User does not have permissions" );
        }
    }
}
