using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Financial;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Financial
{
    public class BankModelValidator : BaseLiparValidator<BankModel>
    {
        public BankModelValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");

            RuleFor(b => b.PaymentUri)
                .NotEmpty().WithMessage("*")
                .NotNull().WithMessage("*");
        }
    }
}
