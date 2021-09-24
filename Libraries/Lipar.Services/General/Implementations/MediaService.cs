using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Core.Infrastructure;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class MediaService : IMediaService
    {
        #region Fields
        private readonly IRepository<Media> _repository;
        private readonly IRepository<MediaBinary> _mediaBinaryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILiparFileProvider _fileProvider;
        #endregion

        #region Ctor
        public MediaService(IRepository<Media> repository
                          , IHttpContextAccessor httpContextAccessor
                          , ILiparFileProvider fileProvider
                          , IRepository<MediaBinary> mediaBinaryRepository)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _fileProvider = fileProvider;
            _mediaBinaryRepository = mediaBinaryRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Insert Media Such As : Picture,File
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public Media AddPicture(IFormFile formFile)
        {
            var fileName = _fileProvider.GetFileNameWithoutExtension(formFile.FileName);

            string fileExtension = _fileProvider.GetFileExtension(formFile.FileName);

            if (!CommonHelper.IsValidateExtention(fileExtension))
                return null;

            var media = new Media
            {
                AltAttribute = null,
                Name = fileName,
                MimeType = fileExtension
            };
            _repository.Insert(media);

            _mediaBinaryRepository.Insert(new MediaBinary
            {
                MediaId = media.Id,
                BinaryData = GetDownloadBits(formFile)
            });

            // Save the file
            SavePictureInFile(media.Id, fileName, GetDownloadBits(formFile), fileExtension);

            return media;
        }
        /// <summary>
        /// Edit Media For Other Field Such As : Name , AltAttribute
        /// </summary>
        /// <param name="model">Media Model</param>
        /// <returns></returns>
        public Media EditPicture(Media model)
        {
            var query = _repository.Table;
            var media = query.Where(m => m.Id == model.Id).AsNoTracking().FirstOrDefault();

            if (media == null)
                return null;

            model.MimeType = media.MimeType;

            if (media.Name != model.Name)
            {
                DeletePicture(media.Id, $"{media.Name}{media.MimeType}");

                var mediaBinaryQuery = _mediaBinaryRepository.Table;
                var mediaBinary = mediaBinaryQuery.Where(x => x.MediaId == model.Id).FirstOrDefault();

                // Save the file
                SavePictureInFile(model.Id, model.Name, mediaBinary.BinaryData ?? Array.Empty<byte>(), model.MimeType);
            }


            _repository.Update(model);

            return model;
        }
        /// <summary>
        /// Delete Media
        /// </summary>
        /// <param name="Id">Media Id</param>
        /// <returns></returns>
        public void Delete(Media model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            DeletePicture(model.Id, $"{model.Name}{model.MimeType}");

           // _mediaBinaryRepository.Delete(new MediaBinary { MediaId = model.Id });

             _repository.Delete(model);
        }
        public Media GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<Media> List(MediaListVM listVM)
        {
            var query = _repository.Table;

            var models = new PagedList<Media>(query, listVM.PageIndex, listVM.PageSize, false);

            return models;
        }

        /// <summary>
        /// گرفتن آدرس تصویر
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public string GetImageUrl(Media media)
        {
            if (media == null)
                return "";

            var fileName = $"{media.Id}-{media.Name}{media.MimeType}";

            var url = "/" + GetImagesPathUrl();
            url += fileName;

            return url;
        }
        #endregion

        #region Utilities
        protected virtual void SavePictureInFile(int pictureId, string fileNameSeo, byte[] pictureBinary, string mimeType)
        {
            var fileName = $"{pictureId}-{fileNameSeo}{mimeType}";
            _fileProvider.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }
        protected virtual string GetPictureLocalPath(string fileName)
        {
            return _fileProvider.GetAbsolutePath("images/uploaded", fileName);
        }
        protected virtual byte[] GetDownloadBits(IFormFile file)
        {
            using var fileStream = file.OpenReadStream();
            using var ms = new MemoryStream();
            fileStream.CopyTo(ms);
            var fileBytes = ms.ToArray();
            return fileBytes;
        }
        protected virtual string GetImagesPathUrl()
        {
            var imagesPathUrl = "images/uploaded/";

            return imagesPathUrl;
        }
        protected void DeletePicture(int Id, string fileName)
        {
            var filter = $"{Id}-{fileName}";

            var currentFiles = _fileProvider.GetFiles(_fileProvider.GetAbsolutePath(LiparPublicDefaults.ImagePath), filter, false);
            foreach (var thumbFileName in currentFiles)
            {
                var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(LiparPublicDefaults.ImagePath);
                var thumbFilePath = _fileProvider.Combine(thumbsDirectoryPath, thumbFileName);
                _fileProvider.DeleteFile(thumbFilePath);
            }
        }
        #endregion
    }
}
