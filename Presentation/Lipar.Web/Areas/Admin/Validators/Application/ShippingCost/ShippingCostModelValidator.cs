using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class ShippingCostModelValidator : BaseLiparValidator<ShippingCostModel>
    {
        public ShippingCostModelValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Price)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*")
                .NotEqual(0).WithMessage("*");
        }
    }
}
