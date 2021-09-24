using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public class StaticPageModelFactory : IStaticPageModelFactory
    {
        #region Fields
        private readonly IStaticPageService _staticPageService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public StaticPageModelFactory(IStaticPageService staticPageService
                                    , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _staticPageService = staticPageService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Methods
        public StaticPageListModel PrepareStaticPageListModel(StaticPageSearchModel searchModel)
        {
            var staticPages = _staticPageService.List(new StaticPageListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            var model = new StaticPageListModel().PrepareToGrid(searchModel, staticPages, () =>
            {
                return staticPages.Select(staticPage =>
                {
                    var staticPageModel = staticPage.ToModel<StaticPageModel>();

                    return staticPageModel;
                });
            });

            return model;
        }

        public StaticPageModel PrepareStaticPageModel(StaticPageModel model, StaticPage staticPage)
        {
            if (staticPage != null)
            {
                model = staticPage.ToModel<StaticPageModel>();
            }

            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }
        #endregion
    }
}
