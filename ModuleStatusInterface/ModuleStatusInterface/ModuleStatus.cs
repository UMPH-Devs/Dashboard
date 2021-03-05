using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;


namespace ModuleStatusInterface {
   public class ModuleStatus {
      public ModuleStatus() {
         CreateDate = DateTime.Now;
         MachineName = Environment.MachineName;
         StatusItems = new List<StatusItem>();
      }
      public void AddStatus(string appId, string name, string value, ItemStatus status, bool display) {
         var statusItem = new StatusItem() {
            AppId = appId,
            Name = name,
            Value = value,
            Status = status,
            Display = display
         };

         StatusItems.Add(statusItem);
      }

      public string AppId { get; set; }
      public string CustomHtml { get; set; }
      public int MinutesUntilWarning { get; set; }
      public int MinutesUntilError { get; set; }
      public string MachineName { get; private set; }
      public string StatusLine { get; set; }
      public string Token => Tokenizer.GetToken(this);
      public DateTime CreateDate { get; private set; }
      public List<StatusItem> StatusItems { get; set; }
      public String LogText { get; set; }
      public bool IsLogHtml { get; set; } = false;
   }
}
