using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.DynamicPage
{
    public class DynamicPageDetailModelValidator : BaseLiparValidator<DynamicPageDetailModel>
    {
        public DynamicPageDetailModelValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Title)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Description)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Body)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
