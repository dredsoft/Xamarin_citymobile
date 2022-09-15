using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CityApp.Utilities.Logging
{
    /// <summary>
    ///     Helper class to avoid overhead from using of dependency injection.
    /// </summary>
    public static class LogManager
    {
        private static ILogManager _platformInstance;

        static LogManager()
        {
            _platformInstance = DependencyService.Get<ILogManager>();
        }

        public static ILogger GetLog([CallerFilePath]string callerFilePath = "")
        {
            return _platformInstance.GetLog(callerFilePath);
        }
    }
}
