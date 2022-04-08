using Lipar.Web.Framework.Extentions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.TagHelpers
{
    [HtmlTargetElement("lipar-select2", Attributes = MultipleAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class LiparSelect2TagHelper : TagHelper
    {

        #region Ctor
        public LiparSelect2TagHelper(IHtmlHelper htmlHelper
                                   , IHtmlGenerator generator)
        {
            _htmlHelper = htmlHelper;
            _generator = generator;
        }
        #endregion

        #region Fields
        private readonly IHtmlHelper _htmlHelper;
        protected IHtmlGenerator _generator { get; set; }

        private const string ForAttributeName = "asp-for";
        private const string ItemsAttributeName = "asp-items";
        private const string MultipleAttributeName = "asp-multiple";

        [HtmlAttributeName(MultipleAttributeName)]
        public bool Multiple { get; set; }

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Items for a dropdown list
        /// </summary>
        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<SelectListItem> Items { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        #endregion

        #region Methods
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //clear the output
            output.SuppressOutput();

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);
            var htmlAttributes = new Dictionary<string, object>();

            var tagName = For.Name;

            var models = For.Model as List<Guid>;

            htmlAttributes.Add("class", "form-control");

            if (Multiple)
            {
                htmlAttributes.Add("multiple", "multiple");
            }
            //tag detail
            IHtmlContent selectList;

            Items = !Items.Any() ? new List<SelectListItem>() : Items;

            selectList = _htmlHelper.ListBox(tagName, Items, htmlAttributes);

            output.Content.SetHtmlContent(selectList.RenderHtmlContent());

            //script detail
            var script = new TagBuilder("script");
            script.InnerHtml.AppendHtml("$(document).ready(function(){" +
                                         $"$('#{tagName}').select2(" +
                                         ");})");

            output.PostContent.SetHtmlContent(script.ToHtmlString());
        }
        #endregion
    }
}
