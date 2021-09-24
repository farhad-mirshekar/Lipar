using Lipar.Web.Framework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.TagHelpers
{
    [HtmlTargetElement("lipar-pagination" , TagStructure =TagStructure.WithoutEndTag)]
    public class LiparPaginationTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;
        public PagingInfo PagingInfo { get; set; }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public LiparPaginationTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "pagination");

            output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("Paging" , PagingInfo));
        }
    }
}
