using FluentValidation;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Organization;

namespace Lipar.Web.Validators.Organization
{
    public class RegisterModelValidator : BaseLiparValidator<RegisterModel>
    {
        private readonly ILocaleStringResourceService _localeStringResourceService;
        public RegisterModelValidator(ILocaleStringResourceService localeStringResourceService)
        {
            _localeStringResourceService = localeStringResourceService;

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.FirstName.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.FirstName.Validator.NotNull"));

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.LastName.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.LastName.Validator.NotNull"));

            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.UserName.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.UserName.Validator.NotNull"))
                .Length(8,50).WithMessage(_localeStringResourceService.GetResource("User.Field.UserName.Validator.MustLengthBetween"));

            RuleFor(r => r.NationalCode)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.NationalCode.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.NationalCode.Validator.NotNull"));

            RuleFor(r => r.CellPhone)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.CellPhone.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.CellPhone.Validator.NotNull"));

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.Password.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.Password.Validator.NotNull"));

            RuleFor(r => r.RePassword)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("User.Field.RePassword.Validator.NotNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("User.Field.RePassword.Validator.NotNull"))
                .Equal(s=>s.Password).WithMessage(_localeStringResourceService.GetResource("User.Field.RePassword.Validator.NotEqualByPassword"));

            RuleFor(r => r.GenderId)
                .NotNull().WithMessage("*")
                .NotEqual(0).WithMessage("**");
        }
    }
}
