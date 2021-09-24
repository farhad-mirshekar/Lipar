using FluentValidation;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Validators;

namespace Lipar.Web.Areas.Admin.Validators.Application
{
    public class ProductModelValidator : BaseLiparValidator<ProductModel>
    {
        public ProductModelValidator(ILocaleStringResourceService _localeStringResourceService)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Name.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Name.Required"));

            RuleFor(p => p.ShortDescription)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.ShortDescription.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.ShortDescription.Required"));

            RuleFor(p => p.FullDescription)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.FullDescription.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.FullDescription.Required"));

            RuleFor(p => p.Price)
                .Must(price=>price > 0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Price.NotEqual"))
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Price.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Price.Required"))
                .GreaterThanOrEqualTo(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Price.NotEqual"));

            RuleFor(p => p.Weight)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Weight.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Weight.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Weight.NotEqual"));

            RuleFor(p => p.Length)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Length.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Length.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Length.NotEqual"));

            RuleFor(p => p.Height)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Height.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Height.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Height.NotEqual"));

            RuleFor(p => p.Width)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Width.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Width.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.Width.NotEqual"));

            RuleFor(p => p.StockQuantity)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.StockQuantity.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.StockQuantity.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Admin.Product.Field.StockQuantity.NotEqual"));
        }
    }
}
