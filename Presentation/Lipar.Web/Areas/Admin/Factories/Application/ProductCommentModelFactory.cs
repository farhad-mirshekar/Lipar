using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ProductCommentModelFactory : IProductCommentModelFactory
    {
        #region Ctor
        public ProductCommentModelFactory(IProductCommentService productCommentService
                                        , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _productCommentService = productCommentService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Fields
        private readonly IProductCommentService _productCommentService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Methods
        public ProductCommentListModel PrepareProductCommentListModel(ProductCommentSearchModel searchModel)
        {
            var productComments = _productCommentService.List(new ProductCommentListVM
            {
                CommentStatusId = searchModel.CommentStatusId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if(productComments == null)
            {
                return null;
            }

            var model = new ProductCommentListModel().PrepareToGrid(searchModel, productComments, () =>
            {
                return productComments.Select(productComment =>
                {
                    var productCommentModel = productComment.ToModel<ProductCommentModel>();

                    return productCommentModel;
                });
            });

            return model;
        }

        public ProductCommentModel PrepareProductCommentModel(ProductCommentModel model, ProductComment productComment)
        {
            if(productComment != null)
            {
                model = productComment.ToModel<ProductCommentModel>();
            }

            //prepare comment status type
            _baseAdminModelFactory.PrepareCommentStatusType(model.AvailableCommentStatusType);

            return model;
        }

        public ProductCommentSearchModel PrepareProductCommentSearchModel()
        {
            var productCommentSearchModel = new ProductCommentSearchModel();

            //prepare comment status type
            _baseAdminModelFactory.PrepareCommentStatusType(productCommentSearchModel.AvailableCommentStatusType);

            return productCommentSearchModel;
        }

        public StatisticsProductComments PrepareStatisticsProductComments()
        {
            var statistics = new StatisticsProductComments();

            var query = _productCommentService.ProductCommentQuery();

            statistics.Total = query.Count();

            statistics.TotalApprovedComments = query.Where(pc => pc.CommentStatusId == (int)CommentStatusEnum.Open).Count();

            statistics.TotalNotApprovedComments = query.Where(pc => pc.CommentStatusId == (int)CommentStatusEnum.Close).Count();

            return statistics;
        }
        #endregion
    }
}
