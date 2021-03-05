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
    public class GetModule : IRequest<JsonModuleStatus>
    {
        private int _id;

        public GetModule(int id)
        {
            _id = id;
        }
        public JsonModuleStatus Handle()
        {
            var db = new DashboardEntities();
            return db.ModuleStatus.Where(x => x.RefModuleTypeId == _id).OrderByDescending(x => x.CreateDate).First().ToJsonType();
        }
    }
}