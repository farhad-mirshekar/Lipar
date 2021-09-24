using FluentValidation;
using FluentValidation.Validators;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.General;

namespace Lipar.Web.Validators.General.ContactUs
{
    public class ContactUsModelValidator : BaseLiparValidator<ContactUsModel>
    {
        public ContactUsModelValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(c => c.Body)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("*");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");
        }
    }
}
