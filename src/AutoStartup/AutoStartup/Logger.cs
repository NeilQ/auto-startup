using System;
using System.Diagnostics;

namespace AutoStartup
{
    public class Logger : ILogger
    {
        private readonly EventLog _eventLog = new EventLog();

        public Logger()
        {
            if (!EventLog.SourceExists("AutoStartup"))
            {
                EventLog.CreateEventSource("AutoStartup", "AutoStartup");
            }
            _eventLog.Source = "AutoStartup";
        }

        public void Log(string text)
        {
            _eventLog.WriteEntry(text);
        }

        public void Log(string text, LogType type)
        {
            EventLogEntryType logEntryType;
            switch (type)
            {
                case LogType.Info:
                    logEntryType = EventLogEntryType.Information;
                    break;
                case LogType.Warning:
                    logEntryType = EventLogEntryType.Warning;
                    break;
                case LogType.Error:
                    logEntryType = EventLogEntryType.Error;
                    break;
                default:
                    logEntryType = EventLogEntryType.Information;
                    break;
            }
            _eventLog.WriteEntry(text, logEntryType);
        }

        public void LogWithTimestamp(string text)
        {
            Log(GetTimestamp() + text);
        }

        public void LogWithTimestamp(string text, LogType type)
        {
            Log(GetTimestamp() + text, type);
        }

        private string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        }
    }
}