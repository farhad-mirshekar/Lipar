using Lipar.Web.Areas.Admin.Factories.Application;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Components
{
    public class StatisticsProductCommentsViewComponent :ViewComponent
    {
        public StatisticsProductCommentsViewComponent(IProductCommentModelFactory productCommentModelFactory)
        {
            _productCommentModelFactory = productCommentModelFactory;
        }

        private readonly IProductCommentModelFactory _productCommentModelFactory;
        public IViewComponentResult Invoke()
        {
            var statistics = _productCommentModelFactory.PrepareStatisticsProductComments();

            return View(statistics);
        }
    }
}
