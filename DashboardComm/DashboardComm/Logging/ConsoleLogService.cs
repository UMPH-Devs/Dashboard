using System;
using System.Collections.Generic;

namespace DashboardComm.Logging {
   public class ConsoleLogService : LogService, ILogService {
      private Dictionary<LogMessageType, bool> _messageTypes = new Dictionary<LogMessageType, bool>();
      
      public ConsoleLogService(ILogMessageFormatter formatter, byte verbosityLimit = 5) : base(formatter, verbosityLimit) {}

         

        public void LogMessage(ILogMessage logMessage) {
         if (logMessage.Verbosity > _verbosityLimit) {
            return;
         }

         if (!_messageTypes.ContainsKey(logMessage.Type))
            _messageTypes.Add(logMessage.Type, true);

         Console.WriteLine(_formatter.Format(logMessage));
      }

      public bool HasMessageType(LogMessageType logMessageType) {
         if (!_messageTypes.ContainsKey(logMessageType)) {
            return false;
         }
         else {
            return _messageTypes[logMessageType];
         }
      }
   }
}
