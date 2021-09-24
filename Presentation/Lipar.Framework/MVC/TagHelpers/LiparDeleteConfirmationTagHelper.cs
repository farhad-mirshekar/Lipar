using Lipar.Web.Framework.Extentions;
using Lipar.Web.Framework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.TagHelpers
{
    [HtmlTargetElement("lipar-delete-confirmation", Attributes = ModelIdAttributeName + "," + ButtonIdAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class LiparDeleteConfirmationTagHelper : TagHelper
    {
        #region Fields
        private const string ModelIdAttributeName = "asp-model-id";
        private const string ButtonIdAttributeName = "asp-button-id";
        private const string ActionAttributeName = "asp-action";

        private readonly IHtmlHelper _htmlHelper;

        protected IHtmlGenerator _generator { get; set; }

        [HtmlAttributeName(ModelIdAttributeName)]
        public string ModelId { get; set; }

        [HtmlAttributeName(ButtonIdAttributeName)]
        public string ButtonId { get; set; }

        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        #endregion

        #region Ctor
        public LiparDeleteConfirmationTagHelper(IHtmlGenerator generator
                                              , IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _generator = generator;
        }
        #endregion

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            if (string.IsNullOrEmpty(Action))
                Action = "Delete";

            var modelName = _htmlHelper.ViewData.ModelMetadata.ModelType.Name.ToLower();
            if (!string.IsNullOrEmpty(Action))
                modelName += "-" + Action;

            var modalId = new HtmlString(modelName + "-delete-confirmation").ToHtmlString();

            var deleteConfirmationModel = new DeleteConfirmationModel
            {
                Id = ModelId,
                ControllerName = _htmlHelper.ViewContext.RouteData.Values["controller"].ToString(),
                ActionName = Action,
                WindowId = modalId
            };

            //tag details
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("id", modalId);
            output.Attributes.Add("class", "modal fade");
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.Add("role", "dialog");
            output.Attributes.Add("aria-labelledby", $"{modalId}-title");
            output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("Delete", deleteConfirmationModel));

            //modal script
            var script = new TagBuilder("script");
            script.InnerHtml.AppendHtml("$(document).ready(function () {" +
                                        $"$('#{ButtonId}').attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{modalId}\")" + "});");

            output.PostContent.SetHtmlContent(script.RenderHtmlContent());
        }
    }
}
