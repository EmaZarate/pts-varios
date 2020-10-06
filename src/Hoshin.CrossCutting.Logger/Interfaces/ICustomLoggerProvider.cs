using Microsoft.Extensions.Logging;

namespace Hoshin.CrossCutting.Logger.Interfaces
{
    public interface ICustomLoggerProvider : ILoggerProvider
    {
        ICustomLogger CreateCustomLogger(string category);
    }
}
