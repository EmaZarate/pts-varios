using Hoshin.CrossCutting.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace Hoshin.CrossCutting.Logger.Implementations
{
    public class CustomLogger : ICustomLogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;
        string basePath = Environment.CurrentDirectory + "\\Logs\\";
        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            this.loggerName = name;
            loggerConfig = config;
        }
        public CustomLogger()
        {

        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = string.Format("{0} - {1}: {2} - {3}", DateTime.Now, logLevel.ToString(), eventId.Id, formatter(state, exception));
            WriteLogToFile(message, logLevel);
        }
        public void Log(LogLevel logLevel, EventId eventId, Exception exception, object additionalParams)
        {
            string message = $@"{DateTime.Now} - {logLevel.ToString()}: {eventId.Id}{Environment.NewLine}{GetLogContent(exception)}{Environment.NewLine}ADDITIONALPARAMS: {JsonConvert.SerializeObject(additionalParams)}";
            WriteLogToFile(message, logLevel);
        }
        private string GetLogContent(Exception exception)
        {
            return $"EXCEPTIONTYPE: {exception.GetType().Name}{Environment.NewLine}MESSAGE: {exception.Message}{Environment.NewLine}STACKTRACE: {exception.StackTrace}";
        }

        private void WriteLogToFile(string message, LogLevel level)
        {
            string fileNameDefault = "logs.txt";
            string fileName = null;
            switch (level)
            {
                case LogLevel.Information:
                    fileName = "logsInformation.txt";
                    break;
                case LogLevel.Warning:
                    fileName = "logsWarning.txt";
                    break;
                case LogLevel.Error:
                    fileName = "logsError.txt";
                    break;
                case LogLevel.Critical:
                    fileName = "logsCritical.txt";
                    break;
            }

            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

            if (fileName != null) WriteTextToFile(message, fileName);

            WriteTextToFile(message, fileNameDefault);
        }

        private void WriteTextToFile(string message, string fileName)
        {
            const int NumberOfRetries = 3;
            const int DelayOnRetry = 1000;

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(Path.Combine(basePath, fileName), true))
                    {
                        file.WriteLine(message);
                    }
                    break;
                }
                catch (IOException e) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
            }
        }

    }
}
