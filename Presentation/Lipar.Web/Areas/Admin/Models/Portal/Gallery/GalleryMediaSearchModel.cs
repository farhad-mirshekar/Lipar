using Nop.Web.Framework.Models;

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
        public int GalleryId { get; set; }
        public GalleryMediaModel AddGalleryMediaModel { get; set; }
        #endregion
    }
}
