using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
    /// <summary>
    /// order service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// get all order setting
        /// </summary>
        /// <returns></returns>
        OrderSetting OrderSettings();
    }
}
