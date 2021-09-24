using FluentValidation;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.Blog
{
    public class BlogModelValidator:BaseLiparValidator<BlogModel>
    {
        public BlogModelValidator(ILocaleStringResourceService _localeStringResourceService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(x => x.Body)
               .NotEmpty()
               .WithMessage("*");

            RuleFor(x => x.MetaKeywords)
               .NotEmpty()
               .WithMessage("*");
        }
    }
}
