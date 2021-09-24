using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class BlogComment : BaseEntity
    {
        #region Ctor
        public BlogComment()
        {
            Children = new HashSet<BlogComment>();
        }
        #endregion

        #region Fields
        public string Body { get; set; }
        public int CommentStatusId { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        public int? ParentId { get; set; }
        #endregion

        #region Navigations
        public Blog Blog { get; set; }
        public BlogComment Parent { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public User User { get; set; }
        public User Remover { get; set; }
        public ICollection<BlogComment> Children { get; set; }
        #endregion
    }
}
