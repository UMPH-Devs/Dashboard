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
    public class GetModuleStatus: IRequest<JsonModuleStatus>
    {
        private int _id;

        public GetModuleStatus(int id)
        {
            _id = id;
        }

        public JsonModuleStatus Handle()
        {
            var db = new DashboardEntities();
            return db.ModuleStatus.Where(x => x.ModuleStatusId == _id).Single().ToJsonType();
        }
    }
}