using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class BlogSettingModel : BaseEntityModel<Guid> , ISettingsModel
    {
        public int BlogPageSize { get; set; }
    }
}
