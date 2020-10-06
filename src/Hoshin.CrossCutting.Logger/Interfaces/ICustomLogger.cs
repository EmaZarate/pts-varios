using Microsoft.Extensions.Logging;
using System;

namespace Hoshin.CrossCutting.Logger.Interfaces
{
    public interface ICustomLogger : ILogger
    {
        void Log(LogLevel logLevel, EventId eventId, Exception exception, object additionalParams);
    }
}
