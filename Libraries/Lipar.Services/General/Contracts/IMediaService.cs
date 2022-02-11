using Lipar.Core;
using Lipar.Entities.Domain.General;
using Microsoft.AspNetCore.Http;
using System;

namespace Lipar.Services.General.Contracts
{
    public interface IMediaService
    {
        Media GetById(Guid Id);
        void Delete(Media model);
        IPagedList<Media> List(MediaListVM listVM);
        Media AddPicture(IFormFile formFile);
        Media EditPicture(Media model);
        string GetImageUrl(Media media);
    }
}
