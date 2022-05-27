using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Financial.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Infrastructure;
using Lipar.Web.Models.Application;
using Melli.Portal.Model;
using Melli.Portal.Services;
using System;
using System.Linq;

namespace Lipar.Web.Factories.Application
{
    public class RedirectModelFactory : IRedirectModelFactory
    {
        #region Ctor
        public RedirectModelFactory(IOrderPaymentStatusService orderPaymentStatusService
                                  , IWorkContext workContext
                                  , IBankPortService bankPortService
                                  , IShoppingCartItemModelFactory shoppingCartItemModelFactory
                                  , ISettingService settingService
                                  , IOrderService orderService
                                  , IStaticCacheManager cacheManager)
        {
            _orderPaymentStatusService = orderPaymentStatusService;
            _workContext = workContext;
            _bankPortService = bankPortService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
            _settingService = settingService;
            _orderService = orderService;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly IOrderPaymentStatusService _orderPaymentStatusService;
        private readonly IWorkContext _workContext;
        private readonly IBankPortService _bankPortService;
        private readonly IShoppingCartItemModelFactory _shoppingCartItemModelFactory;
        private readonly ISettingService _settingService;
        private readonly IOrderService _orderService;
        private readonly IStaticCacheManager _cacheManager;
        #endregion


        public RedirectModel PrepareMelli(PurchaseResult purchaseResult)
        {
            var redirectModel = new RedirectModel();

            var shoppingCartItemId = _workContext.ShoppingCartItems;
            if (!shoppingCartItemId.HasValue || (shoppingCartItemId.HasValue && shoppingCartItemId.Value == Guid.Empty))
            {
                throw new Exception("oshopping cart not found");
            }

            //get order payment status
            var orderPaymentStatus = _orderPaymentStatusService.GetByShoppingCartItem(shoppingCartItemId.Value, purchaseResult.Token);

            if (orderPaymentStatus is null)
            {
                throw new Exception("order payment status not found");
            }

            //get shopping cart

            var shoppingCartItems = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(shoppingCartItemId.Value);

            //get info bank port
            var bankPort = _bankPortService.GetById(orderPaymentStatus.BankPortId.Value, true);

            //create bank melli service
            var bankMelliService = new BankMelliService(new Melli.Portal.BankPortDetail
            {
                MerchantId = bankPort.MerchantId,
                MerchantKey = bankPort.MerchantKey,
                PaymentUrl = bankPort.Bank.PaymentUri,
                RedirectUrl = bankPort.Bank.RedirectUri,
                TerminalId = bankPort.TerminalId,
                VerifyUrl = bankPort.Bank.VerifyUri,
            });

            //call verify melli
            var verifyResult = bankMelliService.PaymentVerify(orderPaymentStatus.SignData, purchaseResult);

            if (verifyResult is null)
            {
                throw new Exception("order payment status not found");
            }

            switch (verifyResult.ResCode)
            {
                case (int)ResCodeVerify.نتیجه_تراکنش_موفق_است:
                    
                    //create order
                    var order = PrepareOrder(orderPaymentStatus, shoppingCartItems);
                    _orderService.Add(order);

                    //update order payment status
                    orderPaymentStatus.OrderPaymentStatusTypeId = (int)OrderPaymentStatusTypeEnum.Paid;
                    orderPaymentStatus.OrderId = order.Id;
                    orderPaymentStatus.ResponseMessage = verifyResult.Description;
                    orderPaymentStatus.ResponseStatus = verifyResult.ResCode;
                    _orderPaymentStatusService.Edit(orderPaymentStatus);

                    //fill redirect model
                    redirectModel.PaymentStatus = true;
                    redirectModel.OrderId = order.Id;
                    redirectModel.Message = verifyResult.Description;
                    redirectModel.RetrivalRefNo = verifyResult.RetrivalRefNo;
                    redirectModel.ReservationNumber = verifyResult.OrderId.ToString();
                    redirectModel.SystemTraceNo = verifyResult.SystemTraceNo;
                    redirectModel.Amount = verifyResult.Amount;

                    //delete basket
                    _shoppingCartItemModelFactory.DeleteAllShoppingCartItem(shoppingCartItemId.Value);

                    //clear cache for shopping cart
                    ClearCaches();

                    break;
                default:

                    //update order payment status
                    orderPaymentStatus.OrderPaymentStatusTypeId = (int)OrderPaymentStatusTypeEnum.Unpaid;
                    orderPaymentStatus.ResponseMessage = verifyResult.Description;
                    orderPaymentStatus.ResponseStatus = verifyResult.ResCode;
                    _orderPaymentStatusService.Edit(orderPaymentStatus);

                    redirectModel.PaymentStatus = false;
                    redirectModel.Message = verifyResult.Description;

                    break;
            }

            return redirectModel;
        }

        #region Utilities
        private Order PrepareOrder(OrderPaymentStatus orderPaymentStatus, ShoppingCartItemListModel shoppingCartItemListModel)
        {
            var setting = _settingService.GetSetting("ordersetting.shoppingcartrate");
            var shoppingCartRate = setting != null ? setting.Value : "0";

            var order = new Order();
            order.UserAddressId = orderPaymentStatus.UserAddressId.Value;
            order.UserId = orderPaymentStatus.UserId;
            order.BankPortId = orderPaymentStatus.BankPortId.Value;
            order.Price = shoppingCartItemListModel.CartAmount;
            order.CreationDate = CommonHelper.GetCurrentDateTime();
            order.ShoppingCartRate = decimal.Parse(shoppingCartRate);

            if (shoppingCartItemListModel != null && shoppingCartItemListModel.AvailableShoppingCartItemModels.Any())
            {
                foreach (var shoppingCartItemModel in shoppingCartItemListModel.AvailableShoppingCartItemModels)
                {
                    var orderDetail = PrepareOrderDetail(order.Id, shoppingCartItemModel);

                    order.OrderDetails.Add(orderDetail);

                }
            }
            return order;
        }

        private OrderDetail PrepareOrderDetail(Guid orderId, ShoppingCartItemModel shoppingCartItemModel)
        {
            var orderDetail = new OrderDetail();
            orderDetail.OrderId = orderId;
            orderDetail.ProductId = shoppingCartItemModel.ProductId;
            orderDetail.ProductPrice = shoppingCartItemModel.ProductPrice;
            orderDetail.ProductDiscountPrice = shoppingCartItemModel.ProductDiscount;
            orderDetail.ProductName = shoppingCartItemModel.ProductName;
            orderDetail.DeliveryDateId = shoppingCartItemModel.DeliveryDate?.Id;
            orderDetail.DeliveryDateName = shoppingCartItemModel.DeliveryDate?.Name;
            orderDetail.DeliveryDatePriority = shoppingCartItemModel.DeliveryDate?.Priority;
            orderDetail.ProductCategoryId = shoppingCartItemModel.CategoryId;
            orderDetail.ProductCategoryName = shoppingCartItemModel.CategoryName;
            orderDetail.Quantity = shoppingCartItemModel.Quantity;
            orderDetail.ShippingCostId = shoppingCartItemModel.ShippingCost?.Id;
            orderDetail.ShippingCostName = shoppingCartItemModel.ShippingCost?.Name;
            orderDetail.ShippingCostPriority = shoppingCartItemModel.ShippingCost?.Priority;
            orderDetail.ProductDiscountTypeId = shoppingCartItemModel.ProductDiscountTypeId;
            orderDetail.CreationDate = CommonHelper.GetCurrentDateTime();

            if (shoppingCartItemModel.ProductAttributeValues != null && shoppingCartItemModel.ProductAttributeValues.Any())
            {
                foreach (var productAttributeValue in shoppingCartItemModel.ProductAttributeValues)
                {
                    var orderDetailAttribute = PrepareOrderDetailAttribute(orderDetail.Id, productAttributeValue);

                    orderDetail.OrderDetailAttributes.Add(orderDetailAttribute);
                }
            }

            return orderDetail;
        }

        private OrderDetailAttribute PrepareOrderDetailAttribute(Guid orderDetailId, ProductAttributeValue productAttributeValue)
        {
            var orderDetailAttribute = new OrderDetailAttribute();

            orderDetailAttribute.ProductAttributeValueId = productAttributeValue.Id;
            orderDetailAttribute.ProductAttributeMappingId = productAttributeValue.ProductAttributeMappingId;
            orderDetailAttribute.OrderDetailId = orderDetailId;
            orderDetailAttribute.Price = productAttributeValue.Price;
            orderDetailAttribute.Name = productAttributeValue.Name;
            orderDetailAttribute.CreationDate = CommonHelper.GetCurrentDateTime();

            return orderDetailAttribute;
        }

        private void ClearCaches()
        {
            _cacheManager.Remove(new CacheKey(LiparCachingDefaults.LiparShoppingCartItem));
            _cacheManager.Remove(new CacheKey(LiparWebCacheKey.LiparShoppingCartList));
            _cacheManager.Remove(new CacheKey(LiparWebCacheKey.LiparMiniShoppingCartList));
        }
        #endregion
    }
}
