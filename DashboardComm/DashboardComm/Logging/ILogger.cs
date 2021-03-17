using System;

namespace DashboardComm.Logging
{
    public interface ILogger
    {
        void Write(ILogMessage logMessage);

        void Write(string message, int verbosity = 0, LogMessageType logMessageType = LogMessageType.Information, Boolean isLogHtml = false);

        void Write(string message, Verbosity verbosity, LogMessageType logMessageType = LogMessageType.Information, Boolean isLogHtml = false);

        string GetLog();

        void ClearMessageLog();

        int VerbosityLimit { get; set; }
        bool DisableLogging { get; set; }
        DateTime? StartDateTime { get; set; }
        DateTime? EndDateTime { get; set; }
        TimeSpan? RunTime { get; }

        bool HasMsgType(LogMessageType logMessageType);
    }
}