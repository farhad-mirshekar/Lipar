using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public interface IMediaModelFactory
    {
        MediaListModel PrepareMediaListModel(MediaSearchModel searchModel);
    }
}
