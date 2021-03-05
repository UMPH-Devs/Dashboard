using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;
using Dashboard.Models;

namespace Dashboard.Handlers
{
    public class GetOverallStatus: IRequest<string>
    {
        private DashboardEntities db = new DashboardEntities();

        public string Handle()
        {
            double MinutesSince(DateTime x) => DateTime.Now.Subtract(x).TotalMinutes;
            bool inError(JsonModuleStatus x) => MinutesSince(x.CreateDate.Value) > x.MinutesUntilError;
            bool inWarning(JsonModuleStatus x) => MinutesSince(x.CreateDate.Value) > x.MinutesUntilWarning;

            var modules = RequestHandler.Send(new GetModules());
            var overallStatus = modules.Any(x => !x.IsForcedSuccess && !x.IsInProgress && !x.StatusItems.Any(y => y.InProgress) && (x.Status == "error" || inError(x))) ? "error":
                                modules.Any(x => !x.IsForcedSuccess && !x.IsInProgress && !x.StatusItems.Any(y => y.InProgress) && (x.Status == "warning" || inWarning(x))) ? "warning": 
                                "success";

            return overallStatus;
        }
    }
}