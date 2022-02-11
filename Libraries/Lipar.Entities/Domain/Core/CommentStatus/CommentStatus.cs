using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Portal;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Core
{
    public class CommentStatus : BaseEntity<int>
    {
        #region Ctor
        public CommentStatus()
        {
            Blogs = new HashSet<Blog>();
            BlogComments = new HashSet<BlogComment>();
            News = new HashSet<News>();
            NewsComments = new HashSet<NewsComment>();
            ProductComments = new HashSet<ProductComment>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<NewsComment> NewsComments { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        #endregion
    }
}
