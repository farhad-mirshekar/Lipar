using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Organization.Position
{
    public class PositionModelValidator : BaseLiparValidator<PositionModel>
    {
        public PositionModelValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");

            RuleFor(p => p.NationalCode)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .EmailAddress()
                .WithMessage("*");

            RuleFor(p => p.CellPhone)
                .NotEmpty()
                .WithMessage("*")
                .NotNull()
                .WithMessage("*");

            RuleFor(p => p.DepartmentId)
                .NotEmpty()
                .WithMessage("*")
                .NotEqual(0)
                .WithMessage("**");
        }
    }
}
