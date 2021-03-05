using System;
using System.Collections.Generic;
using System.Web.Http;
using Dashboard.Models;

namespace Dashboard.Models
{
    public class JsonModuleStatus
    {
        public string Name { get; set; }
        public string AppId { get; set; }
        public string CustomHtml { get; set; }
        public int MinutesUntilWarning { get; set; }
        public int MinutesUntilError { get; set; }
        public string StatusLine { get; set; }
        public DateTime? CreateDate { get; set; }
        public JsonModuleType RefModuleType { get; set; }
        public List<JsonStatusItem> StatusItems { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public string LogText { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public bool IsLogHtml { get; set; }
        public bool IsForcedSuccess { get; set; }

        public bool IsInProgress { get; set; }

        public string InProgressUserid { get; set; }
    }
}
