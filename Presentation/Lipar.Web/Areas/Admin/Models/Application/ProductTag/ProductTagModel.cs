using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    /// <summary>
    /// product tag model
    /// </summary>
    public class ProductTagModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
    }
}
