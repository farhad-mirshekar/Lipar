using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class Category : BaseEntity<Guid>
    {
        #region Ctor
        public Category()
        {
            Blogs = new HashSet<Blog>();
            Children = new HashSet<Category>();
            News = new HashSet<News>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<News> News { get; set; }
        #endregion

    }
}
