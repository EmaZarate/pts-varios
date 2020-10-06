using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Quality.Application.Exceptions.Common
{
    public class CantDesactivateException: ApplicationException
    {
        public string Name { get; set; }
        public int StatusCode { get; set; }

        public CantDesactivateException(string name, string message): base(message)
        {
            this.Name = name;
        }

        public CantDesactivateException(string name, string message, int statusCode) : base(message)
        {
            this.Name = name;
            this.StatusCode = statusCode;
            this.Data["StatusCode"] = statusCode;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Name });
        }
    }
}
