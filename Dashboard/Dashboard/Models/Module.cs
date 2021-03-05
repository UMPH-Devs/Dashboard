using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Dashboard.Models;

namespace Dashboard.Entities
{
    public partial class ModuleStatu
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string AppId { get; set; }
        public string Status => StatusItems.Any(x => x.ItemStatus == "error") ? "error"  : 
                                StatusItems.Any(x => x.ItemStatus == "warning") ? "warning" : 
                                "success";
    }
}