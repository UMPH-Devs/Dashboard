using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardComm.Logging
{
    public class Logger : ILogger
    {
        private List<ILogService> _loggingServices;
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public TimeSpan? RunTime
        {
            get
            {
                if (!StartDateTime.HasValue || !EndDateTime.HasValue)
                    return null;
                else if (EndDateTime < StartDateTime)
                    throw new Exception("End Date Time should not be smaller that Start Date Time in logger.");
                else
                    return (EndDateTime.Value - StartDateTime.Value);
            }
        }

        public virtual int VerbosityLimit
        {
            get
            {
                return (int)_verbosity;
            }
            set
            {
                _verbosity = (Verbosity)value;
            }
        }

        public Verbosity Verbosity
        {
            get { return _verbosity; }
            set { _verbosity = value; }
        }

        private Verbosity _verbosity;

        public virtual bool DisableLogging { get; set; }

        public virtual string GetLog()
        {
            var type = typeof(ILogServiceInteractive);
            var types = _loggingServices.Where(p => type.IsAssignableFrom(p.GetType()));
            if (types.Any())
            {
                return ((ILogServiceInteractive)types.First()).GetLogDump();
            }
            else
            {
                return "[CRITICAL ERROR] GetLog() called, but no provided logging service implements IMemoryLogService.";
            }
        }

        public virtual void ClearMessageLog()
        {
            var type = typeof(ILogServiceInteractive);
            var types = _loggingServices.Where(p => type.IsAssignableFrom(p.GetType()));
            if (types.Any())
            {
                foreach (ILogServiceInteractive log in types)
                {
                    log.ClearLog();
                }
            }
        }

        #region Constructors

        public Logger(List<ILogService> LoggingServices, int verbosityLimit = 3, bool disableLogging = false)
        {
            VerbosityLimit = verbosityLimit;
            DisableLogging = disableLogging;
            _loggingServices = LoggingServices;
            SelfValidate();
        }

        public Logger(string applicationName, int verbosityLimit = 3, bool disableLogging = false)
        { //Legacy Constructor #1
            VerbosityLimit = verbosityLimit;
            DisableLogging = disableLogging;
            var formatter = new LogMessageFormatter();
            _loggingServices = new List<ILogService>();
            _loggingServices.Add(new ConsoleLogService(formatter));
            _loggingServices.Add(new EventLogService(applicationName, formatter));
            _loggingServices.Add(new MemoryLogService(formatter));
            SelfValidate();
        }

        public Logger(List<ILogService> LoggingServices, Verbosity verbosity, bool disableLogging = false)
        {
            _verbosity = verbosity;
            DisableLogging = disableLogging;
            _loggingServices = LoggingServices;
            SelfValidate();
        }

        #endregion Constructors

        private void SelfValidate()
        {
            if (!_loggingServices.Any())
                throw new ArgumentException("Logger must have at least one logging service.");
            if (VerbosityLimit > 6 || VerbosityLimit < 0)
                throw new ArgumentException("Verbosity limit must be between 0 and 5.");
        }

        public virtual bool HasMsgType(LogMessageType logMessageType)
        {
            return this._loggingServices.First().HasMessageType(logMessageType); //The terrible assumption that all logging services are created equal.
        }

        public void Write(ILogMessage logMessage)
        {
            if (DisableLogging)
            {
                return;
            }
            else if (VerbosityLimit < logMessage.Verbosity)
            {
                return; //Don't log if message is above verbosity limit.
            }
            else
            {
                Parallel.ForEach(_loggingServices, log =>
                {
                    log.LogMessage(logMessage);
                });
            }
        }

        public void Write(string message, int verbosity = 0, LogMessageType logMessageType = LogMessageType.Information, Boolean isLogHtml = false)
        {
            if (isLogHtml)
            {
                message += "</br>";
            }

            Write(new LogMessage() { Message = message, Verbosity = Convert.ToInt16(verbosity), Type = logMessageType });
        }

        public void Write(string message, Verbosity verbosity, LogMessageType logMessageType = LogMessageType.Information, Boolean isLogHtml = false)
        {
            if (isLogHtml)
            {
                message += "</br>";
            }

            Write(new LogMessage() { Message = message, Verbosity = Convert.ToInt16(verbosity), Type = logMessageType });
        }
    }
}