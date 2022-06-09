using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class OrderService : IOrderService
    {
        #region Ctor
        public OrderService(ISettingService settingService
                           , IRepository<Order> repository)
        {
            _settingService = settingService;
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly ISettingService _settingService;
        private readonly IRepository<Order> _repository;
        #endregion

        #region Methods
        public OrderSetting OrderSettings()
        {
            var orderSetting = _settingService.LoadSettings<OrderSetting>();
            return orderSetting;
        }

        public void Add(Order model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.CreationDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public IQueryable<Order> GetQuery()
        {
            var query = _repository.TableNoTracking;

            return query;
        }
        #endregion
    }
}
