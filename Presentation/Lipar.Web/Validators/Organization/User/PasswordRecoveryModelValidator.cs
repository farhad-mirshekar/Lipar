using FluentValidation;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Organization;

namespace Lipar.Web.Validators.Organization
{
    public class PasswordRecoveryModelValidator : BaseLiparValidator<PasswordRecoveryModel>
    {
        public PasswordRecoveryModelValidator()
        {
            RuleFor(p => p.Email)
                .NotNull().WithMessage("*")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("پست الکترونیک را در قالب صحیح وارد نمایید");
        }
    }
}
