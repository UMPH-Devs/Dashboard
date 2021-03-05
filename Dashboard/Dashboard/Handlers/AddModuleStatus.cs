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
    public class AddModuleStatus : IRequest<Response<JsonModuleStatus>>
    {
        private ModuleStatu _module;
        private Response<JsonModuleStatus> res = new Response<JsonModuleStatus>();
        private ModuleStatusValidator val = new ModuleStatusValidator();

        public AddModuleStatus(ModuleStatu module)
        {
            _module = module;
        }

        public Response<JsonModuleStatus> Handle()
        {
            var valid = val.Validate(_module);
            if(!valid.IsValid)
            {
                res.Error = valid.Errors[0].ErrorMessage;
                return res;
            }

            var db = new DashboardEntities();

            try
            {
                var moduleType = db.RefModuleTypes.Single(x => x.AppId == _module.AppId);
                _module.RefModuleTypeId = moduleType.RefModuleTypeId;
                var previousStatus = db.ModuleStatus
                    .OrderByDescending(x => x.CreateDate)
                    .Where(x => x.RefModuleTypeId == moduleType.RefModuleTypeId)
                    .FirstOrDefault();

                if(previousStatus != null)
                {
                    
                    _module.StatusItems
                        .ToList()
                        .ForEach
                            (x => x.InProgress = x.ItemStatus == "warning" || x.ItemStatus == "error" 
                            ? previousStatus.StatusItems.FirstOrDefault(y => y.AppId == x.AppId)?.InProgress ?? false 
                            : false)
                            ;
                    if (_module.Status == "warning" || _module.Status == "error")
                    {
                        _module.IsInProgress = previousStatus.IsInProgress;
                    }
                }
                
                db.ModuleStatus.Add(_module);
                db.SaveChanges();
                res.OK = _module.ToJsonType();
                return res;
            }
            catch (Exception e)
            {
                res.Error = e.ToString();
                return res;
            }
        }
    }
}