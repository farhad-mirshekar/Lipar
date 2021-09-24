using Lipar.Core.Infrastructure;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lipar.Services.General.Implementations
{
    public class FroalaEditorService : IFroalaEditorService
    {
        #region Fields
        private readonly ILiparFileProvider _fileProvider;
        #endregion

        #region Ctor
        public FroalaEditorService(ILiparFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        #endregion
        public string UploadPicture(IFormFile formFile)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;

            var imgExt = new List<string>
            {
                ".jpg",
                ".png",
            } as IReadOnlyCollection<string>;

            if (formFile == null)
                Result.Failure(message: "");

            if (formFile.Length > 0)
            {
                fileName = _fileProvider.GetFileName(formFile.FileName);

                string fileExtension = _fileProvider.GetFileExtension(fileName);

                if (imgExt.All(ext => !ext.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase)))
                    return null;

                // Save the file
                SavePictureInFile(fileName, GetDownloadBits(formFile));

                return $"/images/editor/froala-{fileName}";
            }
            return null;
        }
        public void DeletePicture(string fileName)
        {
            var filter = $"{fileName}";

            var currentFiles = _fileProvider.GetFiles(_fileProvider.GetAbsolutePath(LiparPublicDefaults.ImageFroalaPath), filter, false);
            foreach (var thumbFileName in currentFiles)
            {
                var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(LiparPublicDefaults.ImageFroalaPath);
                var thumbFilePath = _fileProvider.Combine(thumbsDirectoryPath, thumbFileName);
                _fileProvider.DeleteFile(thumbFilePath);
            }
        }

        #region Utilities
        protected virtual void SavePictureInFile(string fileNameSeo, byte[] pictureBinary)
        {
            var fileName = $"froala-{fileNameSeo}";
            _fileProvider.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }
        protected virtual string GetPictureLocalPath(string fileName)
        {
            return _fileProvider.GetAbsolutePath("images/editor", fileName);
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
            var imagesPathUrl = "images/editor";

            return imagesPathUrl;
        }
        #endregion
    }
}
