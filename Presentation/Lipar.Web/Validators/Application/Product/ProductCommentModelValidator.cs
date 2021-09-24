using FluentValidation;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Application;

namespace Lipar.Web.Validators.Application.Product
{
    public class ProductCommentModelValidator : BaseLiparValidator<ProductCommentModel>
    {
        public ProductCommentModelValidator()
        {
            RuleFor(pc => pc.CommentText)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
