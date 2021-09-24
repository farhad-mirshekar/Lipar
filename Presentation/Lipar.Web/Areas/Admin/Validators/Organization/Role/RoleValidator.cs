using FluentValidation;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Organization.Role
{
    public class RoleValidator:BaseLiparValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("*");
        }
    }
}
