using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.User
{
    public class UsersNotFoundException: ApplicationException
    {
        public int Id_sector { get; set; }
        public int Id_plant { get; set; }

        public UsersNotFoundException(int id_sector, int id_plant, string message) : base(message)
        {
            Id_sector = id_sector;
            Id_plant = id_plant;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { Id_sector, Id_plant });
        }
    }
}
