using System.Collections;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
   public class DepartmentType : BaseEntity
    {
        #region Ctor
        public DepartmentType()
        {
            Departments = new HashSet<Department>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<Department> Departments { get; set; }
        #endregion
    }
}
