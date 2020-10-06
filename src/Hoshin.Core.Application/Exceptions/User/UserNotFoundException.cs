using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hoshin.Core.Application.Exceptions.User
{
    public class UserNotFoundException : ApplicationException
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int StatusCode { get; set; }

        public UserNotFoundException(string user, string password, string message) : base(message)
        {
            User = user;
            Password = password;
        }

        public UserNotFoundException(string user, string password, string message,int statusCode) : base(message)
        {
            User = user;
            Password = password;
            StatusCode = statusCode;
            this.Data["StatusCode"] = statusCode;
        }

        /// <summary>
        /// Handles the exception, logging the error.
        /// </summary>
        /// <param name="_logger"></param>
        public override void HandleException(ICustomLogger _logger)
        {
            _logger.Log(LogLevel.Error, 0, this, new { User, Password });
        }
    }
}
