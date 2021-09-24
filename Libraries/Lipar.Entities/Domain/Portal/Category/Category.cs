using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class Category : BaseEntity
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
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<News> News { get; set; }
        #endregion

    }
}
