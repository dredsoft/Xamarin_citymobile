﻿using System;
using System.IO;
using CityApp.Droid.Logging;
using CityApp.Utilities.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xamarin.Forms;
using ILogger = CityApp.Utilities.Logging.ILogger;
using LogManager = NLog.LogManager;

[assembly: Dependency(typeof(NLogManager))]
namespace CityApp.Droid.Logging
{
    public class NLogManager : ILogManager
    {
        public NLogManager()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(consoleRule);

            var fileTarget = new FileTarget();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileTarget.FileName = Path.Combine(folder, "Log.txt");
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Warn, fileTarget);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;
        }

        public ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
        {
            string fileName = callerFilePath;

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            }

            var logger = LogManager.GetLogger(fileName);
            return new NLogLogger(logger);
        }
    }
}