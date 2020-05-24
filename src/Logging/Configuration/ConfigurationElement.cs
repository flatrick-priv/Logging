using System;
using System.Configuration;

namespace Logging.Configuration
{
    // https://ivankahl.com/creating-custom-configuration-sections-in-app-config/
    // https://www.c-sharpcorner.com/article/add-custom-configuration-elements-in-net/
    // https://docs.microsoft.com/en-us/previous-versions/2tw134k3(v=vs.140)?redirectedfrom=MSDN

    /// <summary>
    /// Each configuration option are defined here.
    /// </summary>
    public class LoggingSection : ConfigurationElement
    {
        /// <summary>
        /// Which driver/endpoint to log events with.
        /// </summary>
        [ConfigurationProperty("endpoint", DefaultValue = "EventLog", IsKey = true, IsRequired = true)]
        public string Endpoint
        {
            get { return (string)base["endpoint"]; }
            set { base["endpoint"] = value; }
        }

        /// <summary>
        ///  Application name used for sending events to the EventLog
        ///  I.e, mainly/only relevant for the class WinEvent
        /// </summary>
        [ConfigurationProperty("sourceName", DefaultValue = "DataTransformer", IsKey = true, IsRequired = false)]
        public string SourceName
        {
            get { return (string)base["sourceName"]; }
            set { base["sourceName"] = value; }
        }

        /// <summary>
        /// Path for plain text logs
        /// </summary>
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Path
        {
            get { return Environment.ExpandEnvironmentVariables((string)base["path"]); }
            set { base["path"] = value; }
        }

        /// <summary>
        /// Level of detail for the logs (see Enums.cs "LogLevel" for available choices)
        /// </summary>
        [ConfigurationProperty("logLevel", DefaultValue = "Information", IsKey = true, IsRequired = false)]
        public string LogLevel
        {
            get { return (string)base["logLevel"]; }
            set { base["logLevel"] = value; }
        }
    }
}