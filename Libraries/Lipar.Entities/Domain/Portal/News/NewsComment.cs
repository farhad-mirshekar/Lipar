using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class NewsComment : BaseEntity<Guid>
    {
        #region Fields
        public string Body { get; set; }
        public Guid? ParentId { get; set; }
        public int CommentStatusId { get; set; }
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
        #endregion

        #region Navigations
        public News News { get; set; }
        public NewsComment Parent { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public User User { get; set; }
        public ICollection<NewsComment> Children { get; set; }
        #endregion
    }
}
