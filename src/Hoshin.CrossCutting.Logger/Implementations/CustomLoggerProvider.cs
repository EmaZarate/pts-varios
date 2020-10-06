using Hoshin.CrossCutting.Logger.Implementations;
using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Hoshin.CrossCutting.Logger.Implementations
{
    public class CustomLoggerProvider : ICustomLoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            loggerConfig = config;
        }
        public ILogger CreateLogger(string category)
        {
            return CreateCustomLogger(category);
        }

        public ICustomLogger CreateCustomLogger(string category)
        {
            return loggers.GetOrAdd(category,
             name => new CustomLogger(name, loggerConfig));
        }
        public void Dispose()
        {
            //Write code here to dispose the resources
        }
    }
}
