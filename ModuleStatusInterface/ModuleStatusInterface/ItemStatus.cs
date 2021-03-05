using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleStatusInterface
{
    public enum ItemStatus
    {
        Success,
        Warning,
        Error,
        Information
    }

    public static class ItemStatusExtensions
    {
        public static string ToFriendlyString(this ItemStatus status)
        {
            switch (status)
            {
                case ItemStatus.Error:
                    return "error";
                case ItemStatus.Success:
                    return "success";
                case ItemStatus.Warning:
                    return "warning";
                case ItemStatus.Information:
                    return "info";
                default:
                    throw  new Exception("ItemStatus Required");
            }
        }
    }
}
