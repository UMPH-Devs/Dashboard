using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DashboardComm.Logging
{
    public class MemoryLogService : LogService, ILogService, ILogServiceInteractive, ILogServiceSerializedLoadable
    {
        private List<ILogMessage> _log = new List<ILogMessage>();

        public MemoryLogService(ILogMessageFormatter formatter, byte verbosityLimit = 5) : base(formatter, verbosityLimit)
        {
        }

        public void ClearLogAndLoadFromSerializedJson<T>(string jsonserializedlog)
        {
            if (String.IsNullOrWhiteSpace(jsonserializedlog)) //don't do anything here.
                return;
            this.ClearLog();
            _log = JsonConvert.DeserializeObject<List<ILogMessage>>(jsonserializedlog);
        }

        public string GetLogAsSerializedJson()
        {
            return JsonConvert.SerializeObject(_log);
        }

        public void LogMessage(ILogMessage logMessage)
        {
            if (logMessage.Verbosity > _verbosityLimit)
            {
                return;
            }
            _log.Add(logMessage);
        }

        public void ClearLog()
        {
            _log.Clear();
        }

        public List<ILogMessage> GetRawLog()
        {
            return _log;
        }

        public string GetLogDump()
        {
            List<string> logLines = new List<string>();
            int maxmsglength = 40000;
            foreach (var entry in _log.Distinct())
            {
                if (entry.Message.Length > maxmsglength)
                {
                    var originallength = entry.Message.Length;
                    var postpendmsg = $"...(Truncated by logger. Original msg length was {originallength})...";
                    var truncatedlength = maxmsglength - (postpendmsg.Length + 1);
                    entry.Message = $"{entry.Message.Substring(0, truncatedlength)} {postpendmsg}";
                }
                var line = _formatter.Format(entry);
                if (!logLines.Contains(line))
                {
                    logLines.Add(line);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var line in logLines)
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public bool HasMessageType(LogMessageType logMessageType)
        {
            return _log.Any(m => m.Type == logMessageType);
        }
    }
}