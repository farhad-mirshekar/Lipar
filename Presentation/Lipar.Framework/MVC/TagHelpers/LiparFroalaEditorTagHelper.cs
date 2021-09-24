using Lipar.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.TagHelpers
{
    [HtmlTargetElement("lipar-froala-editor", TagStructure = TagStructure.WithoutEndTag)]
    public class LiparFroalaEditorTagHelper : TagHelper
    {
        #region Fields
        private readonly IHtmlHelper _htmlHelper;
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #endregion

        #region Ctor
        public LiparFroalaEditorTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }
        #endregion

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
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

            var tagName = For != null ? For.Name : "";

            var froalaEditor = new FroalaEditorModel
            {
                Name = tagName,
                Content = For.Model != null ? For.Model.ToString() : string.Empty
            };

            output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("Froala.Editor", froalaEditor));

        }


    }
}
