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
    public class GetModuleHistory : IRequest<List<JsonModuleStatus>>
    {
        private int _id;

        public GetModuleHistory(int id)
        {
            _id = id;
        }

        public List<JsonModuleStatus> Handle()
        {
            var db = new DashboardEntities();
            return db.ModuleStatus
                .Where(x => x.RefModuleTypeId == _id)
                .OrderByDescending(x => x.CreateDate)
                .Take(20)
                .ToList()
                .Select(x => x.ToJsonType())
                .ToList();
        }
    }
}