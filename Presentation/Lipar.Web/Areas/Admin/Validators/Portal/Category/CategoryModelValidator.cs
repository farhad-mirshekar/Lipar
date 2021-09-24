using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.Category
{
    public class CategoryModelValidator:BaseLiparValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("*");
        }
    }
}
