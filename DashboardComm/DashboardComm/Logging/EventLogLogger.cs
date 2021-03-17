using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DashboardComm.Logging
{
    public class EventLogLogger : Logger, ILogger
    {
        private readonly StringBuilder _messagesWritten;
        private readonly StringBuilder _messageSummary;
        private readonly bool _writeToConsole;
        private readonly string _applicationName;
        private Dictionary<LogMessageType, bool> _hasLogDataFlags = new Dictionary<LogMessageType, bool>();


        public EventLogLogger(string applicationName, int verbosityLimit = 3, bool writeToConsole = true,
            bool disableLogging = false) : base(applicationName, verbosityLimit, disableLogging)
        {
            _applicationName = applicationName;
            VerbosityLimit = verbosityLimit;
            _messagesWritten = new StringBuilder();
            _messageSummary = new StringBuilder();
            DisableLogging = disableLogging;
            _writeToConsole = writeToConsole;
        }

        public override int VerbosityLimit { get; set; }
        public override bool DisableLogging { get; set; }

        public override bool HasMsgType(LogMessageType logMessageType)
        {
            if (this._hasLogDataFlags.ContainsKey(logMessageType))
            {
                return this._hasLogDataFlags[logMessageType];
            }
            else
            {
                return false;
            }
        }

        public void Write(string message, int verbosity = 0, LogMessageType logMessageType = LogMessageType.Information)
        {
            if (VerbosityLimit < verbosity) return; // don't log if this message is above the verbosity limit

            if (this._hasLogDataFlags.ContainsKey(logMessageType))
            {
                this._hasLogDataFlags[logMessageType] = true;
            }
            else
            {
                this._hasLogDataFlags.Add(logMessageType, true);
            }

            AddMessage(message);

            if (_writeToConsole)
            {
                WriteToConsole(message, logMessageType);
            }

            if (!DisableLogging)
            {
                EventLog oEventLog = new EventLog();
                var entryType = GetLogEntryType(logMessageType);
                
                if (!EventLog.SourceExists(_applicationName))
                {
                    EventLog.CreateEventSource(_applicationName, "Application");
                }

                oEventLog.Source = _applicationName;

                oEventLog.EnableRaisingEvents = true;
                oEventLog.WriteEntry(message, entryType);
                oEventLog.Close();
            }
        }

        public override void ClearMessageLog()
        {
            _messagesWritten?.Clear();
        }

        public override string GetLog()
        {
            var body = _messagesWritten.ToString();
            return body;
        }

        private void AddMessage(string message)
        {
            if (message.Trim().EndsWith("..."))
            {
                _messagesWritten.Append(message);
            }
            else
            {
                _messagesWritten.AppendLine(message);
            }
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

        private void WriteToConsole(string message, LogMessageType logMessageType)
        {
            var sb = new StringBuilder();

            switch (logMessageType)
            {
                case LogMessageType.Error:
                    sb.Append("ERROR: ");
                    break;
                case LogMessageType.Warning:
                    sb.Append("WARNING: ");
                    break;
                default:
                    sb.Append("INFO: ");
                    break;
            }

            sb.Append(message);
            Console.WriteLine(sb.ToString());
        }
    }
}
