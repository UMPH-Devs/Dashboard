using System.Text;
using Newtonsoft.Json;

namespace DashboardComm.Logging
{
    public class LogMessageFormatterDynamic : ILogMessageFormatter
    {
        public string Format(ILogMessage logMessage)
        {
            return logMessage.StateData != null ? UseCustomFormatting(logMessage) : logMessage.Message;
        }

        internal string UseCustomFormatting(ILogMessage logMessage)
        {
            var stateData = JsonConvert.SerializeObject(logMessage.StateData);
            var customFormat = JsonConvert.DeserializeObject<CustomFormat>(stateData);

            if (string.IsNullOrWhiteSpace(customFormat.Layout)) return logMessage.Message;

            var useReplaceToken = !string.IsNullOrWhiteSpace(customFormat.ReplaceToken);
            if (customFormat.Elements == null)
            {
                return useReplaceToken
                    ? customFormat.Layout.Replace(customFormat.ReplaceToken, logMessage.Message)
                    : string.Format(customFormat.Layout, logMessage.Message);
            }
            if (customFormat.Elements != null && customFormat.Elements.Length > 0)
            {
                return useReplaceToken
                    ? SearchAndReplace(customFormat)
                    : string.Format(customFormat.Layout, customFormat.Elements);
            }
            return logMessage.Message;
        }

        internal string SearchAndReplace(CustomFormat customFormat)
        {
            var layout = customFormat.Layout;
            var arr = customFormat.Elements;
            var token = customFormat.ReplaceToken;
            var sb = new StringBuilder(layout);
            for (var i = 0; i < customFormat.Elements.Length; i++)
            {
                sb.Replace(token, arr[i].ToString(), layout.IndexOf(token), token.Length);
                layout = sb.ToString();
            }
            return layout;
        }
    }

    internal class CustomFormat
    {
        public string Layout { get; set; }
        public object[] Elements { get; set; }
        public string ReplaceToken { get; set; }
    }
}
