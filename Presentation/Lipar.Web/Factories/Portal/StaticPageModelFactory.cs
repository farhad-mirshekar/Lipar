using Lipar.Entities.Domain.Portal;
using Lipar.Web.Models.Portal;
using System;

namespace Lipar.Web.Factories.Portal
{
    public class StaticPageModelFactory : IStaticPageModelFactory
    {
        public StaticPageModel PrepareStaticPageModel(StaticPage staticPage)
        {
            if (staticPage == null)
                throw new ArgumentNullException(nameof(staticPage));

            var staticPageModel = new StaticPageModel();
            staticPageModel.Id = staticPage.Id;
            staticPageModel.Name = staticPage.Name;
            staticPageModel.Title = staticPage.Title;
            staticPageModel.MetaDescription = staticPage.MetaDescription;
            staticPageModel.MetaKeywords = staticPage.MetaKeywords;
            staticPageModel.Body = staticPage.Body;
            staticPageModel.CreationDate = staticPage.CreationDate;

            return staticPageModel;

        }
    }
}
