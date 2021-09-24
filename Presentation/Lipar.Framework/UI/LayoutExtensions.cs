using Lipar.Core.Infrastructure;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lipar.Web.Framework.UI
{
    public static class LayoutExtensions
    {
        public static void SetActiveMenuItemSystemName(this IHtmlHelper html, string systemName)
        {
            var _pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            _pageHeadBuilder.SetActiveMenuItemSystemName(systemName);
        }

        public static string GetActiveMenuItemSystemName(this IHtmlHelper html)
        {
            var _pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            return _pageHeadBuilder.GetActiveMenuItemSystemName();
        }

        public static void AddMetaDescription(this IHtmlHelper html, string part)
        {
            var _pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            _pageHeadBuilder.AddMetaDescription(part);
        }

        public static IHtmlContent GenerateMetaDescription(this IHtmlHelper html, string part = "")
        {
            var _pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            html.ApendMetaDescription(part);
            return new HtmlString(html.Encode(_pageHeadBuilder.GenerateMetaDescription()));
        }

        public static void ApendMetaDescription(this IHtmlHelper html, string part)
        {
            var _pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            _pageHeadBuilder.AppendMetaDescription(part);
        }
    }
}
