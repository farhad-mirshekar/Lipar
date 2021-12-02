using FluentValidation;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Dashboard.Models.Organization;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Dashboard.Validators.Organization
{
    public class UserAddressValidatorModel : BaseLiparValidator<UserAddressModel>
    {
        public UserAddressValidatorModel(ILocaleStringResourceService _localeStringResourceService)
        {
            RuleFor(u => u.PostalCode)
                .NotNull().WithMessage(_localeStringResourceService.GetResource("UserAddress.Field.PostalCode.Validator.NotNull"))
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("UserAddress.Field.PostalCode.Validator.NotNull"));

            RuleFor(u => u.Address)
                .NotNull().WithMessage(_localeStringResourceService.GetResource("UserAddress.Field.Address.Validator.NotNull"))
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("UserAddress.Field.Address.Validator.NotNull"));
        }
    }
}
