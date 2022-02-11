using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General.Menu
{
    public class MenuItemModelValidator : BaseLiparValidator<MenuItemModel>
    {
        public MenuItemModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(m => m.MenuId)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(m => m.ViewStatusId)
            .NotEqual(0).WithMessage("*")
            .NotEmpty().WithMessage("*");

            RuleFor(m => m.Priority)
                .NotEmpty()
                .NotEqual(0)
                .WithMessage("*");
        }
    }
}
