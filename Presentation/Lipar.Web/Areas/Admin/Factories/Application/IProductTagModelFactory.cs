using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IProductTagModelFactory
    {
        /// <summary>
        /// prepare product tag list model
        /// </summary>
        /// <param name="searchModel">product tag search model</param>
        /// <returns>ProductTagListModel</returns>
        ProductTagListModel PrepareProductTagListModel(ProductTagSearchModel searchModel);


        /// <summary>
        /// prepare product tag model
        /// </summary>
        /// <param name="model">product tag model</param>
        /// <param name="productTag">product tag</param>
        /// <returns>ProductTagModel</returns>
        ProductTagModel PrepareProductTagModel(ProductTagModel model , ProductTag productTag);
    }
}
