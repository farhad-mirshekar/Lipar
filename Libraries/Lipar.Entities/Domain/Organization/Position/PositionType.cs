using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
   public class PositionType : BaseEntity
    {
        #region Ctor
        public PositionType()
        {
            Positions = new HashSet<Position>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Position> Positions { get; set; }
        #endregion
    }
}
