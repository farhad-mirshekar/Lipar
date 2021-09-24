using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Portal;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class Language : BaseEntity
    {
        #region Ctor
        public Language()
        {
            Menus = new HashSet<Menu>();
            UrlRecords = new HashSet<UrlRecord>();
            Blogs = new HashSet<Blog>();
            News = new HashSet<News>();
            LocaleStringResources = new HashSet<LocaleStringResource>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public int LanguageCultureId { get; set; }
        public string UniqueSeoCode { get; set; }
        public int ViewStatusId { get; set; }
        public int UserId { get; set; }
        #endregion

        #region navigations
        public User User { get; set; }
        public ViewStatus ViewStatus { get; set; }
        public LanguageCulture LanguageCulture { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<UrlRecord> UrlRecords { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<LocaleStringResource> LocaleStringResources { get; set; }
        #endregion
    }
}
