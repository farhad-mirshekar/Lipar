using Lipar.Core.Common;
using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Dashboard.Models
{
    public class BaseEntityModel : BaseLiparModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
