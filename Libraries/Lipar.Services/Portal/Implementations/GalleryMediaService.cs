using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class GalleryMediaService : IGalleryMediaService
    {
        #region Fields
        private readonly IRepository<GalleryMedia> _repository;
        private readonly IRepository<Gallery> _galleryRepository;
        #endregion

        #region Ctor
        public GalleryMediaService(IRepository<GalleryMedia> repository
                                 , IRepository<Gallery> galleryRepository)
        {
            _repository = repository;
            _galleryRepository = galleryRepository;
        }
        #endregion


        #region Methods
        public void Delete(GalleryMedia galleryMedia)
        {
            if (galleryMedia == null)
                throw new ArgumentNullException(nameof(galleryMedia));

            _repository.Delete(galleryMedia);

        }

        public GalleryMedia GetById(Guid Id)
        => _repository.GetById(Id);

        public IPagedList<GalleryMedia> List(GalleryMediaListVM listVM)
        {
            var query = from bm in _repository.Table
                        join b in _galleryRepository.Table on bm.GalleryId equals b.Id
                        select bm;

            if (listVM.GalleryId.HasValue && listVM.GalleryId.Value != Guid.Empty)
                query = query.Where(x => x.GalleryId == listVM.GalleryId.Value);

            if (listVM.MediaId.HasValue && listVM.MediaId.Value != Guid.Empty)
                query = query.Where(x => x.MediaId == listVM.MediaId.Value);

            var galleryMediaList = new PagedList<GalleryMedia>(query, listVM.PageIndex, listVM.PageSize);

            return galleryMediaList;
        }

        public void Save(GalleryMedia galleryMedia)
        {
            var query = _repository.Table;

            var blogMediaMap = query.Where(bm => bm.GalleryId == galleryMedia.GalleryId && bm.MediaId == galleryMedia.MediaId).FirstOrDefault();

            if (blogMediaMap == null)
                _repository.Insert(galleryMedia);
            else
                _repository.Update(galleryMedia);
        }
        #endregion
    }
}
