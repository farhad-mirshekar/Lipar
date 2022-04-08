using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class ProductTagModelValidator: BaseLiparValidator<ProductTagModel>
    {
        public ProductTagModelValidator()
        {
            RuleFor(pt => pt.Name)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
