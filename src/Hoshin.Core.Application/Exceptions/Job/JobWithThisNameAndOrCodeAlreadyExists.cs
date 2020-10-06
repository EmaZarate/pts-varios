using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.Job
{
    public class JobWithThisNameAndOrCodeAlreadyExists : ApplicationException
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public JobWithThisNameAndOrCodeAlreadyExists(string name, string code, string message) : base(message)
        {
            Name = name;
            Code = code;
        }

        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Name, Code });
        }
    }
}
