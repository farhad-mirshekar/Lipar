using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class MenuItem : BaseEntity
    {
        #region Fields
        public string Name { get; set; }
        public int ViewStatusId { get; set; }
        public int? ParentId { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; }
        public int Priority { get; set; }
        public string Parameters { get; set; }
        public int? RemoverID { get; set; }
        public DateTime? RemoveDate { get; set; }
        public int MenuId { get; set; }
        #endregion

        #region Navigations
        public Menu Menu { get; set; }
        public MenuItem Parent { get; set; }
        public ICollection<MenuItem> Children { get; set; }
        #endregion
    }
}
