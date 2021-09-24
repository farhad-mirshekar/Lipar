using Lipar.Core.ReadingTime;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class UrlRecordService : IUrlRecordService
    {
        #region Fields
        private readonly IRepository<UrlRecord> _repository;
        #endregion

        #region Ctor
        public UrlRecordService(IRepository<UrlRecord> repository)
        {
            _repository = repository;
        }
        #endregion
        public void Add(UrlRecord model)
        {
            model.Slug = SlugClean(model.Slug);
            _repository.Insert(model);
        }

        public void Edit(UrlRecord model)
        {
            model.Slug = SlugClean(model.Slug);
            _repository.Update(model);
        }

        public UrlRecord GetById(int Id)
        => _repository.GetById(Id);

        public void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityId = entity.Id;
            var entityName = entity.GetType().Name;

            var query = _repository.TableNoTracking;
            var urlRecord = query.Where(u => u.EntityId == entityId &&
                                   u.EntityName == entityName &&
                                   u.LanguageId == languageId).Select(u => u).FirstOrDefault();


            if (urlRecord == null)
            {
                Add(new UrlRecord
                {
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    EntityId = entityId,
                    EntityName = entityName,
                    LanguageId = languageId,
                    Slug = slug
                });
            }
            else
            {
                Edit(new UrlRecord
                {
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    EntityId = entityId,
                    EntityName = entityName,
                    LanguageId = languageId,
                    Slug = slug,
                    Id = urlRecord.Id
                });
            }
        }

        public UrlRecord GetBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            var query = _repository.Table;

            var urlRecord = query.Where(u => u.Slug.Equals(slug) && u.EnabledTypeId == (int)EnabledTypeEnum.Active).FirstOrDefault();

            return urlRecord;
        }

        #region Utilities
        protected string SlugClean(string slug)
        {
            return CalculateWordsCount.CleanSeoUrl(slug.Trim());
        }
        #endregion
    }
}
