using System;
using System.Diagnostics;

namespace Logging
{
    public class WinEvent : ILogs
    {
        /* Links of interest
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.eventlog?view=netcore-3.1
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.eventlogentry?view=netcore-3.1
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.eventlogentrytype?view=netcore-3.1
        */
        // TODO: Extend this class to send Event Logs to central server

        #region private variables
        private readonly string EventLogApp = "DataTransformer";    /* What application we identify as */
        private readonly string EventLogName;               /* Windows Event Log name/identifier */
        private readonly LogLevel logLevel;
        #endregion

        #region Properties
        public LogLevel Level { get => logLevel; }
        #endregion

        #region Strings
        private readonly string adminPermission = "You need to rerun this application once with elevated permissions!";
        #endregion

        #region Constructors
        /// <summary>
        ///  Ensure that it's impossible to create a WinEvent-object
        ///  without defining which log to send the data to.
        /// </summary>
        private WinEvent() { }
        public WinEvent(string Destination, LogLevel level)
        {
            this.EventLogName = Destination;
            this.logLevel = level;
            SetupEventLogs();
        }
        #endregion

        #region SetupEvents
        public void SetupEventLogs()
        {
            if (!TestEventSource())
            {
                CreateEventSource();
            }
        }

        public bool TestEventSource()
        {
            // If it can't find it in the usual spots, it will need elevated permissions.
            // Instead of throwing the exception saying it requires admin-permissions,
            // it should just swallow the exception and say "not found".
            try
            {
                return EventLog.SourceExists(EventLogApp);
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine($"{EventLogApp} wasn't found as an eligible Event Log Source");
                Console.WriteLine($"{ex}");
                return false;
            }
        }

        public void CreateEventSource()
        {
            // This requires that the process is running with elevated permissions
            // Since a running application can't elevate itself, 
            // this part might need to become a single DLL/exe that requests higher permissions upon startup.
            try
            {
                EventLog.CreateEventSource(EventLogApp, EventLogName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"I was unable to make {EventLogApp} an eligible source to write in the log {EventLogName}");
                Console.WriteLine("Try restarting the application once with elevated permissions");
                Console.WriteLine($"{ex}");
            }
        }

        #endregion

        #region StoringEvents

        public void LogEvent(string timestamp, string message, LogLevel level)
        {
            using EventLog appLog = new EventLog
            {
                Log = EventLogName,
                Source = EventLogApp
            };

            string logmsg = $"Timestamp: {timestamp}\nMessage: {message}";

            switch (level)
            {
                case LogLevel.Critical:
                    appLog.WriteEntry(logmsg, EventLogEntryType.Error);
                    appLog.Close();
                    break;

                case LogLevel.Error:
                    appLog.WriteEntry(logmsg, EventLogEntryType.Error);
                    appLog.Close();
                    break;

                case LogLevel.Warning:
                    appLog.WriteEntry(logmsg, EventLogEntryType.Warning);
                    appLog.Close();
                    break;

                case LogLevel.Information:
                case LogLevel.Debug:
                case LogLevel.Trace:
                    appLog.WriteEntry(logmsg, EventLogEntryType.Information);
                    appLog.Close();
                    break;

                default:
                    appLog.WriteEntry(logmsg, EventLogEntryType.Information);
                    appLog.Close();
                    break;
            }
        }

        #endregion StoringEvents
    }
}