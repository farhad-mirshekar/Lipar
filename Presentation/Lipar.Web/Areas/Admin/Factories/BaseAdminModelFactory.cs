using Lipar.Core.Caching;
using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using App = Lipar.Services.Application.Contracts;
using Lipar.Services.Caching;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Cache;
using Lipar.Web.Areas.Admin.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Lipar.Services.Core.Contracts;

namespace Lipar.Web.Areas.Admin.Factories
{
    public class BaseAdminModelFactory : IBaseAdminModelFactory
    {
        #region Ctor
        public BaseAdminModelFactory(ICategoryService categoryPortalService
                                   , IStaticCacheManager cacheManager
                                   , ICacheKeyService cacheKeyService
                                   , ILanguageService languageService
                                   , IMediaService mediaService
                                   , ICommandService commandService
                                   , IDepartmentService departmentService
                                   , App.ICategoryService categoryProductService
                                   , ILocaleStringResourceService localeStringResourceService
                                   , App.IShippingCostService shippingCostService
                                   , App.IDeliveryDateService deliveryDateService
                                   , App.IProductAttributeService productAttributeService
                                   , ICommentStatusService commentStatusService
                                   , IViewStatusService viewStatusService
                                   , IEnabledTypeService enabledTypeService
                                   , App.IAttributeControlTypeService attributeControlTypeService
                                   , ICommandTypeService commandTypeService
                                   , IContactUsTypeService contactUsTypeService
                                   , App.IDiscountTypeService discountTypeService
                                   , ILanguageCultureService languageCultureService
                                   , IPositionTypeService positionTypeService
                                   , IPasswordFormatTypeService passwordFormatTypeService
                                   , IEmailAccountService emailAccountService)
        {
            _categoryPortalService = categoryPortalService;
            _cacheKeyService = cacheKeyService;
            _cacheManager = cacheManager;
            _languageService = languageService;
            _mediaService = mediaService;
            _commandService = commandService;
            _departmentService = departmentService;
            _categoryProductService = categoryProductService;
            _localeStringResourceService = localeStringResourceService;
            _shippingCostService = shippingCostService;
            _deliveryDateService = deliveryDateService;
            _productAttributeService = productAttributeService;
            _commentStatusService = commentStatusService;
            _viewStatusService = viewStatusService;
            _enabledTypeService = enabledTypeService;
            _attributeControlTypeService = attributeControlTypeService;
            _commandTypeService = commandTypeService;
            _contactUsTypeService = contactUsTypeService;
            _discountTypeService = discountTypeService;
            _languageCultureService = languageCultureService;
            _positionTypeService = positionTypeService;
            _passwordFormatTypeService = passwordFormatTypeService;
            _emailAccountService = emailAccountService;
        }
        #endregion

        #region Fields
        private readonly ICategoryService _categoryPortalService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly ILanguageService _languageService;
        private readonly IMediaService _mediaService;
        private readonly ICommandService _commandService;
        private readonly IDepartmentService _departmentService;
        private readonly App.ICategoryService _categoryProductService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly App.IShippingCostService _shippingCostService;
        private readonly App.IDeliveryDateService _deliveryDateService;
        private readonly App.IProductAttributeService _productAttributeService;
        private readonly ICommentStatusService _commentStatusService;
        private readonly IViewStatusService _viewStatusService;
        private readonly IEnabledTypeService _enabledTypeService;
        private readonly App.IAttributeControlTypeService _attributeControlTypeService;
        private readonly ICommandTypeService _commandTypeService;
        private readonly IContactUsTypeService _contactUsTypeService;
        private readonly App.IDiscountTypeService _discountTypeService;
        private readonly ILanguageCultureService _languageCultureService;
        private readonly IPositionTypeService _positionTypeService;
        private readonly IPasswordFormatTypeService _passwordFormatTypeService;
        private readonly IEmailAccountService _emailAccountService;
        #endregion
        public void PrepareAllLanguage(IList<SelectListItem> items, string defaultItemText = null)
        {
            var languages = _languageService.List(new LanguageListVM { });

            foreach (var language in languages)
            {
                items.Add(new SelectListItem { Text = language.Name.Trim(), Value = language.Id.ToString() });
            }

            PrepareDefaultItem(items);
        }

        public void PrepareAttributeControlType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var attributeControlTypes = _attributeControlTypeService.List();

            foreach (var attributeControlType in attributeControlTypes)
            {
                items.Add(new SelectListItem { Text = attributeControlType.Title.Trim(), Value = attributeControlType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public IList<SelectListItem> PrepareAttributes(string defaultItemText = null)
        {
            var attributes = _productAttributeService.List(new Entities.Domain.Application.ProductAttributeListVM { });

            var items = new List<SelectListItem>();
            foreach (var attribute in attributes)
            {
                items.Add(new SelectListItem { Text = attribute.Name, Value = attribute.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
            return items;
        }
        public IList<SelectListItem> PrepareCategoriesForPortal(string defaultItemText = null)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(LiparModelCacheDefaults.Category_Portal_List_Key);

            return _cacheManager.Get(key, () =>
             {
                 var categories = _categoryPortalService.List(new Entities.Domain.Portal.CategoryListVM { });

                 var items = new List<SelectListItem>();
                 foreach (var category in categories)
                 {
                     var formattedBreadCrumb = _categoryPortalService.GetFormattedBreadCrumb(category);
                     items.Add(new SelectListItem { Text = formattedBreadCrumb, Value = category.Id.ToString() });
                 }

                 PrepareDefaultItem(items, defaultItemText);
                 return items;
             });

        }

        public IList<SelectListItem> PrepareCategoriesForProduct(string defaultItemText = null)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(LiparModelCacheDefaults.Category_Product_List_Key);

            return _cacheManager.Get(key, () =>
            {
                var categories = _categoryProductService.List(new Entities.Domain.Application.CategoryListVM
                {

                });

                var items = new List<SelectListItem>();
                foreach (var category in categories)
                {
                    var formattedBreadCrumb = _categoryProductService.GetFormattedBreadCrumb(category);
                    items.Add(new SelectListItem { Text = formattedBreadCrumb, Value = category.Id.ToString() });
                }

                PrepareDefaultItem(items, defaultItemText);
                return items;
            });
        }

        public IList<SelectListItem> PrepareCommand(string defaultItemText = null)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(LiparModelCacheDefaults.Command_List_Key);

            return _cacheManager.Get(key, () =>
            {
                var commands = _commandService.List(new CommandListVM { PageIndex = 0, PageSize = int.MaxValue });

                var items = new List<SelectListItem>();
                foreach (var command in commands)
                {
                    var formattedBreadCrumb = _commandService.GetFormattedBreadCrumb(command);
                    items.Add(new SelectListItem { Text = formattedBreadCrumb, Value = command.Id.ToString() });
                }

                PrepareDefaultItem(items, defaultItemText);
                return items;
            });
        }
        public void PrepareCommandType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var commandTypes = _commandTypeService.List();

            foreach (var commandType in commandTypes)
            {
                items.Add(new SelectListItem { Text = commandType.Title.Trim(), Value = commandType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        public void PrepareCommentStatusType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var commentStatuses = _commentStatusService.List();

            foreach (var commentStatus in commentStatuses)
            {
                items.Add(new SelectListItem { Text = commentStatus.Title.Trim(), Value = commentStatus.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public void PrepareContactUsType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var contactUsTypes = _contactUsTypeService.List();

            foreach (var contactUsType in contactUsTypes)
            {
                items.Add(new SelectListItem { Text = contactUsType.Title.Trim(), Value = contactUsType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        public void PrepareDefaultItem(IList<SelectListItem> items, string defaultItemText = null)
        {

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (defaultItemText == null)
            {
                defaultItemText = _localeStringResourceService.GetResource("Admin.Dropdown.SelectItem.Text");
            }

            //at now we use "null" as the default value
            const string value = "";

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = "" });
        }

        public IList<SelectListItem> PrepareDeliveryDate(string defaultItemText = null)
        {
            var deliveryDates = _deliveryDateService.List(new Entities.Domain.Application.DeliveryDateListVM { });

            var items = new List<SelectListItem>();
            foreach (var deliveryDate in deliveryDates)
            {
                items.Add(new SelectListItem { Text = deliveryDate.Name, Value = deliveryDate.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
            return items;
        }
        public IList<SelectListItem> PrepareDepartment(string defaultItemText = null)
        {
            return _cacheManager.Get(LiparModelCacheDefaults.Department_List_Key, () =>
             {
                 var departments = _departmentService.List(new DepartmentListVM { });
                 var items = new List<SelectListItem>();

                 foreach (var department in departments)
                 {
                     items.Add(new SelectListItem { Text = department.Name, Value = department.Id.ToString() });
                 }

                 PrepareDefaultItem(items, defaultItemText);
                 return items;
             });
        }

        public void PrepareDiscountType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var discountTypes = _discountTypeService.List();

            foreach (var discountType in discountTypes)
            {
                items.Add(new SelectListItem { Text = discountType.Title.Trim(), Value = discountType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public void PrepareEnabledType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var enabledTypes = _enabledTypeService.List();

            foreach (var enabledType in enabledTypes)
            {
                items.Add(new SelectListItem { Text = enabledType.Title.Trim(), Value = enabledType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public void PrepareForeignLinkType(IList<SelectListItem> items, string defaultItemText = null)
        {
            //foreach (var foreignLink in (ForeignLinkType[])Enum.GetValues(typeof(ForeignLinkType)))
            //{
            //    if (foreignLink != ForeignLinkType.Unknown)
            //        items.Add(new SelectListItem { Text = foreignLink.ToString().Replace("_", " "), Value = foreignLink.ToString() });
            //}

            //PrepareDefaultItem(items, defaultItemText);
        }

        public void PrepareLanguageCultureType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var languageCultures = _languageCultureService.List();

            foreach (var languageCulture in languageCultures)
            {
                items.Add(new SelectListItem { Text = languageCulture.Title.Trim(), Value = languageCulture.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        public MediaSearchModel PrepareMediaSearchModel(MediaSearchModel mediaSearch, int ParentId)
        {
            if (mediaSearch == null)
                throw new ArgumentNullException(nameof(mediaSearch));

            //mediaSearch.ParentId = ParentId;

            var media = _mediaService.List(new MediaListVM { ParentId = ParentId });
            if (media == null)
                throw new ArgumentNullException(nameof(media));

            mediaSearch.SetGridPageSize();

            return mediaSearch;

        }
        public void PreparePositionType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var positionTypes = _positionTypeService.List();

            foreach (var positionType in positionTypes)
            {
                items.Add(new SelectListItem { Text = positionType.Title.Trim(), Value = positionType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public IList<SelectListItem> PrepareShippingCost(string defaultItemText = null)
        {
            var shippingCost = _shippingCostService.List(new Entities.Domain.Application.ShippingCostListVM { });

            var items = new List<SelectListItem>();
            foreach (var shipping in shippingCost)
            {
                items.Add(new SelectListItem { Text = shipping.Name, Value = shipping.Id.ToString() });
            }

            PrepareDefaultItem(items, _localeStringResourceService.GetResource("Admin.Dropdown.SelectItem.Text"));
            return items;
        }
        public void PrepareViewStatusType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var viewStatuses = _viewStatusService.List();

            foreach (var viewStatus in viewStatuses)
            {
                items.Add(new SelectListItem { Text = viewStatus.Title.Trim(), Value = viewStatus.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public void PreparePasswordFormatType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var passwordFormatTypes = _passwordFormatTypeService.List();

            foreach (var passwordFormatType in passwordFormatTypes)
            {
                items.Add(new SelectListItem { Text = passwordFormatType.Title.Trim(), Value = passwordFormatType.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        public void PrepareEmailAccounts(IList<SelectListItem> items, string defaultItemText = null)
        {
            var emailAccounts = _emailAccountService.List(new EmailAccountListVM { });

            foreach (var emailAccount in emailAccounts)
            {
                items.Add(new SelectListItem { Text = emailAccount.Name.Trim(), Value = emailAccount.Id.ToString() });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
    }
}
