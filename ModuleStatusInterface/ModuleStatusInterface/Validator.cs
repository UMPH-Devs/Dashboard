using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ModuleStatusInterface
{
    class Validator
    {
        public class ModuleStatusValidator : AbstractValidator<ModuleStatus>
        {
            public ModuleStatusValidator()
            {
                RuleFor(x => x.AppId).NotNull().NotEmpty().MaximumLength(30);
                RuleFor(x => x.MinutesUntilError).NotNull().GreaterThan(0);
                RuleFor(x => x.MinutesUntilWarning).NotNull().GreaterThan(0);
                RuleFor(x => x.StatusLine).MaximumLength(140);
            }
        }

        public class StatusItemValidator : AbstractValidator<StatusItem>
        {
            public StatusItemValidator()
            {
                RuleFor(x => x.AppId).NotEmpty().MaximumLength(30);
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Value).NotEmpty();
                RuleFor(x => x.Status).NotNull();
            }
        }
    }
}
