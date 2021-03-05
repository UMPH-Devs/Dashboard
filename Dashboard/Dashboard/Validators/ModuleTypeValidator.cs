using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Dashboard.Entities;

namespace Dashboard.Validators
{
    public class ModuleTypeValidator: AbstractValidator<RefModuleType>
    {
        public ModuleTypeValidator()
        {
            RuleFor(x => x.AppId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.TokenRequired).NotNull();
            RuleFor(x => x.URL).Must((mod, URL) => String.IsNullOrEmpty(URL) == (mod.Frequency == null));
        }
    }
}