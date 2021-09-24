using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.DynamicPage
{
    public class DynamicPageModelValidator : BaseLiparValidator<DynamicPageModel>
    {
        public DynamicPageModelValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(d => d.Title)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");
        }
    }
}
