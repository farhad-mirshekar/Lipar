using FluentValidation;
using Lipar.Entities.Domain.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General.Faq
{
    public class FaqGroupValidator : BaseLiparValidator<FaqGroup>
    {
        public FaqGroupValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");
        }
    }
}
