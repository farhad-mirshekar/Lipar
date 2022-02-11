using Lipar.Entities;
using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Services.General.Contracts
{
    public interface IUrlRecordService
    {
        void Add(UrlRecord model);
        void Edit(UrlRecord model);
        UrlRecord GetById(Guid Id);
        void SaveSlug<T, TProperty>(T entity, string slug, int languageId) where T : BaseEntity<TProperty>;
        UrlRecord GetBySlug(string slug);
    }
}
