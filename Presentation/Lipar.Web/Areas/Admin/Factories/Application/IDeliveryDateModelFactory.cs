using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
   public interface IDeliveryDateModelFactory
    {
        /// <summary>
        /// prepare delivery date model for edit or add
        /// </summary>
        /// <param name="model"></param>
        /// <param name="deliveryDate"></param>
        /// <returns></returns>
        DeliveryDateModel PrepareDeliveryDateModel(DeliveryDateModel model, DeliveryDate deliveryDate);
        /// <summary>
        /// prepare delivery date list
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        DeliveryDateListModel PrepareDeliveryDateListModel(DeliveryDateSearchModel searchModel);
    }
}
