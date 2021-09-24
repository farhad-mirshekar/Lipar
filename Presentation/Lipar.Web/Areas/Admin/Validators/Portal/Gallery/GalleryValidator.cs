using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.Gallery
{
    public class GalleryValidator : BaseLiparValidator<GalleryModel>
    {
        public GalleryValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("*");

            RuleFor(g => g.ViewStatusId)
                .NotEqual(0)
                .WithMessage("*");

            RuleFor(g => g.Body)
                .NotEmpty()
                .NotNull()
                .WithMessage("*");
        }
    }
}
