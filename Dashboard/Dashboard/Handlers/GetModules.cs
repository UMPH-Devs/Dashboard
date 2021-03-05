using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;
using Dashboard.Models;
using Dashboard.Extensions;

namespace Dashboard.Handlers
{
    public class GetModules : IRequest<List<JsonModuleStatus>>
    {
        
        public List<JsonModuleStatus> Handle()
        {
            var db = new DashboardEntities();
            DateTime thirtydaysago = DateTime.Now.AddDays(-30);
            JsonModuleStatus getStatusFromId(int id) => db.ModuleStatus
                                                            .Where(x => x.RefModuleTypeId == id).Where(x=> x.CreateDate > thirtydaysago)
                                                            .OrderByDescending(x => x.CreateDate)
                                                            .FirstOrDefault()?
                                                            .ToJsonTypeFaster();

            return db.RefModuleTypes
                                .Select(x => x.RefModuleTypeId)
                                .ToList()
                                .Select(getStatusFromId)
                                .Where(x => x != null)
                                .ToList();
        }
    }
}