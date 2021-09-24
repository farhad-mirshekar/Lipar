namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product_media_mapping
    /// </summary>
   public class ProductMedia : BaseEntity
    {
        /// <summary>
        /// gets or sets the product id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// gets or sets the media id
        /// </summary>
        public int MediaId { get; set; }
        /// <summary>
        /// gets or sets the priority
        /// </summary>
        public int Priority { get; set; }
    }
}
