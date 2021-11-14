using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;

namespace Lipar.Services.Application.Implementations
{
    public class OrderService : IOrderService
    {
        #region Ctor
        public OrderService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        #endregion

        #region Fields
        private readonly ISettingService _settingService;
        #endregion

        #region Methods
        public OrderSetting OrderSettings()
        {
            var orderSetting = _settingService.LoadSettings<OrderSetting>();
            return orderSetting;
        }
        #endregion
    }
}
