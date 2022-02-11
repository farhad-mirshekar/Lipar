using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using System;

namespace Lipar.Services.Portal.Contracts
{
    public interface IBlogCommentService
    {
        void Add(BlogComment model);
        void Edit(BlogComment model);
        void Delete(BlogComment model);
        IPagedList<BlogComment> List(BlogCommentListVM listVM);
        BlogComment GetById(Guid Id);
        int GetBlogCommentsCount(Guid blogId, CommentStatusEnum commentStatus);
    }
}
