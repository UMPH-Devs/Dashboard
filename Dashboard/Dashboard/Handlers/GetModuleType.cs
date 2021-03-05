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
    public class GetModuleType : IRequest<JsonModuleType>
    {
        private int _id;

        public GetModuleType(int id)
        {
            _id = id;
        }
        public JsonModuleType Handle()
        {
            var db = new DashboardEntities();
            return db.RefModuleTypes.Single(x => x.RefModuleTypeId == _id).ToJsonType();
        }
    }
}