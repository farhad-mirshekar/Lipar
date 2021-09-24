using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Factories.Application;
using Lipar.Web.Models.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Components
{
    public class ProductCommentViewComponent : ViewComponent
    {
        #region Ctor
        public ProductCommentViewComponent(IProductCommentService productCommentService
                                         , IProductModelFactory productModelFactory)
        {
            _productCommentService = productCommentService;
            _productModelFactory = productModelFactory;
        }
        #endregion

        #region Fields
        private readonly IProductCommentService _productCommentService;
        private readonly IProductModelFactory _productModelFactory;
        #endregion

        #region Method
        public IViewComponentResult Invoke(IList<ProductCommentListModel> productCommentList)
        {

            return View(productCommentList);
        }
        #endregion
    }
}
