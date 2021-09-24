using FluentValidation;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Portal;

namespace Lipar.Web.Validators.Portal.Blog
{
    public class BlogCommentModelValidator : BaseLiparValidator<BlogCommentModel>
    {
        public BlogCommentModelValidator()
        {
            RuleFor(bc => bc.Body)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");
        }
    }
}
