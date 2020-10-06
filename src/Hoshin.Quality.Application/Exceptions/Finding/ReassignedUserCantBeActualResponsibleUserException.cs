using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Quality.Application.Exceptions.Finding
{
    public class ReassignedUserCantBeActualResponsibleUserException : ApplicationException
    {
        public Domain.Finding.Finding Finding { get; set; }
        public string ReassignedUserID { get; set; }

        public ReassignedUserCantBeActualResponsibleUserException(Domain.Finding.Finding finding, string reassignedUserID) : base("El usuario responsable seleccionado debe ser distinto al actual")
        {
            Finding = finding;
            ReassignedUserID = reassignedUserID;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Finding, ReassignedUserID });
        }
    }
}
