using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Factories.Portal;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Controllers
{
    public class StaticPageController : BasePublishController
    {
        #region Fields
        private readonly IStaticPageModelFactory _staticPageModelFactory;
        private readonly IStaticPageService _staticPageService;
        #endregion

        #region Ctor
        public StaticPageController(IStaticPageModelFactory staticPageModelFactory
                                  , IStaticPageService staticPageService)
        {
            _staticPageModelFactory = staticPageModelFactory;
            _staticPageService = staticPageService;
        }
        #endregion

        #region Methods
        public IActionResult Detail(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return AccessDeniedView();
            }

            var staticPage = _staticPageService.GetById(Id);
            if (staticPage == null)
            {
                return AccessDeniedView();
            }

                if (staticPage.ViewStatusId == (int)ViewStatusEnum.InActive)
            {
                return AccessDeniedView();
            }

            var model = _staticPageModelFactory.PrepareStaticPageModel(staticPage);

            return View(model);
        }
        #endregion
    }
}
