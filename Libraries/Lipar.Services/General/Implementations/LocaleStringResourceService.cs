using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.Caching;
using Lipar.Services.General.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class LocaleStringResourceService : ILocaleStringResourceService
    {
        #region Fields
        private readonly IRepository<LocaleStringResource> _repository;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public LocaleStringResourceService(IRepository<LocaleStringResource> repository
                                         , IStaticCacheManager cacheManager
                                         , ICacheKeyService cacheKeyService
                                         , IWorkContext workContext)
        {
            _repository = repository;
            _cacheManager = cacheManager;
            _cacheKeyService = cacheKeyService;
            _workContext = workContext;
        }
        #endregion

        public void Add(LocaleStringResource model)
        {
            var key = _cacheKeyService.PrepareKey(LiparPublicDefaults.Locale_String_Resource_Get_All_Resource_Values, model.LanguageId);
            _cacheManager.Remove(key);

            model.UserId = _workContext.CurrentUser.Id;
            _repository.Insert(model);
        }

        public void Delete(LocaleStringResource model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(LocaleStringResource model)
        {
            var key = _cacheKeyService.PrepareKey(LiparPublicDefaults.Locale_String_Resource_Get_All_Resource_Values, model.LanguageId);
            _cacheManager.Remove(key);

            model.UserId = _workContext.CurrentUser.Id;
            _repository.Update(model);
        }

        public LocaleStringResource GetById(int Id)
        => _repository.GetById(Id);

        public LocaleStringResource GetByResourceName(string ResourceName, int LanguageId)
        {
            var query = _repository.Table;
            query = query.Where(l => l.ResourceName == ResourceName && l.LanguageId == LanguageId);

            return query.FirstOrDefault();
        }

        public IPagedList<LocaleStringResource> List(LocaleStringResourceListVM listVM)
        {
            var query = _repository.Table;

            query = query.Where(r => r.LanguageId == listVM.LanguageId);

            var languages = new PagedList<LocaleStringResource>(query, listVM.PageIndex, listVM.PageSize, false);

            return languages;
        }

        public string GetResource(string Format)
        {
            return GetResource(Format, 1);
        }

        private string GetResource(string resourceKey, int LanguageId)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            var resources = GetAllResourceValues(LanguageId);
            if (resources == null || resources.Count() == 0)
                return null;

            if (resources.ContainsKey(resourceKey))
            {
                result = resources[resourceKey].Value;
            }
            return result;
        }
        private Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int LanguageId)
        {
            var key = _cacheKeyService.PrepareKey(LiparPublicDefaults.Locale_String_Resource_Get_All_Resource_Values, LanguageId);

            return _cacheManager.Get(key, () =>
            {
                var localeStringResources = List(new LocaleStringResourceListVM() { LanguageId = LanguageId });
                if (localeStringResources.Count() == 0)
                    return null;

                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();

                foreach (var locale in localeStringResources)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
                }

                return dictionary;
            });
        }
    }
}
