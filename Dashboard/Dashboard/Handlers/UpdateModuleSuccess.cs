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
    public class UpdateModuleSuccess : IRequest<JsonModuleStatus>
    {
        private ModuleStatu _mod;
        private DashboardEntities _db;
        public UpdateModuleSuccess(JsonModuleStatus mod)
        {
            _db = new DashboardEntities();
            _mod = _db.ModuleStatus.Single(x => x.ModuleStatusId == mod.StatusId);
        }
        public JsonModuleStatus Handle()
        {
            _mod.IsForcedSuccess = true;
            _db.SaveChanges();
            return _mod.ToJsonType();
        }
    }
}