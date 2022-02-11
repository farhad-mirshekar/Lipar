using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class Menu : BaseEntity<Guid>
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
        #endregion

        #region Navigations
        public Language Language { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        #endregion
    }
}
