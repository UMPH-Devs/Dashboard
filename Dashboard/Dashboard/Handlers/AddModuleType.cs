using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Entities;
using UCM;
using Dashboard.Validators;

namespace Dashboard.Handlers
{
    public class AddModuleType : IRequest<Response<RefModuleType>>
    {
        private RefModuleType _rm;
        private ModuleTypeValidator validator = new ModuleTypeValidator();
        private Response<RefModuleType> response = new Response<RefModuleType>();
        public AddModuleType(RefModuleType rm)
        {
            _rm = rm;
        }

        public Response<RefModuleType> Handle()
        {
            var valid = validator.Validate(_rm);
            if (!valid.IsValid)
            {
                response.Error = valid.Errors[0].ErrorMessage;
                return response;
            }
            var db = new DashboardEntities();
            _rm.CreateDate = DateTime.Now;
            db.RefModuleTypes.Add(_rm);
            db.SaveChanges();
            response.OK = _rm;
            return response;
        }
    }
}