using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Organization.Command
{
    public class CommandModelValidator : BaseLiparValidator<CommandModel>
    {
        public CommandModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("*");

            RuleFor(c => c.SystemName)
                .NotEmpty()
                .NotNull()
                .WithMessage("*");

            RuleFor(c => c.CommandTypeId)
                .NotEqual(0).WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
