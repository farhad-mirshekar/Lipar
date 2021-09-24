using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Portal;
using Lipar.Entities.Domain.General;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Core
{
   public class ViewStatus : BaseEntity
    {

        #region Ctor
        public ViewStatus()
        {
            Blogs = new HashSet<Blog>();
            DynamicPages = new HashSet<DynamicPage>();
            DynamicPageDetails = new HashSet<DynamicPageDetail>();
            Galleries = new HashSet<Gallery>();
            News = new HashSet<News>();
            StaticPages = new HashSet<StaticPage>();
            Languages = new HashSet<Language>();
            LocaleStringResources = new HashSet<LocaleStringResource>();
            ActivityLogTypes = new HashSet<ActivityLogType>();
            ProductAnswers = new HashSet<ProductAnswers>();
            ProductQuestions = new HashSet<ProductQuestion>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<DynamicPage> DynamicPages { get; set; }
        public ICollection<DynamicPageDetail> DynamicPageDetails { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<StaticPage> StaticPages { get; set; }
        public ICollection<Language> Languages { get; set; }
        public ICollection<LocaleStringResource> LocaleStringResources { get; set; }
        public ICollection<ActivityLogType> ActivityLogTypes { get; set; }
        public ICollection<ProductAnswers> ProductAnswers { get; set; }
        public ICollection<ProductQuestion> ProductQuestions { get; set; }
        #endregion
    }
}
