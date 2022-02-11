using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class BlogMediaSearchModel : BaseSearchModel
    {
        public BlogMediaSearchModel()
        {
            AddBlogMediaModel = new BlogMediaModel();
        }
        public Guid BlogId { get; set; }

        public BlogMediaModel AddBlogMediaModel { get; set; }
    }
}
