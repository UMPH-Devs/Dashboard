using System;

namespace DashboardComm.Logging
{
    public class LogMessage : ILogMessage
    {
        /// <summary>
        /// Basic logging information
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Valid values are 0-5.  5 is least important, 0 is most important.
        /// 5 = Extreme detail
        /// 4 - Everything
        /// 3 - Might be useful to user, maybe.
        /// 2 - Something bad but minor happened.
        /// 1 - Recoverable error.
        /// 0 - Critical error.
        /// </summary>
        public Int16 Verbosity
        {
            get
            {
                return (Int16) _verbosity;
            }
            set
            {
                _verbosity = (Verbosity)value;
            }
        }
        private Verbosity _verbosity;

        public LogMessageType Type { get; set; }

        /// <summary>
        /// Will be serialized as json.  Can be null.
        /// </summary>
        public Exception CausalException { get; set; }

        /// <summary>
        /// Will be serialized as json.  Can be null. Stick whatever you might need for debugging purposes here in an anonymous object.
        /// </summary>
        public object StateData { get; set; }

        

    }
}
