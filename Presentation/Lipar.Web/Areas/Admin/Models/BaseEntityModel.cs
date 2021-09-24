using Lipar.Core.Common;
using Nop.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models
{
    public class BaseEntityModel : BaseLiparModel
    {
        public BaseEntityModel()
        {
            CreationDate = CommonHelper.GetCurrentDateTime();
        }
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
