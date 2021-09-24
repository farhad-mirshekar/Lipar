using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface IGalleryService
    {
        void Add(Gallery model);
        void Edit(Gallery model);
        void Delete(Gallery model);
        IPagedList<Gallery> List(GalleryListVM listVM);
        Gallery GetById(int Id);
    }
}
