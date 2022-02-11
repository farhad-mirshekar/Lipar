using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class FaqSearchModel : BaseSearchModel
    {
        #region Fields
        public Guid FaqGroupId { get; set; }
        #endregion
    }
}
