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
    public class DeleteModuleStatus : IRequest<bool>
    {
        private int _modid;
        private int _statusid;

        public DeleteModuleStatus(int statusid)
        {
            _statusid = statusid;
        }

        public bool Handle()
        {
            var db = new DashboardEntities();

            //get ModuleStatus to be deleted later
            var ModuleStatus = db.ModuleStatus.Single(x => x.ModuleStatusId == _statusid);
            //get all the StatusItems for our Module Status
            var StatusItems = db.StatusItems
                .Where(x => x.ModuleStatusId == _statusid)
                .ToList();

            //delete the StatusItems
            db.StatusItems.RemoveRange(StatusItems);
            //delete the module status
            db.ModuleStatus.Remove(ModuleStatus);
            db.SaveChanges();
            return true;
        }
    }
}