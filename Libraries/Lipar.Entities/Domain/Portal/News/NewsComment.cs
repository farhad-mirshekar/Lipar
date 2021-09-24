using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class NewsComment : BaseEntity
    {
        #region Fields
        public string Body { get; set; }
        public int? ParentId { get; set; }
        public int CommentStatusId { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public News News { get; set; }
        public NewsComment Parent { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public User User { get; set; }
        public User Remover { get; set; }
        public ICollection<NewsComment> Children { get; set; }
        #endregion
    }
}
