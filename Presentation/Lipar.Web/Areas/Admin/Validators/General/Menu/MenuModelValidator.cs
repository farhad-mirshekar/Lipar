using FluentValidation;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.General.Menu
{
    public class MenuModelValidator : BaseLiparValidator<MenuModel>
    {
        public MenuModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("*");

            RuleFor(m => m.LanguageId)
                .NotEmpty()
                .NotEqual(0)
                .WithMessage("*");
        }
    }
}
