using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;

namespace Dashboard.Handlers
{
    public class UpdateStatusItemProgress : IRequest<StatusItem>
    {
        private StatusItem _si;
        private DashboardEntities _db = new DashboardEntities();

        public UpdateStatusItemProgress(StatusItem si) {
            _si = si;
        }
        public StatusItem Handle()
        {
            var item = _db.StatusItems.Single(x => x.StatusItemId == _si.StatusItemId);
            item.InProgress = true;
            _db.SaveChanges();
            return item;
        }
    }
}