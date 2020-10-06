using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Quality.Application.Exceptions.Finding
{
    public class InvalidExpirationDateException : ApplicationException
    {
        public Domain.Finding.Finding Finding { get; set; }

        public InvalidExpirationDateException(Domain.Finding.Finding finding) : base("La fecha de vencimiento debe ser mayor a la fecha actual")
        {
            Finding = finding;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Finding });
        }
    }
}
