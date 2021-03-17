namespace DashboardComm.Logging
{
    public interface ILogMessageFormatter
    {
        string Format(ILogMessage logMessage);
    }
}