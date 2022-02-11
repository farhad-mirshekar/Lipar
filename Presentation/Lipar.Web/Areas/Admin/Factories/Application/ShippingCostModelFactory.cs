using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ShippingCostModelFactory : IShippingCostModelFactory
    {
        #region Ctor
        public ShippingCostModelFactory(IShippingCostService shippingCostService
                                      , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _shippingCostService = shippingCostService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Fields
        private readonly IShippingCostService _shippingCostService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Fields
        public ShippingCostListModel PrepareShippingCostListModel(ShippingCostSearchModel searchModel)
        {
            var shippingCosts = _shippingCostService.List(new ShippingCostListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if(shippingCosts == null)
            {
                return null;
            }

            var models = new ShippingCostListModel().PrepareToGrid(searchModel, shippingCosts, () =>
            {
                return shippingCosts.Select(shippingCost =>
                {
                    var shippingCostModel = shippingCost.ToModel<ShippingCostModel, Guid>();

                    return shippingCostModel;
                });
            });

            return models;
        }

        public ShippingCostModel PrepareShippingCostModel(ShippingCostModel model, ShippingCost shippingCost)
        {
            if(shippingCost != null)
            {
                model = shippingCost.ToModel<ShippingCostModel, Guid>();
            }

            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnabledType);

            return model;
        }
        #endregion
    }
}
