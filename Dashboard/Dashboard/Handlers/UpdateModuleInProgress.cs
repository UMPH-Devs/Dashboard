using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using UCM;
using Dashboard.Entities;
using Dashboard.Extensions;
using Dashboard.Models;

namespace Dashboard.Handlers
{
    public class UpdateModuleInProgress : IRequest<JsonModuleStatus>
    {
        DashboardEntities _db;
        JsonModuleStatus _mod;

        public UpdateModuleInProgress(JsonModuleStatus mod)
        {
            _mod = mod;
            _db = new DashboardEntities();
        }

        public JsonModuleStatus Handle()
        {
            var m = _db.ModuleStatus.Single(x => x.ModuleStatusId == _mod.StatusId);
            m.IsInProgress = true;
            m.InProgressUserid = _mod.InProgressUserid;
            m.LogText = _mod.LogText ?? m.LogText;
            m.CreateDate = _mod.CreateDate ?? m.CreateDate;
            m.IsLogHtml = _mod.IsLogHtml;
            m.CustomHtml = _mod.CustomHtml ?? m.CustomHtml;
            

            if(_mod.StatusItems != null)
            {
                var jsonStatusItemIds = _mod.StatusItems.Select(si => si.StatusItemId).ToList();
                var statusItems = _db.StatusItems.Where(si => jsonStatusItemIds.Contains(si.StatusItemId)).ToList();
                foreach (var item in _mod.StatusItems)
                {
                    var dbstatusitem = statusItems.Single(x => x.StatusItemId == item.StatusItemId);
                    dbstatusitem.Name = item.Name;
                    dbstatusitem.Value = item.Value;
                    dbstatusitem.ItemStatus = item.ItemStatus;
                    //_db.SaveChanges();
                }
            }
            

            _db.SaveChanges();
            return m.ToJsonType();
        }
    }
}