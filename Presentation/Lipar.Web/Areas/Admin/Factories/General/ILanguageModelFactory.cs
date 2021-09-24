using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public interface ILanguageModelFactory
    {
        LanguageListModel PrepareLanguageListModel(LanguageSearchModel model);
        LanguageModel PrepareLanguageModel(LanguageModel model, Language language);
        LocaleStringResourceListModel PrepareLocaleStringResourceListModel(LocaleStringResourceSearchModel model);
    }
}
