using System;

namespace Lipar.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
