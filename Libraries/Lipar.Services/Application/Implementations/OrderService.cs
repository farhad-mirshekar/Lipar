using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.ViewModels.Application;
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
            if (model is null)
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

        public OrderViewModel GetOrderViewModel(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new Exception("order id not found");
            }

            var query = _repository.TableNoTracking;

            var orderViewModel = query.Where(o => o.Id == orderId).Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                UserId = o.UserId,
                UserAddressId = o.UserAddressId,
                UserFullName = $"{o.User.FirstName} {o.User.LastName}",
                BankName = o.BankPort.Bank.Name,
                BankPortId = o.BankPortId,
                Price = o.Price,
                ShoppingCartRate = o.ShoppingCartRate,
                CreationDate = o.CreationDate,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.ProductName,
                    Quantity = od.Quantity,
                    DeliveryDateName = od.DeliveryDateName,
                    ProductDiscountPrice = od.ProductDiscountPrice,
                    ProductPrice = od.ProductPrice,
                    ShippingCostName = od.ShippingCostName,
                    ProductDiscountTypeId = od.ProductDiscountTypeId,
                    OrderDetailAttributes = od.OrderDetailAttributes.Select(a => new OrderDetailAttributeViewModel
                    {
                        Price = a.Price,
                        Name = a.Name,
                        AttributeName = a.AttributeName,

                    })
                })
            }).FirstOrDefault();

            if (orderViewModel is null)
            {
                throw new Exception(nameof(orderViewModel));
            }

            return orderViewModel;
        }
        #endregion
    }
}
