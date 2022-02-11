using System;

namespace Lipar.Entities.Domain.Application
{
    public class RelatedProduct : BaseEntity<Guid>
    {
        /// <summary>
        /// gets or sets the first product
        /// </summary>
        public Guid ProductId1 { get; set; }
        /// <summary>
        /// gets or sets the second product
        /// </summary>
        public Guid ProductId2 { get; set; }
        /// <summary>
        /// gets or sets the priority
        /// </summary>
        public int Priority { get; set; }
    }
}
