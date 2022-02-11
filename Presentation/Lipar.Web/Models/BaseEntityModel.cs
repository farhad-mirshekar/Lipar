using System;

namespace Lipar.Web.Models
{
    public class BaseEntityModel<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
