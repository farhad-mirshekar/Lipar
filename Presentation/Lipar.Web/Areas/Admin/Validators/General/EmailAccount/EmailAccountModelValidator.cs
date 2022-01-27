using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General
{
    public class EmailAccountModelValidator : BaseLiparValidator<EmailAccountModel>
    {
        public EmailAccountModelValidator()
        {
            RuleFor(e => e.Email)
                .NotNull().WithMessage("*");

            RuleFor(e => e.Name)
                .NotNull().WithMessage("*");

            RuleFor(e => e.Password)
                .NotNull().WithMessage("*");

            RuleFor(e => e.Username)
                .NotNull().WithMessage("*");

            RuleFor(e => e.Host)
                .NotNull().WithMessage("*");

            RuleFor(e => e.Port)
                .NotNull().WithMessage("*");
        }
    }
}
