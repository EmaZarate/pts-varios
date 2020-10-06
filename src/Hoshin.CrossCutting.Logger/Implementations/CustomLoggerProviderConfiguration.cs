using Microsoft.Extensions.Logging;

namespace Hoshin.CrossCutting.Logger.Implementations
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
