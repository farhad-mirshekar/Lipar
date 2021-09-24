using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class Menu : BaseEntity
    {
        #region Ctor
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int UserId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public Language Language { get; set; }
        public User User { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        #endregion
    }
}
