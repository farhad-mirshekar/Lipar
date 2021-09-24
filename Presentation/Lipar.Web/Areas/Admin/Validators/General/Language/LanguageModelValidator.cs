using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General.Language
{
    public class LanguageModelValidator : BaseLiparValidator<LanguageModel>
    {
        public LanguageModelValidator()
        {
            RuleFor(l => l.Name)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(l => l.LanguageCultureId)
                .NotEqual(0).WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(l => l.ViewStatusId)
                .NotEqual(0).WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
