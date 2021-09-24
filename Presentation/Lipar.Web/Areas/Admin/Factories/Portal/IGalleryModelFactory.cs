using Lipar.Entities.Domain.Portal;
using Lipar.Web.Areas.Admin.Models.Portal;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public interface IGalleryModelFactory
    {
        GalleryListModel PrepareGalleryListModel(GallerySearchModel searchModel);
        GalleryModel PrepareGalleryModel(GalleryModel model, Gallery gallery);
        GalleryMediaListModel PrepareGalleryMediaListModel(GalleryMediaSearchModel searchModel);
    }
}
