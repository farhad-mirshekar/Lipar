using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class MediaModelFactory : IMediaModelFactory
    {
        #region Fields
        private readonly IMediaService _mediaService;
        #endregion

        #region Ctor
        public MediaModelFactory(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
        #endregion
        public MediaListModel PrepareMediaListModel(MediaSearchModel searchModel)
        {
            var media = _mediaService.List(new MediaListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new MediaListModel().PrepareToGrid(searchModel, media, () =>
            {
                return media.Select(m =>
                 {
                     var mediaModel = m.ToModel<MediaModel>();

                     return mediaModel;
                 });
            });

            return model;
        }
    }
}
