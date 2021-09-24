using Lipar.Entities;
using Lipar.Web.Areas.Admin.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class GalleryModel : BaseEntityModel
    {
        #region Ctor
        public GalleryModel()
        {
            AvailableViewStatusType = new List<SelectListItem>();
            GalleryMediaSearchModel = new GalleryMediaSearchModel();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Body { get; set; }
        public int ViewStatusId { get; set; }
        public string MetaKeywords { get; set; }
        public GalleryMediaSearchModel GalleryMediaSearchModel { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        #endregion
    }
}
