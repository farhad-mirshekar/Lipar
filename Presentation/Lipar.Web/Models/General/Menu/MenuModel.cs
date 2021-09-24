using Lipar.Web.Models.Common;
using Lipar.Web.Models.Portal;
using System.Collections.Generic;

namespace Lipar.Web.Models.General
{
    public class MenuModel
    {
        public MenuModel()
        {
            StaticPages = new List<StaticPageModel>();
            dynamicPages = new List<DynamicPageModel>();
        }
        /// <summary>
        /// list static page in show menu
        /// </summary>
        public IList<StaticPageModel> StaticPages { get; set; }
        /// <summary>
        /// info user login
        /// </summary>
        public UserHeaderLinkModel UserHeaderLink { get; set; }
        /// <summary>
        /// list dynamic page in show menu
        /// </summary>
        public IList<DynamicPageModel> dynamicPages { get; set; }

    }
}
