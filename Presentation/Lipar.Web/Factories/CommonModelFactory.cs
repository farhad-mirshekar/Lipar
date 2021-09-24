using Lipar.Core;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Models.Common;
using Lipar.Web.Models.Portal;
using Lipar.Web.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Factories
{
    public class CommonModelFactory : ICommonModelFactory
    {
        #region Ctor
        public CommonModelFactory(IWorkContext workContext
                                , IStaticPageService staticPageService
                                , IDynamicPageService dynamicPageService
                                , IContactUsTypeService contactUsTypeService)
        {
            _workContext = workContext;
            _staticPageService = staticPageService;
            _dynamicPageService = dynamicPageService;
            _contactUsTypeService = contactUsTypeService;
        }
        #endregion

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IStaticPageService _staticPageService;
        private readonly IDynamicPageService _dynamicPageService;
        private readonly IContactUsTypeService _contactUsTypeService;
        #endregion

        #region Methods
        public MenuModel PrepareMenuModel()
        {
            var menuModel = new MenuModel();

            var staticPages = _staticPageService.List(new StaticPageListVM
            {
                IncludeInTopMenu = true,
                ViewStatusId = (int)ViewStatusEnum.Active,
            });

            if(staticPages != null)
            {
                var staticPagesList = staticPages.ToList();
                staticPagesList.ForEach(staticPage =>
                {
                    var staticPageModel = new StaticPageModel();
                    staticPageModel.Name = staticPage.Name;
                    staticPageModel.Title = staticPage.Title;
                    staticPageModel.Id = staticPage.Id;

                    menuModel.StaticPages.Add(staticPageModel);
                });

                menuModel.UserHeaderLink = PrepareHeaderLinksModel();

                var dynamicPages = _dynamicPageService.List(new DynamicPageListVM
                {
                    IncludeInTopMenu = true,
                    //ViewStatusType = ViewStatusType.فعال
                });

                var dynamicPagesList = dynamicPages.ToList();
                dynamicPagesList.ForEach(dynamicPage =>
                {
                    var dynamicPageModel = new DynamicPageModel();
                    dynamicPageModel.Name = dynamicPage.Name;
                    dynamicPageModel.Title = dynamicPage.Title;
                    dynamicPageModel.Id = dynamicPage.Id;

                    menuModel.dynamicPages.Add(dynamicPageModel);
                });
            }

           

            return menuModel;
        }
        public void PrepareContacatUsType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var contactUsTypes = _contactUsTypeService.List();

            foreach (var contactUsType in contactUsTypes)
            {
                items.Add(new SelectListItem
                {
                    Text = contactUsType.Title,
                    Value = contactUsType.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        public ContactUsModel PrepareContactUsModel(ContactUsModel model)
        {
            if (_workContext.CurrentUser != null)
            {
                model.FirstName = _workContext.CurrentUser.FirstName;
                model.LastName = _workContext.CurrentUser.LastName;
                model.Phone = _workContext.CurrentUser.CellPhone;
                model.Email = _workContext.CurrentUser.Email;
            }

            PrepareContacatUsType(model.AvailableContactUsType);

            return model;
        }
        #endregion

        #region Utilities
        protected UserHeaderLinkModel PrepareHeaderLinksModel()
        {
            var user = _workContext.CurrentUser;

            var model = new UserHeaderLinkModel();

            if (user != null)
            {
                model.UserInfo = $"{user.FirstName} {user.LastName}";
                model.IsAuthenticated = true;
            }

            return model;
        }
        protected void PrepareDefaultItem(IList<SelectListItem> items, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (string.IsNullOrWhiteSpace(defaultItemText))
            {
                defaultItemText = "انتخاب نمایید";
            }

            const string value = "0";

            items.Insert(0, new SelectListItem { Value = value, Text = defaultItemText });
        }
        #endregion
    }
}
