using Lipar.Core.Infrastructure;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Localization;

namespace Lipar.Web.Framework.MVC.Razor
{
    public abstract class LiparRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        private Localizer _localizer;
        private ILocaleStringResourceService _localeStringResourceService;
        public Localizer T
        {
            get
            {
                if (_localeStringResourceService == null)
                    _localeStringResourceService = EngineContext.Current.Resolve<ILocaleStringResourceService>();

                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = _localeStringResourceService.GetResource(format);
                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return new LocalizedString(format);
                        }
                        return new LocalizedString((args == null || args.Length == 0)
                            ? resFormat
                            : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }
    }
}
