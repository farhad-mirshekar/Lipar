using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ProductAttributeModelFactory : IProductAttributeModelFactory
    {
        #region Ctor
        public ProductAttributeModelFactory(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }
        #endregion

        #region Fields
        private readonly IProductAttributeService _productAttributeService;
        #endregion

        #region Methods

        public ProductAttributeListModel PrepareProductAttributeListModel(ProductAttributeSearchModel searchModel)
        {
            var productAttributes = _productAttributeService.List(new ProductAttributeListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.Page,
            });

            if(productAttributes == null)
            {
                return null;
            }

            var model = new ProductAttributeListModel().PrepareToGrid(searchModel, productAttributes, () =>
            {
                return productAttributes.Select(productAttribute =>
                {
                    var productAttributeModel = productAttribute.ToModel<ProductAttributeModel>();

                    return productAttributeModel;
                });
            });

            return model;
        }

        public ProductAttributeModel PrepareProductAttributeModel(ProductAttributeModel model, ProductAttribute attribute)
        {
            if(attribute != null)
            {
                model = attribute.ToModel<ProductAttributeModel>();
            }

            return model;
        }
        #endregion
    }
}
