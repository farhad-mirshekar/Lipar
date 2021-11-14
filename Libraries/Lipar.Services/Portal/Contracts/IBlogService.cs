using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface IBlogService
    {
        void Add(Blog model);
        void Edit(Blog model);
        Blog GetById(int Id);
        void Delete(Blog model);
        IPagedList<Blog> List(BlogListVM listVM);
        void DeletePicture(int MediaId);
        /// <summary>
        /// load blog settings
        /// </summary>
        /// <returns></returns>
        BlogSetting BlogSettings();
    }
}
