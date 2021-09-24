using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface INewsCommentService
    {
        void Add(NewsComment model);
        void Edit(NewsComment model);
        void Delete(NewsComment model);
        IPagedList<NewsComment> List(NewsCommentListVM listVM);
    }
}
