using FluentValidation;
using Lipar.Web.Areas.Dashboard.Models.Organization;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Dashboard.Validators.Organization
{
    public class ProfileModelValidator : BaseLiparValidator<ProfileModel>
    {
        public ProfileModelValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull().WithMessage("User.Field.FirstName.Validator.NotNull")
                .NotEmpty().WithMessage("User.Field.FirstName.Validator.NotNull");

            RuleFor(p => p.LastName)
                .NotNull().WithMessage("User.Field.LastName.Validator.NotNull")
                .NotEmpty().WithMessage("User.Field.LastName.Validator.NotNull");

            RuleFor(p => p.NationalCode)
                .NotNull().WithMessage("User.Field.NationalCode.Validator.NotNull")
                .NotEmpty().WithMessage("User.Field.NationalCode.Validator.NotNull");

            RuleFor(p => p.CellPhone)
                .NotNull().WithMessage("User.Field.CellPhone.Validator.NotNull")
                .NotEmpty().WithMessage("User.Field.CellPhone.Validator.NotNull");

            RuleFor(p => p.Email)
                .NotNull().WithMessage("User.Field.Email.Validator.NotNull")
                .NotEmpty().WithMessage("User.Field.Email.Validator.NotNull");
        }
    }
}
