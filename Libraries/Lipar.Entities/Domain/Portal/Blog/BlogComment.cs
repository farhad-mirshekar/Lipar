using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class BlogComment : BaseEntity<Guid>
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
        public Guid UserId { get; set; }
        public Guid BlogId { get; set; }
        public Guid? ParentId { get; set; }
        #endregion

        #region Navigations
        public Blog Blog { get; set; }
        public BlogComment Parent { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public User User { get; set; }
        public ICollection<BlogComment> Children { get; set; }
        #endregion
    }
}
