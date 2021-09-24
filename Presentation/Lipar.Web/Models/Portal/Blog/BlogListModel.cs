using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Models.Portal
{
    public class BlogListModel
    {
        public BlogListModel()
        {
            AvailableBlogs = new HashSet<BlogModel>();
            PagingInfo = new PagingInfo();
        }
        public IEnumerable<BlogModel> AvailableBlogs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
