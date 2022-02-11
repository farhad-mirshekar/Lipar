using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class LocaleStringResourceModel : BaseEntityModel<Guid>
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
    }
}
