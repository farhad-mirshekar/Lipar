using Lipar.Entities.Domain.Core;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Center : BaseEntity
    {
        #region Ctor
        public Center()
        {
            Departments = new HashSet<Department>();
            Positions = new HashSet<Position>();
            Roles = new HashSet<Role>();
        }
        #endregion

        #region Fields
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int EnabledTypeId { get; set; }
        public string Url { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<Role> Roles { get; set; }
        #endregion
    }
}
