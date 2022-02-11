using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Factories.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Models;
using Lipar.Web.Models.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Controllers
{
    public class ProductController : BasePublishController
    {
        #region Ctor
        public ProductController(IProductModelFactory productModelFactory
                               , IProductService productService
                               , ICompareProductService compareProductService
                               , ILocaleStringResourceService localeStringResourceService
                               , IProductCommentService productCommentService
                               , IWorkContext workContext
                               , IProductQuestionService productQuestionService
                               , IHttpContextAccessor httpContextAccessor
                               , IProductAnswersService productAnswersService)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
            _compareProductService = compareProductService;
            _localeStringResourceService = localeStringResourceService;
            _productCommentService = productCommentService;
            _workContext = workContext;
            _productQuestionService = productQuestionService;
            _httpContextAccessor = httpContextAccessor;
            _productAnswersService = productAnswersService;
        }
        #endregion

        #region Fields
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly ICompareProductService _compareProductService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IProductCommentService _productCommentService;
        private readonly IWorkContext _workContext;
        private readonly IProductQuestionService _productQuestionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductAnswersService _productAnswersService;
        #endregion

        #region Product Methods
        [Route("Product/{ProductId:guid}")]
        public IActionResult Detail(Guid ProductId)
        {
            var product = _productService.GetById(ProductId);
            if (product == null || product.RemoverId.HasValue)
            {
                return InvokeHttp404();
            }

            var model = _productModelFactory.PrepareProductModel(new ProductModel(), product, true);

            return View(model);
        }
        #endregion

        #region Compare Product
        /// <summary>
        /// add product in compare list
        /// </summary>
        /// <param name="ProductId">product id</param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult AddProductInCompareList(Guid ProductId)
        {
            var product = _productService.GetById(ProductId);
            if (product == null || product.RemoverId.HasValue || !product.Published)
            {
                return Json(new
                {
                    success = false,
                    message = "No product found with the specified ID"
                });
            }

            _compareProductService.AddProductToCompareList(product.Id);

            return Json(new
            {
                success = true,
                Message = string.Format(_localeStringResourceService.GetResource("Products.ProductHasBeenAddedToCompareList.Link"), Url.RouteUrl("CompareProducts")),
                NotyType = "Info"
            });
        }

        /// <summary>
        /// gets all product for compare
        /// </summary>
        /// <returns></returns>
        public IActionResult CompareProducts()
        {
            var compareProducts = _compareProductService.GetCompareProducts();

            var productModels = new List<ProductModel>();

            foreach (var product in compareProducts)
            {
                var productModel = _productModelFactory.PrepareProductModel(new ProductModel(), product, false);
                productModels.Add(productModel);
            }

            return View(productModels);
        }

        /// <summary>
        ///remove product from compare list 
        /// </summary>
        /// <param name="ProductId">product id</param>
        /// <returns></returns>
        public IActionResult RemoveProductFromCompareList(Guid ProductId)
        {
            var product = _productService.GetById(ProductId);
            if (product == null)
            {
                return RedirectToRoute("Homepage");
            }

            _compareProductService.RemoveProductFromCompareList(ProductId);

            return RedirectToRoute("CompareProducts");
        }

        /// <summary>
        /// clear compare list
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearCompareList()
        {
            _compareProductService.ClearCompareProducts();

            return RedirectToRoute("CompareProducts");
        }
        #endregion

        #region Comment Product
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ProductCommentAdd(ProductCommentModel model)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.Comment.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            if (ModelState.IsValid)
            {
                var product = _productService.GetById(model.ProductId, true);
                if (product == null || product.RemoverId.HasValue)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.Comment.YouMustBeLoggedIn"),
                        NotyType = "error"
                    });
                }

                if (!product.AllowCustomerReviews)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.Comment.CommentStatusType.CommentsAreClosed"),
                        NotyType = "error"
                    });
                }

                //list product comment by user id
                var productComments = _productCommentService.List(new ProductCommentListVM
                {
                    ProductId = model.ProductId,
                    UserId = _workContext.CurrentUser.Id,
                });

                if (productComments.Any(pc => pc.CommentStatusId == (int)CommentStatusEnum.Close))
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.Comment.Error.UserHasADisabledComment"),
                        NotyType = "error"
                    });
                }

                var productComment = new ProductComment
                {
                    ProductId = model.ProductId,
                    UserId = _workContext.CurrentUser.Id,
                    CommentText = model.CommentText.Trim(),
                    CreationDate = CommonHelper.GetCurrentDateTime(),
                    CommentStatusId = (int)CommentStatusEnum.Close,
                    ParentId = model.ParentId,
                };

                _productCommentService.Add(productComment);

                return Json(new ResultViewModel
                {
                    Success = true,
                    Message = _localeStringResourceService.GetResource("Web.Comment.CreateText"),
                    NotyType = "success"
                });
            }

            return Json(new
            {
                Success = false,
                Message = _localeStringResourceService.GetResource("Web.Comment.YouMustBeLoggedIn"),
                NotyType = "error"
            });
        }
        #endregion

        #region Product Question-Answer
        public IActionResult ProductQuestionAdd(Guid productId)
        {
            var product = _productService.GetById(productId, true);
            if (product == null || product.RemoverId.HasValue)
            {
                return null;
            }
            var model = _productModelFactory.PrepareProductQuestionModel(new ProductQuestionModel(), product);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductQuestionAdd(ProductQuestionModel model)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.ProductQuestion.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            if (ModelState.IsValid)
            {
                var product = _productService.GetById(model.ProductId, true);
                if (product == null || product.RemoverId.HasValue)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.Product.Notfound"),
                        NotyType = "error"
                    });
                }

                var productQuestions = _productQuestionService.List(new ProductQuestionListVM
                {
                    ProductId = model.ProductId,
                    UserId = _workContext.CurrentUser.Id,
                    ViewStatusId = (int)ViewStatusEnum.InActive,
                });

                if (productQuestions.Count > 0)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.ProductQuestion.Error.UserHasADisabledComment"),
                        NotyType = "error"
                    });
                }

                var productQuestion = new ProductQuestion();
                productQuestion.ProductId = model.ProductId;
                productQuestion.UserId = _workContext.CurrentUser.Id;
                productQuestion.QuestionText = model.QuestionText;
                productQuestion.ViewStatusId = (int)ViewStatusEnum.InActive;

                _productQuestionService.Add(productQuestion);

                return Json(new ResultViewModel
                {
                    Success = true,
                    Message = _localeStringResourceService.GetResource("Web.ProductQuestion.CreateQuestion"),
                    NotyType = "success",
                    Clear = true,
                    DivName = "#js-product-question-create"
                });
            }

            return Json(new ResultViewModel
            {
                Success = false,
                Message = _localeStringResourceService.GetResource("Web.ProductQuestion.YouMustBeLoggedIn"),
                NotyType = "error",
            });
        }

        public IActionResult ProductAnswerAdd(Guid questionId)
        {
            if(questionId == Guid.Empty)
            {
                return NotFound();
            }

            var question = _productQuestionService.GetById(questionId, true);

            if(question == null)
            {
                return NotFound();
            }

            var model = _productModelFactory.PrepareProductAnswersModel(new ProductAnswersModel(), question);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductAnswerAdd(ProductAnswersModel model)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.ProductQuestion.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            if (ModelState.IsValid)
            {
                var question = _productQuestionService.GetById(model.ProductQuestionId, true);

                if (question == null)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.Product.Notfound"),
                        NotyType = "error"
                    });
                }

                var productAnswers = _productAnswersService.List(new ProductAnswersListVM
                {
                    ProductQuestionId = model.ProductQuestionId,
                    UserId = _workContext.CurrentUser.Id,
                    ViewStatusId = (int)ViewStatusEnum.InActive,
                });

                if (productAnswers.Count > 0)
                {
                    return Json(new ResultViewModel
                    {
                        Success = false,
                        Message = _localeStringResourceService.GetResource("Web.ProductQuestion.Error.UserHasADisabledComment"),
                        NotyType = "error"
                    });
                }

                var productAnswer = new ProductAnswers();
                productAnswer.ProductQuestionId = model.ProductQuestionId;
                productAnswer.UserId = _workContext.CurrentUser.Id;
                productAnswer.AnswerText = model.AnswerText;
                productAnswer.ViewStatusId = (int)ViewStatusEnum.InActive;

                _productAnswersService.Add(productAnswer);

                return Json(new ResultViewModel
                {
                    Success = true,
                    Message = _localeStringResourceService.GetResource("Web.ProductQuestion.CreateQuestion"),
                    NotyType = "success",
                    Clear = true,
                    DivName = "#js-product-answer-create"
                });
            }

            return Json(new ResultViewModel
            {
                Success = false,
                Message = _localeStringResourceService.GetResource("Web.ProductQuestion.YouMustBeLoggedIn"),
                NotyType = "error",
            });
        }
        #endregion


    }
}
