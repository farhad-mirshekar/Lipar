using Lipar.Web.Models.General;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductModel : BaseEntityModel
    {
        public ProductModel()
        {
            Pictures = new MediaListModel();
            ProductAttributes = new List<ProductAttributeModel>();
            RelatedProducts = new List<RelatedProductModel>();
            Category = new CategoryModel();
            DeliveryDate = new DeliveryDateModel();
            ShippingCost = new ShippingCostModel();
            ProductCommentModel = new ProductCommentModel();
            ProductComments = new List<ProductCommentListModel>();
            ProductQuestions = new List<ProductQuestionModel>();
        }
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
        public CategoryModel Category { get; set; }
        /// <summary>
        /// gets or sets delivery date
        /// </summary>
        public DeliveryDateModel DeliveryDate { get; set; }
        /// <summary>
        /// gets or sets shipping cost
        /// </summary>
        public ShippingCostModel ShippingCost { get; set; }
        /// <summary>
        /// gets or sets updated date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        public MediaListModel Pictures { get; set; }
        /// <summary>
        /// gets or sets product attributes
        /// </summary>
        public IList<ProductAttributeModel> ProductAttributes { get; set; }
        /// <summary>
        /// gets or sets related products
        /// </summary>
        public IList<RelatedProductModel> RelatedProducts { get; set; }
        /// <summary>
        /// gets or sets the all product comment
        /// </summary>
        public IList<ProductCommentListModel> ProductComments { get; set; }
        /// <summary>
        /// gets or sets product comment model
        /// </summary>
        public ProductCommentModel ProductCommentModel { get; set; }
        /// <summary>
        /// gets or sets the all product question
        /// </summary>
        public IList<ProductQuestionModel> ProductQuestions { get; set; }
        #endregion
    }
}
