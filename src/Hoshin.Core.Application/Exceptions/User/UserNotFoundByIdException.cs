using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.User
{
    public class UserNotFoundByIdException: ApplicationException
    {
        public string ID { get; set; }
      

        public UserNotFoundByIdException(string id, string message) : base(message)
        {
            ID = id;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { ID });
        }
    }
}
