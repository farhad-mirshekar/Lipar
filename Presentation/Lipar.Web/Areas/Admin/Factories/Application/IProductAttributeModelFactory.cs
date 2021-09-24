using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
   public interface IProductAttributeModelFactory
    {
        /// <summary>
        /// prepare product attribute model
        /// </summary>
        /// <param name="model">product attribute model</param>
        /// <param name="attribute">product attribute</param>
        /// <returns></returns>
        ProductAttributeModel PrepareProductAttributeModel(ProductAttributeModel model, ProductAttribute attribute);
        /// <summary>
        /// prepare product attribute list
        /// </summary>
        /// <param name="searchModel">product attribute search model</param>
        /// <returns></returns>
        ProductAttributeListModel PrepareProductAttributeListModel(ProductAttributeSearchModel searchModel);
    }
}
