using System;

namespace Logging
{
    public class Syslog : ILogs
    {
        // https://github.com/emertechie/SyslogNet
        // Critical - Events that demand immediate attention of sysadm
        // Error - Events that indicate problems, should be looked at when appropiate
        // Warning - Events that aren't ideal, but still manageable by the application
        // Information - An information event. This indicates a significant, successful operation.
        // Debug - Events relevant when troubleshooting/debugging
        // Trace - When you absolutely need every single possible detail about what's going on
        LogLevel ILogs.Level => throw new NotImplementedException();

        void ILogs.LogEvent(string timestamp, string message, LogLevel level)
        {
            throw new NotImplementedException();
        }
    }
}