using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IProductModelFactory
    {
        /// <summary>
        /// prepare product list model
        /// </summary>
        /// <param name="searchModel">search model</param>
        /// <returns></returns>
        ProductListModel PrepareProductListModel(ProductSearchModel searchModel);
        /// <summary>
        /// prepare product model
        /// </summary>
        /// <param name="model">product model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        ProductModel PrepareProductModel(ProductModel model, Product product);
        /// <summary>
        /// prepare product attribute mapping list model 
        /// </summary>
        /// <param name="searchModel">search model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        ProductAttributeMappingListModel PrepareProductAttributeMappingListModel(ProductAttributeMappingSearchModel searchModel, Product product);
        /// <summary>
        /// prepare product attribute model
        /// </summary>
        /// <param name="model">product attribute</param>
        /// <param name="product">product</param>
        /// <param name="productAttributeMapping">product attribute mapping</param>
        /// <returns></returns>
        ProductAttributeMappingModel PrepareProductAttributeMappingModel(ProductAttributeMappingModel model, Product product, ProductAttributeMapping productAttributeMapping);
        /// <summary>
        /// prepare product attribute value list
        /// </summary>
        /// <param name="searchModel">search model</param>
        /// <returns></returns>
        ProductAttributeValueListModel PrepareProductAttributeValueListModel(ProductAttributeValueSearchModel searchModel);
        /// <summary>
        /// prepare product attribute value model
        /// </summary>
        /// <param name="model">product attribute value model</param>
        /// <param name="productAttributeMapping">product attribute mapping</param>
        /// <param name="productAttributeValue">product attribute value</param>
        /// <returns></returns>
        ProductAttributeValueModel PrepareProductAttributeValueModel(ProductAttributeValueModel model, ProductAttributeMapping productAttributeMapping, ProductAttributeValue productAttributeValue);
        /// <summary>
        /// prepare product media list model
        /// </summary>
        /// <param name="searchModel">search model</param>
        /// <returns></returns>
        ProductMediaListModel PrepareProductMediaListModel(ProductMediaSearchModel searchModel);
        /// <summary>
        /// prepare related product list model
        /// </summary>
        /// <param name="searchModel">search model</param>
        /// <returns></returns>
        RelatedProductListModel PrepareRelatedProductListModel(RelatedProductSearchModel searchModel);
        /// <summary>
        /// prepare related product model
        /// </summary>
        /// <param name="model">related product model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        RelatedProductModel PrepareRelatedProductModel(RelatedProductModel model, Product product);
        /// <summary>
        /// prepare product search model
        /// </summary>
        /// <param name="searchModel">product search model</param>
        /// <returns></returns>
        ProductSearchModel PrepareProductSearchModel(ProductSearchModel searchModel);
    }
}
