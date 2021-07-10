using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace WebStoreLogger
{
    public class Log4NetLoggerProvider:ILoggerProvider
    {
        private readonly string configurationFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> loggers = new();

        public Log4NetLoggerProvider(string ConfigurationFile) => configurationFile = ConfigurationFile;

        public ILogger CreateLogger(string Category)=>
            loggers.GetOrAdd(Category, category =>
            {
                var xml = new XmlDocument();
                xml.Load(configurationFile);
                return new Log4NetLogger(category, xml["log4net"]);
            });

        public void Dispose() => loggers.Clear();
    }
}
