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
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.FirstNameNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.FirstNameNull"));

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.LastNameNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.LastNameNull"));

            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.UserNameNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.UserNameNull"))
                .Length(8,50).WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.UserName.Length"));

            RuleFor(r => r.NationalCode)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.NationalCodeNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.NationalCodeNull"));

            RuleFor(r => r.CellPhone)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.CellPhoneNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.CellPhoneNull"));

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.PasswordNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.PasswordNull"));

            RuleFor(r => r.RePassword)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.RePasswordNull"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Web.Register.Page.RePasswordNull"));
        }
    }
}
