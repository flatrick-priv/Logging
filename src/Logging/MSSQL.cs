using System;

namespace Logging
{
    public class LogToSQL : ILogs
    {
        LogLevel ILogs.Level => throw new NotImplementedException();

        void ILogs.LogEvent(string timestamp, string message, LogLevel level)
        {
            throw new NotImplementedException();
        }
    }
}