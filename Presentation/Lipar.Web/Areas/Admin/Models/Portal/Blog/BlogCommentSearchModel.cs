using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class BlogCommentSearchModel : BaseSearchModel
    {
        public Guid? BlogId { get; set; }
    }
}
