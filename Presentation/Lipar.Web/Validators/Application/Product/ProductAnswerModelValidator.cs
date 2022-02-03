using FluentValidation;
using Lipar.Web.Framework.Validators;
using Lipar.Web.Models.Application;

namespace Lipar.Web.Validators.Application.Product
{
    public class ProductAnswerModelValidator : BaseLiparValidator<ProductAnswersModel>
    {
        public ProductAnswerModelValidator()
        {
            RuleFor(x => x.AnswerText)
                .NotNull().WithMessage("*");
        }
    }
}
