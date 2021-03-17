namespace DashboardComm.Logging {
   public interface ILogService {
      void LogMessage(ILogMessage logMessage);
      bool HasMessageType(LogMessageType logMessageType);
   }
}