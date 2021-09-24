using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Framework.Models;
using Lipar.Web.Models.Portal;
using System;
using System.Linq;

namespace Lipar.Web.Factories.Portal
{
    public class DynamicPageModelFactory : IDynamicPageModelFactory
    {
        #region Fields
        private readonly IDynamicPageService _dynamicPageService;
        private readonly IDynamicPageDetailService _dynamicPageDetailService;
        #endregion

        #region Ctor
        public DynamicPageModelFactory(IDynamicPageService dynamicPageService
                                     , IDynamicPageDetailService dynamicPageDetailService)
        {
            _dynamicPageService = dynamicPageService;
            _dynamicPageDetailService = dynamicPageDetailService;
        }
        #endregion

        #region Methods
        public DynamicPageDetailListModel PrepareDynamicPageDetailListModel(DynamicPage dynamicPage,int PageIndex = 1, int PageSize = int.MaxValue)
        {
            var dynamicPageDetails = _dynamicPageDetailService.List(new DynamicPageDetailListVM
            {
                PageIndex = PageIndex - 1,
                PageSize = PageSize,
                DynamicPageId = dynamicPage.Id,
            });

            var dynamicPageListModel = new DynamicPageDetailListModel();

            dynamicPageListModel.DynamicPageModel.Id = dynamicPage.Id;
            dynamicPageListModel.DynamicPageModel.Name = dynamicPage.Name;
            dynamicPageListModel.DynamicPageModel.Title = dynamicPage.Title;

            var dynamicPageDetailsModel = dynamicPageDetails.Select(dynamicPage =>
             {
                 var dynamicPageDetail = new DynamicPageDetailModel();
                 dynamicPageDetail.Id = dynamicPage.Id;
                 dynamicPageDetail.CreationDate = dynamicPage.CreationDate;

                 dynamicPageDetail.Name = dynamicPage.Name;
                 dynamicPageDetail.Title = dynamicPage.Title;

                 return dynamicPageDetail;
             });

            dynamicPageListModel.AvailableDynamicPageDetailModel = dynamicPageDetailsModel;
            dynamicPageListModel.PagingInfo = new PagingInfo { CurrentPage = PageIndex == 0 ? 1 : PageIndex, TotalItems = dynamicPageDetails.TotalCount, ItemsPerPage = 2, Route = "DynamicPage" };

            return dynamicPageListModel;
        }

        public DynamicPageDetailModel PrepareDynamicPageDetailModel(DynamicPageDetail dynamicPageDetail)
        {
            if (dynamicPageDetail == null)
                throw new ArgumentNullException(nameof(dynamicPageDetail));

            var dynamicPageDetailModel = new DynamicPageDetailModel();
            
            dynamicPageDetailModel.Id = dynamicPageDetail.Id;
            dynamicPageDetailModel.Name = dynamicPageDetail.Name;
            dynamicPageDetailModel.Title = dynamicPageDetail.Title;
            dynamicPageDetailModel.MetaKeywords = dynamicPageDetail.MetaKeywords;
            dynamicPageDetailModel.MetaDescription = dynamicPageDetail.MetaDescription;
            dynamicPageDetailModel.Priority = dynamicPageDetail.Priority;
            dynamicPageDetailModel.Body = dynamicPageDetail.Body;

            var dynamicPage = _dynamicPageService.GetById(dynamicPageDetail.DynamicPageId);
            dynamicPageDetailModel.DynamicPageId = dynamicPage.Id;
            dynamicPageDetailModel.DynamicPageName = dynamicPage.Title;

            return dynamicPageDetailModel;
        }
        #endregion
    }
}
