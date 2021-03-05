using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleStatusInterface
{
     public class StatusItem
    {
        public string Name { get; set; }
        public string AppId { get; set; }
        public string ItemStatus => Status.ToFriendlyString();
        public ItemStatus Status { get; set; }
        public string Value { get; set; }
        public bool Display { get; set; } = false;
    }
}