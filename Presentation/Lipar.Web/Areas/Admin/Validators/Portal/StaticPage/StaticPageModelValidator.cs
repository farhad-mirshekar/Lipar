using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.StaticPage
{
    public class StaticPageModelValidator : BaseLiparValidator<StaticPageModel>
    {
        public StaticPageModelValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Title)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(s => s.Body)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
