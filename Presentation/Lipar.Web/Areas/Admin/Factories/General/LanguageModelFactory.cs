using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class LanguageModelFactory : ILanguageModelFactory
    {
        #region Fields
        private readonly ILanguageService _languageService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public LanguageModelFactory(ILanguageService languageService
                                , ILocaleStringResourceService localeStringResourceService
                                , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _languageService = languageService;
            _localeStringResourceService = localeStringResourceService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        public LanguageModel PrepareLanguageModel(LanguageModel model, Language language)
        {
            if (language != null)
            {
                model ??= language.ToModel<LanguageModel, int>();
                PrepareLocaleResourceSearchModel(model.LocaleResourceSearchModel, language);
            }
            _baseAdminModelFactory.PrepareLanguageCultureType(model.AvailableLanguageCultureType);
            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }
        public LanguageListModel PrepareLanguageListModel(LanguageSearchModel searchModel)
        {
            var languages = _languageService.List(new LanguageListVM { Name = searchModel.Name, PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new LanguageListModel().PrepareToGrid(searchModel, languages, () =>
            {
                return languages.Select(language =>
                {
                    var languageModel = language.ToModel<LanguageModel, int>();

                    return languageModel;
                });
            });

            return model;
        }
        public LocaleStringResourceListModel PrepareLocaleStringResourceListModel(LocaleStringResourceSearchModel searchModel)
        {
            var localeStringResources = _localeStringResourceService.List(new LocaleStringResourceListVM { LanguageId = searchModel.LanguageId, PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new LocaleStringResourceListModel().PrepareToGrid(searchModel, localeStringResources, () =>
            {
                return localeStringResources.Select(localeStringResource =>
                {
                    var Resource = localeStringResource.ToModel<LocaleStringResourceModel, Guid>();

                    return Resource;
                });
            });

            return model;
        }

        #region Utilities
        protected LocaleStringResourceSearchModel PrepareLocaleResourceSearchModel(LocaleStringResourceSearchModel searchModel, Language language)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (language == null)
                throw new ArgumentNullException(nameof(language));

            searchModel.LanguageId = language.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
