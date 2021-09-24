using Lipar.Entities;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IUrlRecordService
    {
        void Add(UrlRecord model);
        void Edit(UrlRecord model);
        UrlRecord GetById(int Id);
        void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity;
        UrlRecord GetBySlug(string slug);
    }
}
