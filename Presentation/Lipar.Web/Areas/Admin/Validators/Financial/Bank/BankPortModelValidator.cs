using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Financial;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Financial
{
    public class BankPortModelValidator : BaseLiparValidator<BankPortModel>
    {
        public BankPortModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(x => x.MerchantId)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(x => x.Username)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(x => x.MerchantKey)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");

            RuleFor(x => x.TerminalId)
                .NotNull().WithMessage("*")
                .NotEmpty().WithMessage("*");
        }
    }
}
