using System.Collections.Generic;
using System.Diagnostics;

namespace DashboardComm.Logging
{
    public class EventLogService : LogService, ILogService
    {
        private string _applicationName;

        private Dictionary<LogMessageType, bool> _messageTypes = new Dictionary<LogMessageType, bool>();

        public EventLogService(string applicationName, ILogMessageFormatter formatter, byte verbosityLimit = 5) : base(formatter, verbosityLimit)
        {
            _applicationName = applicationName;
        }

        public void LogMessage(ILogMessage logMessage)
        {
            if (logMessage.Verbosity > _verbosityLimit)
            {
                return;
            }

            if (!_messageTypes.ContainsKey(logMessage.Type))
                _messageTypes.Add(logMessage.Type, true);
            EventLog oEventLog = new EventLog();
            var entryType = GetLogEntryType(logMessage.Type);

            // From command prompt: eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO MYEVENTSOURCE /D "My first log"
            // This will create a new event source named MYEVENTSOURCE under APPLICATION event log as INFORMATION event type.

            if (!EventLog.SourceExists(_applicationName))
            {
                EventLog.CreateEventSource(_applicationName, "Application");
            }

            oEventLog.Source = _applicationName;

            oEventLog.EnableRaisingEvents = true;
            oEventLog.WriteEntry(logMessage.Message, entryType, 1);  //Number...It doesn't have any meaning in this case.
            oEventLog.Close();
        }

        private EventLogEntryType GetLogEntryType(LogMessageType logMessageType)
        {
            EventLogEntryType type;
            switch (logMessageType)
            {
                case LogMessageType.Error:
                    type = EventLogEntryType.Error;
                    break;

                case LogMessageType.Warning:
                    type = EventLogEntryType.Warning;
                    break;

                default:
                    type = EventLogEntryType.Information;
                    break;
            }

            return type;
        }

        public bool HasMessageType(LogMessageType logMessageType)
        {
            if (!_messageTypes.ContainsKey(logMessageType))
            {
                return false;
            }
            else
            {
                return _messageTypes[logMessageType];
            }
        }
    }
}