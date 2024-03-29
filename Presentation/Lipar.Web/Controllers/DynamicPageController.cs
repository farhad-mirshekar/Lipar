﻿using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Factories.Portal;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Controllers
{
    public class DynamicPageController : BasePublishController
    {
        #region Fields
        private readonly IDynamicPageModelFactory _dynamicPageModelFactory;
        private readonly IDynamicPageDetailService _dynamicPageDetailService;
        private readonly IDynamicPageService _dynamicPageService;
        #endregion

        #region Ctor
        public DynamicPageController(IDynamicPageModelFactory dynamicPageModelFactory
                                   , IDynamicPageDetailService dynamicPageDetailService
                                   , IDynamicPageService dynamicPageService)
        {
            _dynamicPageModelFactory = dynamicPageModelFactory;
            _dynamicPageDetailService = dynamicPageDetailService;
            _dynamicPageService = dynamicPageService;
        }
        #endregion

        #region Methods
        public IActionResult Detail(Guid DynamicPageId)
        {
            if (DynamicPageId == Guid.Empty)
                return InvokeHttp404();

            var dynamicPage = _dynamicPageService.GetById(DynamicPageId);
            if(dynamicPage == null)
            {
                return InvokeHttp404();
            }

            if(dynamicPage.RemoverId.HasValue)
            {
                return InvokeHttp404();
            }

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailListModel(dynamicPage);
            return View(model);
        }

        public IActionResult DetailPage(Guid DynamicPageDetailId)
        {
            if (DynamicPageDetailId == Guid.Empty)
                return InvokeHttp404();

            var dynamicPageDetail = _dynamicPageDetailService.GetById(DynamicPageDetailId);
            if (dynamicPageDetail == null)
                return InvokeHttp404();

            if(dynamicPageDetail.ViewStatusId == (int)ViewStatusEnum.InActive)
            {
                return InvokeHttp404();
            }

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailModel(dynamicPageDetail);
            return View(model);
        }
        #endregion
    }
}
