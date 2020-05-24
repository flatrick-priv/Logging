using System;
using System.Collections.Generic;
using System.Configuration;
using Logging.Configuration;

/*
 * https://stackify.com/serilog-tutorial-net-logging/
 * This part of my code should be replaced by https://serilog.net/ in the end.
 * But this is for my own education to better understand how to do these things manually.
 */

namespace Logging
{
    public static class Logs
    {
        private static readonly List<ILogs> endpoints = new List<ILogs>();
        public static void SetupLogging()
        {
            LoggingConfig conf = (LoggingConfig)ConfigurationManager.GetSection("Logging");

            foreach (LoggingSection endpoint in conf.Endpoints)
            {
                switch (endpoint.Endpoint.ToLower())
                {
                    case "logtotext":
                        endpoints.Add(new LogToText(endpoint.Path,
          (LogLevel) Enum.Parse ( typeof(LogLevel), endpoint.LogLevel) ) );
                        break;
                    case "eventlog":
                        endpoints.Add(new WinEvent(endpoint.Path,
         (LogLevel) Enum.Parse ( typeof(LogLevel), endpoint.LogLevel) ) );
                        break;
                    default:
                        endpoints.Add(new WinEvent("Application",
                                                    LogLevel.Information));
                        break;
                }
            }
        }

        /// <summary>
        /// Takes the incoming logevent, adds a timestamp and then forwards it to each configured endpoint
        /// </summary>
        public static void LogMessage(string message, LogLevel level)
        {
            string timestamp = Timestamp();
            foreach (ILogs entry in endpoints)
            {
                // ILogs = entry.Key
                // Configured LogLevel = entry.Value
                if (level <= entry.Level)
                {
                    entry.LogEvent(timestamp, message, level);
                }
            }
        }

        internal static string Timestamp()
        {
            /* 
             * Date: https://en.wikipedia.org/wiki/ISO_8601#Dates
             * Time: https://en.wikipedia.org/wiki/ISO_8601#Times
             * This timestamp has the same precision as MS SQLs DateTime2
             * 2020-03-31T15:04:11.9778816+02:00 - LOGLEVEL - MACHINENAME - USER(@DOMAIN) - CONTEXT - MESSAGE
             */
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffK";
            return DateTime.Now.ToString(dateFormat);
        }
    }
}