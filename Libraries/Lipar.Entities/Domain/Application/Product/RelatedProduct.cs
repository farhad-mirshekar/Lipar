namespace Lipar.Entities.Domain.Application
{
    public class RelatedProduct : BaseEntity
    {
        /// <summary>
        /// gets or sets the first product
        /// </summary>
        public int ProductId1 { get; set; }
        /// <summary>
        /// gets or sets the second product
        /// </summary>
        public int ProductId2 { get; set; }
        /// <summary>
        /// gets or sets the priority
        /// </summary>
        public int Priority { get; set; }
    }
}
