using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface INewsService
    {
        void Add(News model);
        void Edit(News model);
        void Delete(News model);
        IPagedList<News> List(NewsListVM listVM);
    }
}
