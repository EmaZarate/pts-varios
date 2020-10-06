using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Exceptions.Finding
{
    public class CantDeleteFindingException : ApplicationException
    {
        public Domain.Finding.Finding Finding { get; set; }

        public CantDeleteFindingException(Domain.Finding.Finding finding) : base("El hallazgo no puede eliminarse")
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
