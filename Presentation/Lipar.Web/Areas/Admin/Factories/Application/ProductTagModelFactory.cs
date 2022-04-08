using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ProductTagModelFactory : IProductTagModelFactory
    {
        #region Ctor
        public ProductTagModelFactory(IProductTagService productTagService)
        {
            _productTagService = productTagService;
        }
        #endregion

        #region Fields
        private readonly IProductTagService _productTagService;
        #endregion

        #region Methods
        public ProductTagListModel PrepareProductTagListModel(ProductTagSearchModel searchModel)
        {
            var productTags = _productTagService.List(new ProductTagListVM
            {
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if(productTags is null)
            {
                return null;
            }

            var models = new ProductTagListModel().PrepareToGrid(searchModel, productTags, () =>
            {
                return productTags.Select(productTag =>
                {
                    var productTagModel = new ProductTagModel();
                    productTagModel.Id = productTag.Id;
                    productTagModel.CreationDate = productTag.CreationDate;
                    productTagModel.Name = productTag.Name;

                    return productTagModel;
                });
            });

            return models;
        }

        public ProductTagModel PrepareProductTagModel(ProductTagModel model, ProductTag productTag)
        {
            if(!(productTag is null))
            {
                model.Id = productTag.Id;
                model.CreationDate = productTag.CreationDate;
                model.Name = productTag.Name;
            }

            return model;
        }

        #endregion
    }
}
