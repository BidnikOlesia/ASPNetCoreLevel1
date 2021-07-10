using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using log4net;
using Microsoft.Extensions.Logging;

namespace WebStoreLogger
{

    public class Log4NetLogger:ILogger
    {
        private readonly ILog log;

        public Log4NetLogger(string Category, XmlElement Configuration) 
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            log = LogManager.GetLogger(logger_repository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => log.IsDebugEnabled,
            LogLevel.Debug => log.IsDebugEnabled,
            LogLevel.Information => log.IsInfoEnabled,
            LogLevel.Warning => log.IsWarnEnabled,
            LogLevel.Error => log.IsErrorEnabled,
            LogLevel.Critical => log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentNullException(nameof(formatter));

            if (!IsEnabled(logLevel))
                return;

            var log_message = formatter(state, exception);

            if (string.IsNullOrEmpty(log_message) && exception is null) 
                return;

            switch (logLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);

                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    log.Debug(log_message);
                    break;

                case LogLevel.Information:
                    log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    log.Error(log_message, exception);
                    break;

                case LogLevel.Critical:
                    log.Fatal(log_message, exception);
                    break;

            }
        }
    }
}
