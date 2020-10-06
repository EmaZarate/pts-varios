using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.Exceptions.Common
{
    public class DuplicateEntityException : ApplicationException
    {
        public string Name { get; set; }
        public DuplicateEntityException(string name, string message) : base(message)
        {
            this.Name = name;
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
