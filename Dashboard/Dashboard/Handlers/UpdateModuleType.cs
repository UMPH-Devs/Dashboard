using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;
using Dashboard.Extensions;
using Dashboard.Models;
using Dashboard.Validators;

namespace Dashboard.Handlers
{
    public class UpdateModuleType : IRequest<Response<JsonModuleType>>
    {
        private RefModuleType _rm;
        private Response<JsonModuleType> res = new Response<JsonModuleType>();
        private ModuleTypeValidator val = new ModuleTypeValidator();

        public UpdateModuleType(RefModuleType rm)
        {
            _rm = rm;
        }

        public Response<JsonModuleType> Handle()
        {
            var valid = val.Validate(_rm);
            if (!valid.IsValid)
            {
                res.Error = "model invalid";
                return res;
            }

            var db = new DashboardEntities();
            var moduleType = db.RefModuleTypes.Single(x => x.RefModuleTypeId == _rm.RefModuleTypeId);
            moduleType.Name = _rm.Name;
            moduleType.URL = _rm.URL;
            moduleType.Frequency = _rm.Frequency;
            db.SaveChanges();
            res.OK = moduleType.ToJsonType();
            return res;
        }
    }
}