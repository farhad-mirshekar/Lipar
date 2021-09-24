using Lipar.Entities.Domain.Portal;
using Lipar.Web.Models.Portal;

namespace Lipar.Web.Factories.Portal
{
    public interface IBlogModelFactory
    {
        /// <summary>
        /// List Blog
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        BlogListModel PrepareBlogList(int PageIndex = 0, int PageSize = 5);
        /// <summary>
        /// Get Blog
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        BlogModel PreapareBlogModel(BlogModel blogModel , Blog blog , bool showComments);

        /// <summary>
        /// prepare blog comment model list
        /// </summary>
        /// <param name="blogComment"></param>
        /// <returns></returns>
        BlogCommentListModel PrepareBlogCommentListModel(BlogComment blogComment);

        /// <summary>
        /// prepare blog comment model
        /// </summary>
        /// <param name="blogComment"></param>
        /// <returns></returns>
        BlogCommentModel PrepareBlogCommentModel(BlogModel blogModel);
    }
}
