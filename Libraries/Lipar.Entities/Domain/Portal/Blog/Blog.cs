using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class Blog : BaseEntity<Guid>
    {
        #region Ctor
        public Blog()
        {
            BlogMedias = new HashSet<BlogMedia>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public int CommentStatusId { get; set; }
        public int VisitedCount { get; set; }
        public int ViewStatusId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string ReadingTime { get; set; }
        public int? LanguageId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region navigations
        public Language Language { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public ViewStatus ViewStatus { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<BlogMedia> BlogMedias { get; set; }
        #endregion

    }
}
