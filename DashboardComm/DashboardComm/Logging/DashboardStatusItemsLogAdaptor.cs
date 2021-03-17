using System.Collections.Generic;

namespace DashboardComm.Logging {
   public static class DashboardStatusItemsLogAdaptor {
      public static List<ModuleStatusInterface.StatusItem> StatusItemsGetDashboardUpdate(ILogServiceInteractive logger, string applicationId) {
         var StatusItems = new System.Collections.Generic.List<ModuleStatusInterface.StatusItem>();
         foreach (var logMessage in logger.GetRawLog()) {
            ModuleStatusInterface.ItemStatus status = DashboardStatusItemsLogAdaptor.TranslateLogStatusToDashboardStatus(logMessage.Type);
            var newstatusitem = new ModuleStatusInterface.StatusItem() {
               AppId = applicationId,
               Name = $"[LOG][{logMessage.Type.ToString().ToUpper()}]",
               Status = status,
               Value = logMessage.Message
            };
            StatusItems.Add(newstatusitem);
         }
         return StatusItems;
      }

      public static ModuleStatusInterface.ItemStatus TranslateLogStatusToDashboardStatus(LogMessageType logMessageType) {
         switch (logMessageType) {
            case LogMessageType.Error:
               return ModuleStatusInterface.ItemStatus.Error;
            case LogMessageType.Information:
               return ModuleStatusInterface.ItemStatus.Information;
            case LogMessageType.Warning:
               return ModuleStatusInterface.ItemStatus.Warning;
            default:
               return ModuleStatusInterface.ItemStatus.Information;

         }
      }
   }
}
