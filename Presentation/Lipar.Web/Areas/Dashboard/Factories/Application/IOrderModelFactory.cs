using Lipar.Web.Areas.Dashboard.Models.Application;

namespace Lipar.Web.Areas.Dashboard.Factories.Application
{
    /// <summary>
    /// order model factory
    /// </summary>
    public interface IOrderModelFactory
    {
        /// <summary>
        /// prepare order list model
        /// </summary>
        /// <returns></returns>
        OrderListModel PrepareOrderListModel(OrderSearchModel searchModel);

        /// <summary>
        /// prepare invoice detail
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        OrderModel PrepareInvoiceDetail(OrderSearchModel searchModel);
    }
}
