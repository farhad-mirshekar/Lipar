using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Portal.Blog
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

            //RuleFor(bc => bc.ParentId)
            //    .Null();
        }
    }
}
