using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.Sector
{
    public class SectorWithThisNameAndOrCodeAlreadyExists : ApplicationException
    {
        public string Name { get; set; }
        public string Code { get; set; }


        public SectorWithThisNameAndOrCodeAlreadyExists(string name, string code, string message) : base(message)
        {
            Name = name;
            Code = code;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Name, Code });
        }
    }
}