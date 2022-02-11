using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class FaqGroupModelFactory : IFaqGroupModelFactory
    {
        #region Fields
        private readonly IFaqGroupService _faqGroupService;
        private readonly IFaqService _faqService;
        #endregion

        #region Ctor
        public FaqGroupModelFactory(IFaqGroupService faqGroupService
                                  , IFaqService faqService)
        {
            _faqGroupService = faqGroupService;
            _faqService = faqService;
        }
        #endregion

        #region Methods
        public FaqGroupListModel PrepareFaqGroupListModel(FaqGroupSearchModel searchModel)
        {
            var faqGroups = _faqGroupService.List(
                new FaqGroupListVM
                {
                    Name = searchModel.Name,
                    PageIndex = searchModel.Page - 1,
                    PageSize = searchModel.Page
                });

            if (faqGroups == null)
                throw new ArgumentNullException(nameof(faqGroups));

            var model = new FaqGroupListModel().PrepareToGrid(searchModel, faqGroups, () =>
            {
                return faqGroups.Select(faqGroup =>
                {
                    var faqGroupModel = faqGroup.ToModel<FaqGroupModel, Guid>();

                    return faqGroupModel;
                });
            });

            return model;
        }

        public FaqGroupModel PrepareFaqGroupModel(FaqGroupModel model, FaqGroup faqGroup)
        {
            if(faqGroup != null)
            {
                model = faqGroup.ToModel<FaqGroupModel, Guid>();

                //set grid faq cartable
                PrepareFaqSearchModel(model.FaqSearchModel, faqGroup);
            }

            return model;
        }

        public FaqListModel PrepareFaqListModel(FaqSearchModel searchModel)
        {
            var faqs = _faqService.List(
                new FaqListVM
                {
                    FaqGroupId = searchModel.FaqGroupId,
                    PageIndex = searchModel.Page - 1,
                    PageSize = searchModel.PageSize
                });

            if (faqs == null)
                throw new ArgumentNullException(nameof(faqs));

            var model = new FaqListModel().PrepareToGrid(searchModel, faqs, () =>
            {
                return faqs.Select(faq =>
                {
                    var faqModel = faq.ToModel<FaqModel, Guid>();

                    return faqModel;
                });
            });

            return model;
        }

        public FaqModel PrepareFaqModel(FaqModel model, Faq faq)
        {
            if (faq != null)
            {
                model = faq.ToModel<FaqModel, Guid>();
            }

            return model;
        }
        #endregion

        #region Utilities
        protected FaqSearchModel PrepareFaqSearchModel(FaqSearchModel searchModel , FaqGroup faqGroup)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (faqGroup == null)
                throw new ArgumentNullException(nameof(faqGroup));

            searchModel.FaqGroupId = faqGroup.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
