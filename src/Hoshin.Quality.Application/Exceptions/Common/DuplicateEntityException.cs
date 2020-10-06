using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Exceptions.Common
{
    public class DuplicateEntityException : ApplicationException
    {
        public string Name { get; set; }
        public int StatusCode { get; set; }


        public DuplicateEntityException(string name, string message): base(message)
        {
            this.Name = name;
        }

        public DuplicateEntityException(string name, string message,int statusCode) : base(message)
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
