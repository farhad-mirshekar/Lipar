using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    public class ProductComment : BaseEntity<Guid>
    {
        #region Ctor
        public ProductComment()
        {
            Children = new HashSet<ProductComment>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the comment text
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// gets or sets the replay text for admin
        /// </summary>
        public string ReplyText { get; set; }
        /// <summary>
        /// gets or sets the comment status type
        /// </summary>
        public int CommentStatusId { get; set; }
        /// <summary>
        /// gets or sets the parent 
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// gets or sets the product
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// gets or sets the user creator
        /// </summary>
        public Guid UserId { get; set; }
        #endregion

        #region Navigations
        public ProductComment Parent { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public ICollection<ProductComment> Children { get; set; }
        #endregion
    }
}
