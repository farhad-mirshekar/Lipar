using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General.Faq
{
    public class FaqValidator : BaseLiparValidator<FaqModel>
    {
        public FaqValidator()
        {
            RuleFor(f => f.Question)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");

            RuleFor(f => f.Answer)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");
        }
    }
}
