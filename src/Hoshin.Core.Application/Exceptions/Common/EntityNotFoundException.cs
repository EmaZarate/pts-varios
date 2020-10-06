using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.Exceptions.Common
{
    public class EntityNotFoundException : ApplicationException
    {
        public string Id { get; set; }
        public EntityNotFoundException(string id, string message) : base(message)
        {
            this.Id = id;
        }
        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Id });
        }
    }
}
