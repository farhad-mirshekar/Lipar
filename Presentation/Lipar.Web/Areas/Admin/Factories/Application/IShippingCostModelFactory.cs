using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
   public interface IShippingCostModelFactory
    {
        /// <summary>
        /// prepare shipping cost model for edit or add
        /// </summary>
        /// <param name="model"></param>
        /// <param name="shippingCost"></param>
        /// <returns></returns>
        ShippingCostModel PrepareShippingCostModel(ShippingCostModel model, ShippingCost shippingCost);
        /// <summary>
        /// prepare shipping cost list
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        ShippingCostListModel PrepareShippingCostListModel(ShippingCostSearchModel searchModel);
    }
}
