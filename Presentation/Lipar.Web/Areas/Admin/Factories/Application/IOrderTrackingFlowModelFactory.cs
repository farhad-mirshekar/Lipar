using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;
using System;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IOrderTrackingFlowModelFactory
    {
        /// <summary>
        /// prepare order tracking flow list model
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        OrderTrackingFlowListModel PreapreOrderTrackingFlowListModel(OrderTrackingFlowSearchModel searchModel);


        /// <summary>
        /// prepare order tracking flow search model
        /// </summary>
        /// <returns></returns>
        OrderTrackingFlowSearchModel PrepareOrderTrackingFlowSearchModel();

        /// <summary>
        /// prepare order tracking flow model
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        OrderTrackingFlowModel PrepareOrderTrackingFlowModel(OrderTrackingFlowSearchModel searchModel);
    }
}
