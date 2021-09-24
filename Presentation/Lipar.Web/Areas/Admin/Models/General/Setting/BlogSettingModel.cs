using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class BlogSettingModel : BaseEntityModel , ISettingsModel
    {
        public int BlogPageSize { get; set; }
    }
}
