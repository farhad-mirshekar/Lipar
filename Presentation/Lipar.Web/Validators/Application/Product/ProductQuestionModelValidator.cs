using FluentValidation;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Application;

namespace Lipar.Web.Validators.Application.Product
{
    public class ProductQuestionModelValidator : BaseLiparValidator<ProductQuestionModel>
    {
        public ProductQuestionModelValidator()
        {
            RuleFor(p => p.QuestionText)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(p => p.ProductId)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
