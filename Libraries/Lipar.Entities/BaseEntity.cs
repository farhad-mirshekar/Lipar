using System;

namespace Lipar.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
