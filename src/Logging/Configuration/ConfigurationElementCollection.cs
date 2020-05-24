using System;
using System.Configuration;

namespace Logging.Configuration
{
    // https://ivankahl.com/creating-custom-configuration-sections-in-app-config/
    // https://www.c-sharpcorner.com/article/add-custom-configuration-elements-in-net/
    // https://docs.microsoft.com/en-us/previous-versions/2tw134k3(v=vs.140)?redirectedfrom=MSDN

    /// <summary>
    /// Middlestep of the configuration management, this binds the different configuration elements into a collection
    /// </summary>
    public class LoggingCollection : ConfigurationElementCollection
    {
        public LoggingSection this[int index]
        {
            get { return (LoggingSection)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public new LoggingSection this[string key]
        {
            get { return (LoggingSection)BaseGet(key); }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggingSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("GetElementKey was given a null value");
            }
            else
            {
                return ((LoggingSection)element).Endpoint;
            }
        }
    }
}