﻿using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Hoshin.Core.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public virtual void HandleException(ICustomLogger _logger)
        {
            _logger.LogError(Message);
        }
    }
}
