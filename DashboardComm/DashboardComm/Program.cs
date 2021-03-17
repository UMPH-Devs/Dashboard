using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DashboardComm.Logging;
using ModuleStatusInterface;
using Newtonsoft.Json;

namespace DashboardComm
{
    internal class Program
    {
        private static Arguments ogArguments;

        private static void Main(string[] args)
        {
            ogArguments = new Arguments(args);

            if (GetSetting("?") != null || GetSetting("h") != null || GetSetting("help") != null)
            {
                Console.WriteLine(DisplayUsage());
                return;
            }

            ProcessRequest();
        }

        private static string DisplayUsage()
        {
            StringBuilder oStringBuilder = new StringBuilder();

            oStringBuilder.AppendLine("Usage -> DashboardComm.exe [--help] [--KEY=X]");
            oStringBuilder.AppendLine();
            oStringBuilder.AppendLine("         --help|-h|/?            - This message.");
            oStringBuilder.AppendLine();
            oStringBuilder.AppendLine("         --EventSource           - Default: DashboardComm*");
            oStringBuilder.AppendLine("         --AppId                 - Default: DashboardComm");
            oStringBuilder.AppendLine("         --TargetEnvironment     - Valid values are dev, stg, and prd. Default: stg");
            oStringBuilder.AppendLine("         --MinutesUntilWarning   - Default: 1450");
            oStringBuilder.AppendLine("         --MinutesUntilError     - Default: 1500");
            oStringBuilder.AppendLine("         --LogMessage");
            oStringBuilder.AppendLine("         --IsLogHtml             - Default: false");
            oStringBuilder.AppendLine("         --CustomHtml");
            oStringBuilder.AppendLine("         --StatusesFile          - The path to a file containing Statuses in a JSON object (see below)");
            oStringBuilder.AppendLine("         --Statuses              - A set of JSON objects** with the following properties: ");
            oStringBuilder.AppendLine("                                        AppId: quoted string");
            oStringBuilder.AppendLine("                                         Name: quoted string");
            oStringBuilder.AppendLine("                                        Value: quoted string");
            oStringBuilder.AppendLine("                                      Display: true or false - no quotes");
            oStringBuilder.AppendLine("                                       Status: 0 (Success), 1 (Warning), 2 (Error), or 3 (Information) - no quotes");
            oStringBuilder.AppendLine();
            oStringBuilder.AppendLine("         --KEY=X                 - Any key in the appSettings section of the .config file may be overriden via the command.");
            oStringBuilder.AppendLine("                                   KEY is the corresponding name of the key in the .config file.");
            oStringBuilder.AppendLine("");
            oStringBuilder.AppendLine("*To create initial Event Log from the command prompt as Administrator type:");
            oStringBuilder.AppendLine("");
            oStringBuilder.AppendLine("   eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO DashboardComm /D \"My first log\"");
            oStringBuilder.AppendLine("");
            oStringBuilder.AppendLine("This will create a new event source named \"DashboardComm\" under APPLICATION event log as INFORMATION event type.");
            oStringBuilder.AppendLine("\"Dashboardcomm\" is the devault event source if not overridden.");
            oStringBuilder.AppendLine("");
            oStringBuilder.AppendLine("**JSON object must have quotes escaped or use alternating quotes \" vs ' for nested quotes.");
            oStringBuilder.AppendLine("  Display and Status values should not be enclosed in quotes.");
            oStringBuilder.AppendLine("   {\'Item1\':{\'AppId\':\'AppId\',\'Name\':\'StatusName1\',\'Value\':\'foo1\',\'Display\':false,\'Status\':1},");
            oStringBuilder.AppendLine("    \'Item2\':{\'AppId\':\'AppId\',\'Name\':\'StatusName2\',\'Value\':\'foo2\',Display:\'false\',\'Status\':0}}");

            return (oStringBuilder.ToString());
        }

        private static string GetSetting(string sName)
        {
            if (!string.IsNullOrWhiteSpace(ogArguments[sName]))
            {
                return ogArguments[sName];
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings[sName];
            }
        }

        private static int GetIntSetting(string sName)
        {
            int.TryParse(GetSetting(sName), out int iResult);

            return iResult;
        }

        private static Boolean GetBoolSetting(string sName)
        {
            Boolean.TryParse(GetSetting(sName), out Boolean bResult);

            return bResult;
        }

        private static void ProcessRequest()
        {
            var oMemoryLogService = new MemoryLogService(new LogMessageFormatter());
            Logger oLogger = GetLoggerInstance(oMemoryLogService);

            oLogger.Write("Time: " + DateTime.Now + Environment.NewLine, Verbosity.Information, LogMessageType.Information, GetBoolSetting("IsLogHtml"));
            oLogger.Write("Log Message:" + Environment.NewLine + Environment.NewLine + GetSetting("LogMessage"), Verbosity.Information, LogMessageType.Information, GetBoolSetting("IsLogHtml"));

            UpdateDashboard(oLogger);
        }

        private static Logger GetLoggerInstance(ILogService memorylogger)
        {
            // From command prompt: eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO MYEVENTSOURCE /D "My first log"
            // This will create a new event source named MYEVENTSOURCE under APPLICATION event log as INFORMATION event type.

            List<ILogService> loggingServices =
                new List<ILogService>
                {
                    memorylogger,
                    new EventLogService(GetSetting("EventSource"), new LogMessageFormatterTerse())
                };

            return new Logger(loggingServices);
        }

        private static void UpdateDashboard(Logger oLogger)
        {
            try
            {
                Console.WriteLine(Environment.NewLine + "AppId = " + GetSetting("AppId"));
                Console.WriteLine("MinutesUntilWarning = " + GetIntSetting("MinutesUntilWarning"));
                Console.WriteLine("MinutesUntilError = " + GetIntSetting("MinutesUntilError"));

                dynamic oStatusItems;

                if (!string.IsNullOrWhiteSpace(GetSetting("StatusesFile")))
                {
                    string sStatusesJSON = File.ReadAllText(GetSetting("StatusesFile"));
                    oStatusItems = JsonConvert.DeserializeObject<dynamic>(sStatusesJSON);
                }
                else
                {
                    oStatusItems = JsonConvert.DeserializeObject<dynamic>(GetSetting("Statuses"));
                }

                // Begin - Uncomment for testing
                //var sTestString = "{\"Item1\":{\"AppId\":\"AppId\",\"Name\":\"StatusName1\",\"Value\":\"foo1\",\"Display\":false,\"Status\":1},\"Item2\":{\"AppId\":\"AppId\",\"Name\":\"StatusName2\",\"Value\":\"foo2\",Display:\"false\",\"Status\":0}}";
                //oStatusItems = JsonConvert.DeserializeObject<dynamic>(sTestString);
                // End - Uncomment for testing

                List<StatusItem> oStatusItemsList = new List<StatusItem>();

                ModuleStatusInterface.ItemStatus oStatusItemStatus;

                foreach (var oStatusItem in oStatusItems)
                {
                    Boolean.TryParse(oStatusItem.First.Display.ToString(), out Boolean bDisplay);
                    int.TryParse(oStatusItem.First.Status.ToString(), out int iStatus);

                    switch (iStatus)
                    {
                        case 0:
                            oStatusItemStatus = ItemStatus.Success;
                            break;

                        case 1:
                            oStatusItemStatus = ItemStatus.Warning;
                            break;

                        case 2:
                            oStatusItemStatus = ItemStatus.Error;
                            break;

                        default:
                            oStatusItemStatus = ItemStatus.Information;
                            break;
                    }

                    oStatusItemsList.Add(
                        new StatusItem
                        {
                            AppId = oStatusItem.First.AppId,
                            Name = oStatusItem.First.Name,
                            Value = oStatusItem.First.Value,
                            Display = bDisplay,
                            Status = oStatusItemStatus
                        }
                        );

                    Console.WriteLine(Environment.NewLine + "Item Status:");
                    Console.WriteLine("   AppId = " + oStatusItem.First.AppId);
                    Console.WriteLine("   Name = " + oStatusItem.First.Name);
                    Console.WriteLine("   Value = " + oStatusItem.First.Value);
                    Console.WriteLine("   Display = " + bDisplay);
                    Console.WriteLine("   Status = " + oStatusItemStatus);
                }

                ModuleStatus oModuleStatus = new ModuleStatus
                {
                    AppId = GetSetting("AppId"),
                    MinutesUntilWarning = GetIntSetting("MinutesUntilWarning"),
                    MinutesUntilError = GetIntSetting("MinutesUntilError"),
                    StatusItems = oStatusItemsList,
                    IsLogHtml = GetBoolSetting("IsLogHtml"),
                    CustomHtml = GetSetting("CustomHtml"),
                    LogText = oLogger.GetLog()
                };

                var oSendStatus = pushStatus(oModuleStatus, GetSetting("TargetEnvironment"));
            }
            catch (Exception oException)
            {
                Console.WriteLine("Error during UpdateDashboard: " + oException.Message + "... " + oException.InnerException?.Message);
                oLogger.Write("Error during UpdateDashboard: " + oException.Message + "... " + oException.InnerException?.Message);
            }
        }

        public static async Task<ModuleStatus> pushStatus(ModuleStatus status, string sEnvironment)
        {
            return Messenger.SendStatusSync(Messenger.GetTargetFromString(sEnvironment), status);
        }
    }
}
