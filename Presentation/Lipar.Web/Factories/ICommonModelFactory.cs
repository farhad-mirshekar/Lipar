using Lipar.Entities.Domain.General;
using Lipar.Web.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Factories
{
    public interface ICommonModelFactory
    {
        /// <summary>
        /// prepare menu main
        /// </summary>
        /// <returns></returns>
        MenuModel PrepareMenuModel();
        /// <summary>
        /// prepare drop down for contact us type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        void PrepareContacatUsType(IList<SelectListItem> items, string defaultItemText = null);
        /// <summary>
        /// prepare contact us model for create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="contactUs"></param>
        /// <returns></returns>
        ContactUsModel PrepareContactUsModel(ContactUsModel model);
    }
}
