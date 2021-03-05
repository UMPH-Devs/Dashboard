using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;
using Dashboard.Extensions;
using Dashboard.Models;

namespace Dashboard.Handlers
{
    public class GetModuleTypes : IRequest<List<JsonModuleType>>
    {
        public List<JsonModuleType> Handle()
        {
            var db = new DashboardEntities();
            return db.RefModuleTypes.ToList().Select(x => x.ToJsonType()).ToList();
        }
    }
}