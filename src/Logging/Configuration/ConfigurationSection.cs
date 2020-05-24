using System.Configuration;

namespace Logging.Configuration
{
    // https://ivankahl.com/creating-custom-configuration-sections-in-app-config/
    // https://www.c-sharpcorner.com/article/add-custom-configuration-elements-in-net/
    // https://docs.microsoft.com/en-us/previous-versions/2tw134k3(v=vs.140)?redirectedfrom=MSDN

    /// <summary>
    /// The code for the sub-section of the app.config
    /// </summary>
    public class LoggingConfig : ConfigurationSection
    {
        [ConfigurationProperty("endpoints")]
        [ConfigurationCollection(typeof(LoggingCollection))]
        public LoggingCollection Endpoints
        {
            get
            {
                return (LoggingCollection)this["endpoints"];
            }
        }
    }
}