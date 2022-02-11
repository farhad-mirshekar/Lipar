using Lipar.Entities.Domain.Core;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Department : BaseEntity<Guid>
    {
        #region Ctor
        public Department()
        {
            Positions = new HashSet<Position>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// gets or sets the postal code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// gets or sets the phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// gets or sets the code phone
        /// </summary>
        public string CodePhone { get; set; }
        /// <summary>
        /// gets or the set enable type id
        /// </summary>
        public int EnabledTypeId { get; set; }
        public int DepartmentTypeId { get; set; }
        public Guid CenterId { get; set; }
        public Guid? ParentId { get; set; }
        #endregion

        #region Navigations
        public Department Parent { get; set; }
        public  ICollection<Department> Children { get; set; }
        public  Center Center { get; set; }
        public DepartmentType DepartmentType { get; set; }
        public EnabledType EnabledType { get; set; }
        public ICollection<Position> Positions { get; set; }
        #endregion
    }
}
