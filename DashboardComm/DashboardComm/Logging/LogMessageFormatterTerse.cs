using System;

namespace DashboardComm.Logging
{
    public class LogMessageFormatterTerse : ILogMessageFormatter
    {

        public string Format(ILogMessage logMessage)
        {
            string template = "* {0,3} {1, 1}";
            string statustext = String.Empty;
            string bodytext = String.Empty;
            
            switch (logMessage.Type)
            {
                
                case LogMessageType.Information:
                    statustext = String.Empty;
                    bodytext = logMessage.Message;
                    break;
                case LogMessageType.Error:
                case LogMessageType.Warning:
                    statustext = $"[{logMessage.Type.ToString().Substring(0,1).ToUpper()}]";
                    bodytext = logMessage.Message;
                    break;
                default:
                    statustext = $"[{logMessage.Type.ToString().ToUpper()}]";
                    bodytext = logMessage.Message;
                    break;
            }
            return String.Format(template, statustext, bodytext);

        }

    }
}
