using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ProductQuestionModelFactory : IProductQuestionModelFactory
    {
        #region Ctor
        public ProductQuestionModelFactory(IProductQuestionService productQuestionService
                                         , IBaseAdminModelFactory baseAdminModelFactory
                                         , IProductAnswersService productAnswersService)
        {
            _productQuestionService = productQuestionService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _productAnswersService = productAnswersService;
        }
        #endregion

        #region Fields
        private readonly IProductQuestionService _productQuestionService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IProductAnswersService _productAnswersService;
        #endregion

        #region Methods
        public ProductQuestionListModel PrepareProductQuestionListModel(ProductQuestionSearchModel searchModel)
        {
            var productQuestions = _productQuestionService.List(new ProductQuestionListVM
            {
                ProductId = searchModel.ProductId,
                ViewStatusId = searchModel.ViewStatusId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize
            });

            if (productQuestions == null)
            {
                return null;
            }

            var models = new ProductQuestionListModel().PrepareToGrid(searchModel, productQuestions, () =>
            {
                return productQuestions.Select(productQuestion =>
                {
                    var productQuestionModel = productQuestion.ToModel<ProductQuestionModel, Guid>();

                    return productQuestionModel;
                });
            });

            return models;
        }

        public ProductQuestionModel PrepareProductQuestionModel(ProductQuestionModel model, ProductQuestion productQuestion)
        {
            if (productQuestion != null)
            {
                model = productQuestion.ToModel<ProductQuestionModel, Guid>();

                //prepare product answer search model
                PrepareProductAnswersSearchModel(model.ProductAnswersSearchModel, productQuestion);
            }

            //prepare view status
            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatus);
            
            return model;
        }

        public ProductQuestionSearchModel PrepareProductQuestionSearchModel(ProductQuestionSearchModel searchModel, Product product)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            searchModel.ProductId = product?.Id;

            //prepare view status
            _baseAdminModelFactory.PrepareViewStatusType(searchModel.AvailableViewStatus);

            searchModel.SetGridPageSize();

            return searchModel;
        }

        public ProductAnswersListModel PrepareProductAnswersListModel(ProductAnswersSearchModel searchModel)
        {
            var productAnswers = _productAnswersService.List(new ProductAnswersListVM
            {
                ProductQuestionId = searchModel.ProductQuestionId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if (productAnswers == null)
            {
                return null;
            }

            var models = new ProductAnswersListModel().PrepareToGrid(searchModel, productAnswers, () =>
            {
                return productAnswers.Select(productAnswer =>
                {
                    var productAnswerModel = productAnswer.ToModel<ProductAnswersModel, Guid>();

                    return productAnswerModel;
                });
            });

            return models;
        }

        public ProductAnswersModel PrepareProductAnswersModel(ProductAnswersModel model, ProductAnswers productAnswers)
        {
            if(productAnswers != null)
            {
                model = productAnswers.ToModel<ProductAnswersModel,Guid>();
            }

            //prepare view status
            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatus);

            return model;
        }

        public ProductAnswersSearchModel PrepareProductAnswersSearchModel(ProductAnswersSearchModel searchModel, ProductQuestion productQuestion)
        {
            if(searchModel == null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            if(productQuestion == null)
            {
                throw new ArgumentNullException(nameof(productQuestion));
            }

            searchModel.ProductQuestionId = productQuestion.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
