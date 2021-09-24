using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class DeliveryDateModelFactory : IDeliveryDateModelFactory
    {
        #region Ctor
        public DeliveryDateModelFactory(IDeliveryDateService deliveryDateService
                                      , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _deliveryDateService = deliveryDateService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Fields
        private readonly IDeliveryDateService _deliveryDateService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Methods
        public DeliveryDateListModel PrepareDeliveryDateListModel(DeliveryDateSearchModel searchModel)
        {
            var deliveryDates = _deliveryDateService.List(new DeliveryDateListVM
                {
                    Name = searchModel.Name,
                     PageIndex = searchModel.Page - 1,
                     PageSize = searchModel.PageSize,
                });

            var model = new DeliveryDateListModel().PrepareToGrid(searchModel, deliveryDates, () =>
            {
                return deliveryDates.Select(deliveryDate =>
                {
                    var deliveryDateModel = deliveryDate.ToModel<DeliveryDateModel>();

                    return deliveryDateModel;
                });
            });

            return model;
        }

        public DeliveryDateModel PrepareDeliveryDateModel(DeliveryDateModel model, DeliveryDate deliveryDate)
        {
            if(deliveryDate != null)
            {
                model = deliveryDate.ToModel<DeliveryDateModel>();
            }

            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnabledType);

            return model;
        }
        #endregion
    }
}
