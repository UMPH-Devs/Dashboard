namespace DashboardComm.Logging
{
    public abstract class LogService
    {
        protected Verbosity _verbosity;
        protected byte _verbosityLimit
        {
            get
            {
                return (byte)_verbosity;
            }
            set
            {
                _verbosity = (Verbosity)value;
            }
        }
        protected ILogMessageFormatter _formatter;

        protected LogService(ILogMessageFormatter formatter, byte verbosityLimit = 5)
        {
            var verbo = (Verbosity)verbosityLimit;
            Constructor(formatter, verbo);
        }

        protected LogService(ILogMessageFormatter formatter, Verbosity verbosityLimit = Verbosity.Debug)
        {
            Constructor(formatter, verbosityLimit);
        }

        private void Constructor(ILogMessageFormatter formatter, Verbosity verbosityLimit = Verbosity.Debug)
        {
            _formatter = formatter == null ? new LogMessageFormatter() : formatter;
            _verbosity = verbosityLimit;
        }



    }
}
