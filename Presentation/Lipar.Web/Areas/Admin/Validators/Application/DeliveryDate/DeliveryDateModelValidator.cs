using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class DeliveryDateModelValidator : BaseLiparValidator<DeliveryDateModel>
    {
        public DeliveryDateModelValidator()
        {
            RuleFor(dd => dd.Name)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(dd => dd.Priority)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*")
                .NotEqual(0).WithMessage("*");
        }
    }
}
