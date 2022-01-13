using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class BlogMediaSearchModel : BaseSearchModel
    {
        public BlogMediaSearchModel()
        {
            AddBlogMediaModel = new BlogMediaModel();
        }
        public int BlogId { get; set; }

        public BlogMediaModel AddBlogMediaModel { get; set; }
    }
}
