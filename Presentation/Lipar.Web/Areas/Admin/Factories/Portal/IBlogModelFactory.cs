using Lipar.Entities.Domain.Portal;
using Lipar.Web.Areas.Admin.Models.Portal;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public interface IBlogModelFactory
    {
        BlogListModel PrepareBlogListModel(BlogSearchModel searchModel);
        BlogModel PrepareBlogModel(BlogModel model, Blog blog);
        BlogMediaListModel PrepareBlogMediaListModel(BlogMediaSearchModel searchModel);
        BlogCommentSearchModel PrepareBlogCommentSearchModel(BlogCommentSearchModel searchModel, Blog blog);
        BlogCommentListModel PrepareBlogCommentListModel(BlogCommentSearchModel searchModel);
        BlogCommentModel PrepareBlogCommentModel(BlogCommentModel model, BlogComment blogComment);
    }
}
