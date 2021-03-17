namespace DashboardComm.Logging
{
    public class LogMessageFormatter : ILogMessageFormatter
    {
        public string Format(ILogMessage logMessage)
        {
            return $"[{logMessage.Type.ToString().ToUpper()}] {logMessage.Message}";
        }
    }
}
