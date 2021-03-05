using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Dashboard.Entities;

namespace Dashboard.Validators
{
    public class ModuleStatusValidator: AbstractValidator<ModuleStatu>
    {
        private DashboardEntities db = new DashboardEntities();
        public ModuleStatusValidator()
        {
            RuleFor(x => x.AppId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .Must(mod => db.RefModuleTypes.Select(x => x.AppId).ToList().Contains(mod));
            RuleFor(x => x.MachineName).NotNull().NotEmpty();
            RuleFor(x => x.MinutesUntilError).NotNull();
            RuleFor(x => x.MinutesUntilWarning).NotNull();
        }
    }
}