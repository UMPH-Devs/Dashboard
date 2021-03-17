using System.Collections.Generic;

namespace DashboardComm.Logging
{
    public interface ILogServiceInteractive
    {
        void LogMessage(ILogMessage logMessage);
        bool HasMessageType(LogMessageType logMessageType);
        void ClearLog();
        string GetLogDump();
        List<ILogMessage> GetRawLog();
    }
}