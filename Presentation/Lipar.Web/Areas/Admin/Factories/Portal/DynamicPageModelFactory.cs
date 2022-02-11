using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public class DynamicPageModelFactory : IDynamicPageModelFactory
    {
        #region Fields
        private readonly IDynamicPageService _dynamicPageService;
        private readonly IDynamicPageDetailService _dynamicPageDetailService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public DynamicPageModelFactory(IDynamicPageService dynamicPageService
                                     , IDynamicPageDetailService dynamicPageDetailService
                                     , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _dynamicPageService = dynamicPageService;
            _dynamicPageDetailService = dynamicPageDetailService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion
        public DynamicPageDetailListModel PrepareDynamicPageDetailListModel(DynamicPageDetailSearchModel searchModel)
        {
            var dynamicPageDetails = _dynamicPageDetailService.List(new DynamicPageDetailListVM
            {
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if (dynamicPageDetails == null)
                return null;

            var model = new DynamicPageDetailListModel().PrepareToGrid(searchModel, dynamicPageDetails, () =>
            {
               return dynamicPageDetails.Select(dynamicPageDetail =>
                {
                    var dynamicPageDetailModel = new DynamicPageDetailModel();

                    dynamicPageDetailModel = dynamicPageDetail.ToModel<DynamicPageDetailModel, Guid>();

                    return dynamicPageDetailModel;
                });
            });

            return model;
        }

        public DynamicPageDetailModel PrepareDynamicPageDetailModel(DynamicPageDetailModel model, DynamicPageDetail dynamicPageDetail)
        {
            if(dynamicPageDetail != null)
            {
                model = dynamicPageDetail.ToModel<DynamicPageDetailModel, Guid>();
            }

            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }

        public DynamicPageDetailSearchModel PrepareDynamicPageDetailSearchModel(DynamicPageDetailSearchModel searchModel, DynamicPage dynamicPage)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //if (dynamicPage == null)
            //    throw new ArgumentNullException(nameof(dynamicPage));

            searchModel.DynamicPageId = dynamicPage?.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }

        public DynamicPageListModel PrepareDynamicPageListModel(DynamicPageSearchModel searchModel)
        {
            var dynamicPages = _dynamicPageService.List(new DynamicPageListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if (dynamicPages == null)
                return null;

            var model = new DynamicPageListModel().PrepareToGrid(searchModel, dynamicPages, () =>
            {
                return dynamicPages.Select(dynamicPage =>
                {
                    var dynamicPageModel = new DynamicPageModel();
                    dynamicPageModel = dynamicPage.ToModel<DynamicPageModel, Guid>();

                    dynamicPageModel.ApprovedDynamicPageDetail = _dynamicPageDetailService.GetDynamicPageDetailCount(dynamicPage.Id, ViewStatusEnum.Active);
                    dynamicPageModel.NotApprovedDynamicPageDetail = _dynamicPageDetailService.GetDynamicPageDetailCount(dynamicPage.Id, ViewStatusEnum.InActive);

                    return dynamicPageModel;
                });
            });

            return model;
        }

        public DynamicPageModel PrepareDynamicPageModel(DynamicPageModel model, DynamicPage dynamicPage)
        {
            if(dynamicPage != null)
            {
                model = dynamicPage.ToModel<DynamicPageModel, Guid>();
            }

            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }
    }
}
