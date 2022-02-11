using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Financial
{
    public class BankModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string PaymentUri { get; set; }
        public string RedirectUri { get; set; }
        public BankPortModel BankPort { get; set; }
    }
}
