using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Quality.Application.Exceptions.Finding
{
    public class FindingStateMustBeExpiredException : ApplicationException
    {
        public Domain.Finding.Finding Finding { get; set; }

        public FindingStateMustBeExpiredException(Domain.Finding.Finding finding) : base("El estado del hallazgo debe ser vencido")
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
