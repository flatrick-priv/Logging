using System;
using System.IO;
using Newtonsoft.Json;

namespace Logging
{
    // TODO: logrotate Main log upon either size OR date
    // TODO: a max size for all normal logs should be definable
    // TODO: a min size of free space before stopping service should be definable
    public class LogToText : ILogs
    {
        #region private variables
        private readonly string PathToLog;
        private readonly string mainLog;
        private readonly string errorLog;
        private readonly string debugLog;
        private readonly string traceLog;
        private readonly LogLevel logLevel;
        #endregion private variables

        #region Properties
        public LogLevel Level { get => logLevel; }
        #endregion Properties

        #region Constructors
        private LogToText() { }
        public LogToText(string Destination, LogLevel level)
        {
            this.PathToLog = Destination;
            this.mainLog = this.PathToLog + @"\main.jsonl";
            this.errorLog = this.PathToLog + @"\error.jsonl";
            this.debugLog = this.PathToLog + @"\debug.jsonl";
            this.traceLog = this.PathToLog + @"\trace.jsonl";
            this.logLevel = level;
        }
        #endregion Constructors

        /// <summary>
        /// Log events to files.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void LogEvent(string timestamp,
                             string message,
                             LogLevel level)
        {
            var eventObject = new EventObject
            {
                timestamp = timestamp,
                level = level.ToString(),
                message = message,
            };

            switch (this.Level)
            {
                case LogLevel.Critical:
                case LogLevel.Error:
                case LogLevel.Warning:
                    LogIt(eventObject, errorLog);
                    break;
                case LogLevel.Information:
                    if (level < LogLevel.Warning)  LogIt(eventObject, errorLog);
                    if (level <= LogLevel.Information)  LogIt(eventObject, mainLog);
                    break;
                case LogLevel.Debug:
                    if (level < LogLevel.Warning)  LogIt(eventObject, errorLog);
                    if (level <= LogLevel.Information)  LogIt(eventObject, mainLog);
                    LogIt(eventObject, debugLog);
                    break;
                case LogLevel.Trace:
                    if (level < LogLevel.Warning)  LogIt(eventObject, errorLog);
                    if (level <= LogLevel.Information)  LogIt(eventObject, mainLog);
                    LogIt(eventObject, traceLog);
                    break;
            }
        }

        // FIX: Verify if path is even a valid path to begin with, throw exception otherwise
        // FIX: If the creation of the destination-folder fails, throw exception
        /// <summary>
        /// The method that actually writes the log to disk.
        /// </summary>
        /// <param name="log"></param>
        internal void LogIt(EventObject log, string logFile)
        {
            CreateLogFolderIfMissing();
            WriteData(log, logFile);

        }

        internal void WriteData(EventObject log, string logFile)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(logFile, true);
                sw.WriteLine(JsonConvert.SerializeObject(log));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not write to the file {logFile}, verify permissions.\nException: {ex}");
                throw;
            }
        }

        internal void CreateLogFolderIfMissing()
        {
            if (!Directory.Exists(PathToLog))
            {
                try
                {
                    Directory.CreateDirectory(PathToLog);
                    Console.WriteLine($"The program created the folder {PathToLog}\nYippy!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The program was unable to create the folder {PathToLog}\nThe cause was: {ex}");
                    throw;
                }
            }
    }
}
    /// <summary>
    /// Object for holding the event to be logged
    /// </summary>
    internal class EventObject
    {
        // JsonProperty is required for any non-public property,
        // otherwise they won't get serialized by NewtonSoft.Json
        [JsonProperty]
        internal string timestamp;
        [JsonProperty]
        internal string level;
        [JsonProperty]
        internal string message;
    }
}