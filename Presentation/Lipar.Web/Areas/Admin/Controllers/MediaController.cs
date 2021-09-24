using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class MediaController : BaseAdminController
    {
        #region Fields
        private readonly IMediaService _mediaService;
        private readonly IMediaModelFactory _mediaModelFactory;
        private readonly IFroalaEditorService _froalaEditorService;
        #endregion
        public MediaController(IMediaService mediaService
                             , IMediaModelFactory mediaModelFactory
                             , IFroalaEditorService froalaEditorService)
        {
            _mediaService = mediaService;
            _mediaModelFactory = mediaModelFactory;
            _froalaEditorService = froalaEditorService;
        }
        #region Ctor

        #endregion

        #region Methods
        [HttpPost]
        public IActionResult List(MediaSearchModel searchModel)
        {
            var model = _mediaModelFactory.PrepareMediaListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Upload()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded"
                });
            }

            const string qqFileNameParameter = "qqfilename";

            var qqFileName = Request.Form.ContainsKey(qqFileNameParameter)
                ? Request.Form[qqFileNameParameter].ToString()
                : string.Empty;

            var media = _mediaService.AddPicture(httpPostedFile);
            if (media == null)
                return Json(new
                {
                    success = false,
                    message = "No file uploaded"
                });

            return Json(new { success = true, mediaId = media.Id, imageUrl = _mediaService.GetImageUrl(media) });
        }

        [HttpPost]
        public IActionResult Edit(int ParentId, MediaModel model)
        {
            var media = model.ToEntity<Media>();
            media.Id = model.MediaId != 0 ? model.MediaId : model.Id;

            media = _mediaService.EditPicture(media);
            if (media == null)
                return ErrorJson(string.Format("error"));

            return Json(new { Result = true });
        }
        #endregion

        #region Froala-Editor
        public IActionResult PictureFroala()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded"
                });
            }
            var froalaEditorResult = _froalaEditorService.UploadPicture(httpPostedFile);

            if (string.IsNullOrEmpty(froalaEditorResult))
                return Json(new
                {
                    success = false,
                    message = "No file uploaded"
                });

            return Json(new { success = true, link = froalaEditorResult });
        }
        [HttpPost]
        public IActionResult DeletePictureFroala(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded"
                });
            }
            _froalaEditorService.DeletePicture(fileName.Trim());

            return Json(new { success = true });
        }
        #endregion
    }
}
