using System;

namespace DashboardComm.Logging
{
    public interface ILogMessage
    {
        Exception CausalException { get; set; }
        string Message { get; set; }
        object StateData { get; set; }
        LogMessageType Type { get; set; }
        short Verbosity { get; set; }
    }
}