using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Models;
using Dashboard.Entities;

namespace Dashboard.Extensions
{
    public static class ExtensionMethods
    {
        internal static JsonModuleStatus ToJsonType(this ModuleStatu module)
        {
            return new JsonModuleStatus
            {
                Name = module.RefModuleType?.Name,
                AppId = module.RefModuleType?.AppId,
                CustomHtml = module.CustomHtml,
                MinutesUntilWarning = module.MinutesUntilWarning,
                MinutesUntilError = module.MinutesUntilError,
                StatusLine = module.StatusLine,
                CreateDate = module.CreateDate,
                RefModuleType = module.RefModuleType?.ToJsonType(),
                Id = module.RefModuleTypeId,
                StatusItems = module.StatusItems?.Select(x => x?.ToJsonType())?.ToList(),
                LogText = module.LogText,
                Status = module.Status,
                StatusId = module.ModuleStatusId,
                IsLogHtml = module.IsLogHtml,
                IsForcedSuccess = module.IsForcedSuccess,
                IsInProgress = module.IsInProgress,
                InProgressUserid = module.InProgressUserid
            };
        }
        internal static JsonModuleStatus ToJsonTypeFaster(this ModuleStatu module)
        {
            return new JsonModuleStatus
            {
                Name = module.RefModuleType?.Name,
                AppId = module.RefModuleType?.AppId,
                CustomHtml = module.CustomHtml,
                MinutesUntilWarning = module.MinutesUntilWarning,
                MinutesUntilError = module.MinutesUntilError,
                StatusLine = module.StatusLine,
                CreateDate = module.CreateDate,
                RefModuleType = module.RefModuleType?.ToJsonType(),
                Id = module.RefModuleTypeId,
                StatusItems = module.StatusItems?.Select(x => x?.ToJsonType()).Where(x=> x.Display)?.ToList(),
                //LogText = module.LogText,
                Status = module.Status,
                StatusId = module.ModuleStatusId,
                //IsLogHtml = module.IsLogHtml,
                IsForcedSuccess = module.IsForcedSuccess,
                IsInProgress = module.IsInProgress,
                InProgressUserid = module.InProgressUserid
            };
        }

        internal static JsonStatusItem ToJsonType(this StatusItem item)
        {
            return new JsonStatusItem
            {
                Name = item.Name,
                Value = item.Value,
                ItemStatus = item.ItemStatus,
                Display = item.Display,
                InProgress = item.InProgress,
                StatusItemId = item.StatusItemId,
                InProgressUserid = item.InProgressUserid
            };
        }

        internal static JsonModuleType ToJsonType(this RefModuleType moduleType)
        {
            return new JsonModuleType
            {
                Name = moduleType.Name,
                AppId = moduleType.AppId,
                TokenRequired = moduleType.TokenRequired,
                URL = moduleType.URL,
                Frequency = moduleType.Frequency,
                RefModuleTypeId = moduleType.RefModuleTypeId,
                CreateDate = moduleType.CreateDate
            };
        }
    }
}