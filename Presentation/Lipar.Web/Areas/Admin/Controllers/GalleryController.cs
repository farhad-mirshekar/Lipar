using Lipar.Entities.Domain.Portal;
using Lipar.Entities.Domain.General;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class GalleryController : BaseAdminController
    {
        #region Fields
        private readonly IGalleryModelFactory _galleryModelFactory;
        private readonly IGalleryService _galleryService;
        private readonly IMediaService _mediaService;
        private readonly IGalleryMediaService _galleryMediaService;
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public GalleryController(IGalleryModelFactory galleryModelFactory
                               , IGalleryService galleryService
                               , IMediaService mediaService
                               , IGalleryMediaService galleryMediaService
                               , ICommandService commandService
                               , IActivityLogService activityLogService
                               , ILocaleStringResourceService localeStringResourceService)
        {
            _galleryModelFactory = galleryModelFactory;
            _galleryService = galleryService;
            _mediaService = mediaService;
            _galleryMediaService = galleryMediaService;
            _commandService = commandService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Gallery Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new GallerySearchModel());

        [HttpPost]
        public IActionResult List(GallerySearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageGallery"))
                return AccessDeniedView();

            var model = _galleryModelFactory.PrepareGalleryListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageGallery"))
                return AccessDeniedView();

            var model = _galleryModelFactory.PrepareGalleryModel(new GalleryModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(GalleryModel model)
        {
            if (!_commandService.CheckPermission("ManageGallery"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var gallery = model.ToEntity<Gallery>();

                _galleryService.Add(gallery);

                // add activity log for create gallery
                _activityLogService.Add("Admin.Gallery.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.Gallery.Create"), gallery);

                //success add
                return RedirectToAction("Edit", new { Id = gallery.Id });
            }

            model = _galleryModelFactory.PrepareGalleryModel(model, null);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageGallery"))
                return AccessDeniedView();

            var gallery = _galleryService.GetById(Id);
            if (gallery == null)
                return RedirectToAction("List");

            var model = _galleryModelFactory.PrepareGalleryModel(null, gallery);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(GalleryModel model)
        {
            if (!_commandService.CheckPermission("ManageGallery"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var gallery = model.ToEntity<Gallery>();

                _galleryService.Edit(gallery);

                // add activity log for edit gallery
                _activityLogService.Add("Admin.Gallery.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Gallery.Edit"), gallery);

                //success add
                return RedirectToAction("Edit", new { Id = gallery.Id });
            }

            model = _galleryModelFactory.PrepareGalleryModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var gallery = _galleryService.GetById(Id);
            if (gallery == null)
                return RedirectToAction("List");

            _galleryService.Delete(gallery);

            // add activity log for delete gallery
            _activityLogService.Add("Admin.Gallery.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Gallery.Delete"), gallery);

            return RedirectToAction("List");
        }
        #endregion

        #region Gallery Media
        [HttpPost]
        public IActionResult GalleryMediaList(GalleryMediaSearchModel searchModel)
        {
            var model = _galleryModelFactory.PrepareGalleryMediaListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public IActionResult GalleryMediaAdd(GalleryMediaModel model)
        {
            _mediaService.EditPicture(new Media
            {
                Id = model.MediaId,
                AltAttribute = model.AltAttribute,
                Name = model.Name
            });


            _galleryMediaService.Save(new GalleryMedia { GalleryId = model.GalleryId, MediaId = model.MediaId });

            return Json(new { Result = true });
        }

        [HttpPost]
        public IActionResult GalleryMediaDelete(int Id)
        {

            //_mediaService.Delete(Id);
            //_blogService.DeletePicture(Id);

            return new NullJsonResult();
        }
        [HttpPost]
        public IActionResult GalleryMediaEdit(GalleryMediaModel model)
        {
            var galleryMedia = _galleryMediaService.GetById(model.Id) ?? throw new ArgumentException("No picture found with the specified id");
            var media = _mediaService.GetById(galleryMedia.MediaId) ?? throw new ArgumentException("No picture found with the specified id");

            media.AltAttribute = model.AltAttribute;
            media.Name = model.Name;

            _mediaService.EditPicture(media);

            galleryMedia.Priority = model.Priority;

            _galleryMediaService.Save(galleryMedia);

            return new NullJsonResult();
        }
        #endregion
    }
}
