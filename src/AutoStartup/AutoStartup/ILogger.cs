using System.Security.Cryptography.X509Certificates;

namespace AutoStartup
{
    public interface ILogger
    {
        void Log(string text);

        void Log(string text, LogType type);

        void LogWithTimestamp(string text);

        void LogWithTimestamp(string text, LogType type);
    }

    public enum LogType
    {
        Info,
        Warning,
        Error
    }
}