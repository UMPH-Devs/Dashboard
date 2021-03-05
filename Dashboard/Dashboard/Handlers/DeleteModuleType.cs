using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;

namespace Dashboard.Handlers
{
    public class DeleteModuleType : IRequest<bool>
    {
        private int _id;

        public DeleteModuleType(int id)
        {
            _id = id;
        }

        public bool Handle()
        {
            var db = new DashboardEntities();
            var moduleType = db.RefModuleTypes.Single(x => x.RefModuleTypeId == _id);
            var modules = db.ModuleStatus.Where(x => x.RefModuleTypeId == _id);
            var moduleIds = modules.Select(x => x.ModuleStatusId).ToList();
            var StatusItems = db.StatusItems
                .Where(x => moduleIds.Contains(x.ModuleStatusId.Value))
                .ToList();
            db.StatusItems.RemoveRange(StatusItems);
            db.ModuleStatus.RemoveRange(modules);
            db.RefModuleTypes.Remove(moduleType);
            db.SaveChanges();
            return true;
        }
    }
}