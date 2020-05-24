namespace Logging
{
    public interface ILogs
    {
        void LogEvent(string timestamp, string message, LogLevel level);
        LogLevel Level { get; }
    }
}