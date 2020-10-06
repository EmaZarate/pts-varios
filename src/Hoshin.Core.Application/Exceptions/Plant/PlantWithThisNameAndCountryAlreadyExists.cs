using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.Plant
{
    public class PlantWithThisNameAndCountryAlreadyExists : ApplicationException
    {
        public string Name { get; set; }
        public string Country { get; set; }


        public PlantWithThisNameAndCountryAlreadyExists(string name, string country, string message) : base(message)
        {
            Name = name;
            Country = country;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Name, Country });
        }
    }
}