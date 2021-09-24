using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class News : BaseEntity
    {
        #region Ctor
        public News()
        {
            NewsComments = new HashSet<NewsComment>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public string ReadingTime { get; set; }
        public int CommentStatusId { get; set; }
        public int VisitedCount { get; set; }
        public int ViewStatusId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int? LanguageId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region navigations
        public Category Category { get; set; }
        public User User { get; set; }
        public User Remover { get; set; }
        public Language Language { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public ViewStatus ViewStatus { get; set; }
        public ICollection<NewsComment> NewsComments { get; set; }
        #endregion
    }
}
