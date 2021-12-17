using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using System.Collections.Generic;
using Application = Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Financial;

namespace Lipar.Entities.Domain.Core
{
   public class EnabledType : BaseEntity
    {
        #region Ctor
        public EnabledType()
        {
            Departments = new HashSet<Department>();
            Users = new HashSet<User>();
            UrlRecords = new HashSet<UrlRecord>();
            Centers = new HashSet<Center>();
            DeliveryDates = new HashSet<DeliveryDate>();
            shippingCosts = new HashSet<ShippingCost>();
            ApplicationCategories = new HashSet<Application.Category>();
            Banks = new HashSet<Bank>();
            BankPorts = new HashSet<BankPort>();
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
        public ICollection<User> Users { get; set; }
        public ICollection<UrlRecord> UrlRecords { get; set; }
        public ICollection<Center> Centers { get; set; }
        public ICollection<DeliveryDate> DeliveryDates { get; set; }
        public ICollection<ShippingCost> shippingCosts { get; set; }
        public ICollection<Application.Category> ApplicationCategories { get; set; }
        public ICollection<Bank> Banks { get; set; }
        public ICollection<BankPort> BankPorts { get; set; }
        #endregion
    }
}
