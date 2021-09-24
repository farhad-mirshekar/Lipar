using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class ProductAttributeModelValidator : BaseLiparValidator<ProductAttributeModel>
    {
        public ProductAttributeModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");
        }
    }
}
