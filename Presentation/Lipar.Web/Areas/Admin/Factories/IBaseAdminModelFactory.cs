using Lipar.Web.Areas.Admin.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Factories
{
    public interface IBaseAdminModelFactory
    {
        void PrepareDefaultItem(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareLanguageCultureType(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareViewStatusType(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareCommentStatusType(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareAllLanguage(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareForeignLinkType(IList<SelectListItem> items, string defaultItemText = null);
        void PrepareCommandType(IList<SelectListItem> items, string defaultItemText = null);
        void PreparePositionType(IList<SelectListItem> items, string defaultItemtext = null);
        void PrepareEnabledType(IList<SelectListItem> items, string defaultItemtext = null);
        /// <summary>
        /// prepare discount type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemtext"></param>
        void PrepareDiscountType(IList<SelectListItem> items, string defaultItemtext = null);
        /// <summary>
        /// prepare contact us type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemtext"></param>
        void PrepareContactUsType(IList<SelectListItem> items, string defaultItemtext = null);
        /// <summary>
        /// prepare attribute control type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemtext"></param>
        void PrepareAttributeControlType(IList<SelectListItem> items, string defaultItemtext = null);
        IList<SelectListItem> PrepareCategoriesForPortal(string defaultItemText = null);
        MediaSearchModel PrepareMediaSearchModel(MediaSearchModel mediaSearch, Guid ParentId);
        IList<SelectListItem> PrepareCommand(string defaultItemText = null);
        IList<SelectListItem> PrepareDepartment(string defaultItemText = null);
        /// <summary>
        /// prepare all categories product 
        /// </summary>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        IList<SelectListItem> PrepareCategoriesForProduct(string defaultItemText = null);
        /// <summary>
        /// prepare all shipping cost
        /// </summary>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        IList<SelectListItem> PrepareShippingCost(string defaultItemText = null);
        /// <summary>
        /// prepare all delivery date
        /// </summary>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        IList<SelectListItem> PrepareDeliveryDate(string defaultItemText = null);
        /// <summary>
        /// prepare all attributes
        /// </summary>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        IList<SelectListItem> PrepareAttributes(string defaultItemText = null);

        /// <summary>
        /// prepare password format type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        void PreparePasswordFormatType(IList<SelectListItem> items, string defaultItemText = null);

        /// <summary>
        /// prepare email account
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        void PrepareEmailAccounts(IList<SelectListItem> items, string defaultItemText = null);

        /// <summary>
        /// prepare product tags
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        void PrepareProductTags(IList<SelectListItem> items , string defaultItemText = null);
    }
}
