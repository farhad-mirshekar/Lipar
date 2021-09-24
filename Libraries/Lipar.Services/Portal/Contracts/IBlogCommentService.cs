using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface IBlogCommentService
    {
        void Add(BlogComment model);
        void Edit(BlogComment model);
        void Delete(BlogComment model);
        IPagedList<BlogComment> List(BlogCommentListVM listVM);
        BlogComment GetById(int Id);
        int GetBlogCommentsCount(int blogId, CommentStatusEnum commentStatus);
    }
}
