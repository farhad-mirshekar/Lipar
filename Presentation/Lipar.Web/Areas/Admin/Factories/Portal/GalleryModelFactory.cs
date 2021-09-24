using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public class GalleryModelFactory : IGalleryModelFactory
    {
        #region Fields
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IGalleryService _galleryService;
        private readonly IGalleryMediaService _galleryMediaService;
        private readonly IMediaService _mediaService;
        #endregion

        #region Ctor
        public GalleryModelFactory(IBaseAdminModelFactory baseAdminModelFactory
                                 , IGalleryService galleryService
                                 , IGalleryMediaService galleryMediaService
                                 , IMediaService mediaService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _galleryService = galleryService;
            _galleryMediaService = galleryMediaService;
            _mediaService = mediaService;
        }
        #endregion
        public GalleryListModel PrepareGalleryListModel(GallerySearchModel searchModel)
        {
            var galleries = _galleryService.List(new GalleryListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize, Name = searchModel.Name });

            var model = new GalleryListModel().PrepareToGrid(searchModel, galleries, () =>
            {
                return galleries.Select(gallery =>
                {
                    var galleryModel = gallery.ToModel<GalleryModel>();

                    return galleryModel;
                });
            });

            return model;
        }

        public GalleryMediaListModel PrepareGalleryMediaListModel(GalleryMediaSearchModel searchModel)
        {
            var galleryMedia = _galleryMediaService.List(new GalleryMediaListVM { GalleryId = searchModel.GalleryId, PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new GalleryMediaListModel().PrepareToGrid(searchModel, galleryMedia, () =>
            {
                return galleryMedia.Select(bm =>
                {
                    var galleryMediaModel = bm.ToModel<GalleryMediaModel>();

                    var mediaResult = _mediaService.GetById(galleryMediaModel.MediaId);
                    if (mediaResult == null)
                        throw new Exception(nameof(mediaResult));

                    galleryMediaModel.AltAttribute = mediaResult.AltAttribute;
                    //blogMedia.Priority = mediaResult.Data.Priority;
                    galleryMediaModel.Name = mediaResult.Name;

                    return galleryMediaModel;
                });
            });

            return model;
        }

        public GalleryModel PrepareGalleryModel(GalleryModel model, Gallery gallery)
        {
            if (gallery != null)
            {
                model = gallery.ToModel<GalleryModel>();
            }

            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }

        #region Utilities
        protected GalleryMediaSearchModel PrepareGalleryMediaSearchModel(GalleryMediaSearchModel searchModel, Gallery gallery)
        {
            if (searchModel == null)
                throw new Exception(nameof(searchModel));

            if (gallery == null)
                throw new Exception(nameof(gallery));

            searchModel.GalleryId = gallery.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
