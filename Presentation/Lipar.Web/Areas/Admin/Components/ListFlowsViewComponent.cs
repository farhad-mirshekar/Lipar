using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Models.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Components
{
    public class ListFlowsViewComponent : ViewComponent
    {
        #region Ctor
        public ListFlowsViewComponent(IOrderTrackingFlowService orderTrackingFlowService)
        {
            _orderTrackingFlowService = orderTrackingFlowService;
        }
        #endregion

        #region Fields
        private readonly IOrderTrackingFlowService _orderTrackingFlowService;
        #endregion

        public IViewComponentResult Invoke(Guid orderTrackingId)
        {
            var listFlows = _orderTrackingFlowService.GetOrderTrackingFlows(new OrderTrackingFlowListVM
            {
                OrderTrackingId = orderTrackingId
            }).Select(o=>new OrderTrackingFlowModel
            {
                OrderTrackingId = o.OrderTrackingId,
                ActionDate = o.ActionDate,
                Description = o.Description,
                FromDocStateId = o.FromDocStateId,
                FromDocStateTitle = o.FromDocStateTitle,
                FromPositionFullName = o.FromPositionFullName,
                FromPositionId = o.FromPositionId,
                ToDocStateId = o.ToDocStateId,
                ToDocStateTitle = o.ToDocStateTitle,
                ToPositionFullName = o.ToPositionFullName,
                ToPositionId = o.ToPositionId
            }).ToList();

            return View(listFlows);
        }
    }
}
