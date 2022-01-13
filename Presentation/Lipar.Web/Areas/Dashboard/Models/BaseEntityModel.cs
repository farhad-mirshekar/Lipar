using Lipar.Core.Common;
using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Dashboard.Models
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
