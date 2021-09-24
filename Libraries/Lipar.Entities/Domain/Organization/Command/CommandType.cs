using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
   public class CommandType : BaseEntity
    {
        #region Ctor
        public CommandType()
        {
            Commands = new HashSet<Command>();
        }
        #endregion

        #region Fields
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Command> Commands { get; set; }
        #endregion
    }
}
