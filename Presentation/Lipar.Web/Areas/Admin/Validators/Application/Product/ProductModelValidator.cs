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
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Name.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Name.Validator.Required"));

            RuleFor(p => p.ShortDescription)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.ShortDescription.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.ShortDescription.Validator.Required"));

            RuleFor(p => p.FullDescription)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.FullDescription.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.FullDescription.Validator.Required"));

            RuleFor(p => p.Price)
                .Must(price=>price > 0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Price.Validator.NotEqual"))
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Price.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Price.Validator.Required"))
                .GreaterThanOrEqualTo(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Price.Validator.NotEqual"));

            RuleFor(p => p.Weight)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Weight.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Weight.Validator.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Weight.Validator.NotEqual"));

            RuleFor(p => p.Length)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Length.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Length.Validator.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Length.Validator.NotEqual"));

            RuleFor(p => p.Height)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Height.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Height.Validator.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Height.Validator.NotEqual"));

            RuleFor(p => p.Width)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.Width.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.Width.Validator.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.Width.Validator.NotEqual"));

            RuleFor(p => p.StockQuantity)
                .NotEmpty().WithMessage(_localeStringResourceService.GetResource("Product.Field.StockQuantity.Validator.Required"))
                .NotNull().WithMessage(_localeStringResourceService.GetResource("Product.Field.StockQuantity.Validator.Required"))
                .NotEqual(0).WithMessage(_localeStringResourceService.GetResource("Product.Field.StockQuantity.Validator.NotEqual"));
        }
    }
}
