using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class ProductAttributeMappingModelValidator : BaseLiparValidator<ProductAttributeMappingModel>
    {
        public ProductAttributeMappingModelValidator()
        {
            RuleFor(p => p.TextPrompt)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");
        }
    }
}
