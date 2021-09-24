using Lipar.Web.Factories.Portal;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    public class BlogSideViewComponent : ViewComponent
    {
        private readonly IBlogModelFactory _blogModelFactory;
        public BlogSideViewComponent(IBlogModelFactory blogModelFactory)
        {
            _blogModelFactory = blogModelFactory;
        }
        public IViewComponentResult Invoke()
        {
            var model = _blogModelFactory.PrepareBlogList(PageIndex:1);

            return View(model);
        }
    }
}
