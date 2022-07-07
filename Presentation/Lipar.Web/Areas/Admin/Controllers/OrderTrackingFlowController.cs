using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Helpers;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class OrderTrackingFlowController : BaseAdminController
    {
        #region Ctor
        public OrderTrackingFlowController(IOrderTrackingFlowModelFactory orderTrackingFlowModelFactory)
        {
            _orderTrackingFlowModelFactory = orderTrackingFlowModelFactory;
        }
        #endregion

        #region Fields
        private readonly IOrderTrackingFlowModelFactory _orderTrackingFlowModelFactory;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManagePaymentManagement)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManagePaymentManagement)]
        public IActionResult List()
        {
            //prepare search model
            var searchModel = _orderTrackingFlowModelFactory.PrepareOrderTrackingFlowSearchModel();
            return View(searchModel);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManagePaymentManagement)]
        public IActionResult List(OrderTrackingFlowSearchModel searchModel)
        {
            var model = _orderTrackingFlowModelFactory.PreapreOrderTrackingFlowListModel(searchModel);

            return Json(model);
        }

        public IActionResult Edit(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var searchModel = new OrderTrackingFlowSearchModel();
            searchModel.OrderTrackingId = id;

            var model = _orderTrackingFlowModelFactory.PrepareOrderTrackingFlowModel(searchModel);

            return View(model);
        }
        #endregion
    }
}
