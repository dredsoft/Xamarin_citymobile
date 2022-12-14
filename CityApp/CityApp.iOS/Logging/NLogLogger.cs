using CityApp.iOS.Logging;
using NLog;
using Xamarin.Forms;
using ILogger = CityApp.Utilities.Logging.ILogger;

[assembly: Dependency(typeof(NLogLogger))]
namespace CityApp.iOS.Logging
{
    public class NLogLogger : ILogger
    {
        private Logger log;

        public NLogLogger(Logger log)
        {
            this.log = log;
        }

        public void Debug(string text, params object[] args)
        {
            log.Debug(text, args);
        }

        public void Error(string text, params object[] args)
        {
            log.Error(text, args);
        }

        public void Fatal(string text, params object[] args)
        {
            log.Fatal(text, args);
        }

        public void Info(string text, params object[] args)
        {
            log.Info(text, args);
        }

        public void Trace(string text, string memberName = "", int sourceLineNumber = 0)
        {
            log.Trace($"{memberName} {sourceLineNumber} {text}");
        }

        public void Warn(string text, params object[] args)
        {
            log.Warn(text, args);
        }
    }
}