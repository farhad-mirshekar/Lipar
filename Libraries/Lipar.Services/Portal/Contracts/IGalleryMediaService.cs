using Lipar.Core;
using Lipar.Entities.Domain.Portal;
using System;

namespace Lipar.Services.Portal.Contracts
{
   public interface IGalleryMediaService
    {
        /// <summary>
        /// Gets list gallery media
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<GalleryMedia> List(GalleryMediaListVM listVM);
        /// <summary>
        /// Add or edit 
        /// </summary>
        /// <param name="galleryMedia"></param>
        void Save(GalleryMedia galleryMedia);
        /// <summary>
        /// Delete gallery media
        /// </summary>
        /// <param name="galleryMedia"></param>
        void Delete(GalleryMedia galleryMedia);
        /// <summary>
        /// Get gallery media by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        GalleryMedia GetById(Guid Id);
    }
}
