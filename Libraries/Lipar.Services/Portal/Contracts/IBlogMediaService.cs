using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
   public interface IBlogMediaService
    {
        IPagedList<BlogMedia> List(BlogMediaListVM listVM);
        void Save(BlogMedia blogMedia);
        void Delete(BlogMedia blogMedia);
    }
}
