using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Organization
{
    public class UserAddressModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public UserAddressModel()
        {
            AvailableCountries = new List<SelectListItem>();
        }
        #endregion

        public string PostalCode { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }

        #region Select
        public IList<SelectListItem> AvailableCountries { get; set; }
        #endregion
    }
}
