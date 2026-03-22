using System;

namespace LoggerModule
{
    public interface ILoggerManager
    {
        void LogError(string message, Exception ex);
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
    }
}
