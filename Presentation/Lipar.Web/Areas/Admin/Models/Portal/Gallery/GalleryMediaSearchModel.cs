using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class GalleryMediaSearchModel : BaseSearchModel
    {
        #region Ctor
        public GalleryMediaSearchModel()
        {
            AddGalleryMediaModel = new GalleryMediaModel();
        }
        #endregion

        #region Fields
        public Guid GalleryId { get; set; }
        public GalleryMediaModel AddGalleryMediaModel { get; set; }
        #endregion
    }
}
