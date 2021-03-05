using System;
using System.Web.Http;

namespace Dashboard.Models { 
    public class JsonModuleType
    {
        public string Name { get; set; }
        public string AppId { get; set; }
        public bool TokenRequired { get; set; }
        public string URL { get; set; }
        public int? Frequency {get;set;}
        public int RefModuleTypeId { get; set; }
        public DateTime? CreateDate { get; set; }
    }

}
