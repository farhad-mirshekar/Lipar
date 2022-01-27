using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General
{
    public class MessageTemplateModelValidator : BaseLiparValidator<MessageTemplateModel>
    {
        public MessageTemplateModelValidator()
        {
            RuleFor(m => m.Name)
                .NotNull().WithMessage("*");

            RuleFor(m => m.Subject)
                .NotNull().WithMessage("*");

            RuleFor(m => m.Template)
                .NotNull().WithMessage("*");
        }
    }
}
