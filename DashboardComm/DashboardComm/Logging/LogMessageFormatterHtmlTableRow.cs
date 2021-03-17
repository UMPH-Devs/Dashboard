namespace DashboardComm.Logging
{
    public class LogMessageFormatterHtmlTableRow : ILogMessageFormatter
    {
        
            public string Format(ILogMessage logMessage)
            {
                string css = "style='background-color:#d9edf7;color:#000'";
                switch (logMessage.Type)
                {
                    case LogMessageType.Error:
                        css = "style='background-color:#f2dede;color:#000'"; //pink
                        break;
                    case LogMessageType.Warning:
                        css = "style='background-color:#fcf8e3;color:#000'"; //yellow
                        break;
                    case LogMessageType.Information:
                        css = "style='background-color:#d9edf7;color:#000'"; //blue
                        break;
                    default:
                        css = "style='background-color:#d9edf7;color:#000'"; //blue
                        break;
                }
                return $"<tr><td {css}>{logMessage.Type.ToString().ToUpper()}</td><td  {css}>{logMessage.Message}</td></tr>";
            }
        
    }
}
