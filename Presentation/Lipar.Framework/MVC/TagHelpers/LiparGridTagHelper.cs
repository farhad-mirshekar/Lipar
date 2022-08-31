using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.TagHelpers
{
    [HtmlTargetElement("lipar-grid" , TagStructure = TagStructure.NormalOrSelfClosing)]
    public class LiparGridTagHelper : TagHelper
    {
        #region const property

        private const string GridNameAttributeName = "asp-grid-name";
        private const string GridIdAttributeName = "asp-grid-id";
        private const string GridUrlReadAttributeName = "asp-grid-read-url";
        private const string GridColumnsAttributeName = "asp-columns";
        private const string GridCommandsAttributeName = "asp-commands";
        private readonly IHtmlHelper _htmlHelper;
        protected IHtmlGenerator _generator { get; set; }
        #endregion

        #region Fields

        [HtmlAttributeName(GridNameAttributeName)]
        public string Name { get; set; }

        [HtmlAttributeName(GridIdAttributeName)]
        public string Id { get; set; }

        [HtmlAttributeName(GridUrlReadAttributeName)]
        public string ReadUrl { get; set; }

        [HtmlAttributeName(GridColumnsAttributeName)]
        public List<ColumnInfo> Columns { get; set; }

        [HtmlAttributeName(GridCommandsAttributeName)]
        public List<Command> Commands { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #endregion

        public LiparGridTagHelper(IHtmlGenerator generator
                                , IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _generator = generator;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            var grid = new Grid
            {
                Id = Id,
                Name = Name,
                ReadUrl = ReadUrl,
                Columns = Columns,
                Commands = Commands
            };

            output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("LiparGrid" , grid));

        }
    }

    public class Grid
    {
        public Grid()
        {
            Columns = new List<ColumnInfo>();
            Commands = new List<Command>();
        }
        public List<ColumnInfo> Columns { get; set; }
        public List<Command> Commands { get; set; }
        public string ReadUrl { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class ColumnInfo
    {
        public ColumnInfo()
        {
            Visible = true;
        }
        public string Name { get; set; }
        public string HeaderText { get; set; }
        public bool IsKey { get; set; }
        public bool Visible { get; set; }
    }

    public class Command
    {
        public Command()
        {
            Visible = true;
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Visible { get; set; }
        public CommandType CommandType { get; set; }
    }
    public enum CommandType
    {
        Edit = 1,
        Delete
    }
}
