using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class DiscountType:BaseEntity<int>
    {
        #region Ctor
        public DiscountType()
        {
            Products = new HashSet<Product>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
