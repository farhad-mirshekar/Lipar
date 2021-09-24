using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product class
    /// </summary>
   public class Product : BaseEntity
    {
        #region Ctor
        public Product()
        {
            CallForPrice = false;
            IsDownload = false;
            SpecialOffer = false;
            ShowOnHomePage = false;
            AllowCustomerReviews = false;
            Published = true;
            ProductComments = new HashSet<ProductComment>();
            ProductQuestions = new HashSet<ProductQuestion>();
            ProductAttributeMappings = new HashSet<ProductAttributeMapping>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets ot sets the name product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }
        /// <summary>
        /// gets or sets the show on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }
        /// <summary>
        /// gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }
        /// <summary>
        /// gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        /// gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }
        /// <summary>
        /// gets or sets the allow customer reviews
        /// </summary>
        public bool AllowCustomerReviews { get; set; }
        /// <summary>
        /// gets or sets the call for price
        /// </summary>
        public bool CallForPrice { get; set; }
        /// <summary>
        /// gets or sets the price product
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// gets or sets the special offer product
        /// </summary>
        public bool SpecialOffer { get; set; }
        /// <summary>
        /// gets or sets the discount
        /// </summary>
        public decimal? Discount { get; set; }
        /// <summary>
        /// gets or sets the discount type
        /// </summary>
        public int? DiscountTypeId { get; set; }
        /// <summary>
        /// gets or sets the weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// gets or sets the width
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        /// gets or sets the length
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        /// gets or sets the height
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        /// gets or sets the stock quantity
        /// </summary>
        public int StockQuantity { get; set; }
        /// <summary>
        /// gets or sets the is download
        /// </summary>
        public bool IsDownload { get; set; }
        /// <summary>
        /// gets or sets the published
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        /// gets or sets user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// gets or sets category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// gets or sets update date product
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// gets or sets shipping cost
        /// </summary>
        public int? ShippingCostId { get; set; }
        /// <summary>
        /// gets or sets delivery date
        /// </summary>
        public int? DeliveryDateId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations

        public Category Category { get; set; }
        public DeliveryDate DeliveryDate { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public DiscountType DiscountType { get; set; }
        public User User { get; set; }
        public User Remover { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductQuestion> ProductQuestions { get; set; }
        public ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        #endregion
    }
}
