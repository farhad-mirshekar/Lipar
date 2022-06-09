using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Models.Organization
{
    public class UserAddressListModel
    {
        public UserAddressListModel()
        {
            AvailableUserAddress = new List<UserAddressModel>();
        }
        public IEnumerable<UserAddressModel> AvailableUserAddress { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
