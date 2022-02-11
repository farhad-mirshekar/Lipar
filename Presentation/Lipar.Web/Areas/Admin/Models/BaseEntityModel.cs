using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models
{
    public class BaseEntityModel<T> : BaseLiparModel
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
