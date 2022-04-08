using Lipar.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableDeliveryDate = new List<SelectListItem>();
            AvailableDiscountType = new List<SelectListItem>();
            AvailableShippingCost = new List<SelectListItem>();
            AvailableProductTags = new List<SelectListItem>();
            ProductTags = new List<Guid>();
            Published = true;
            ProductAttributeMappingSearchModel = new ProductAttributeMappingSearchModel();
            ProductMediaSearchModel = new ProductMediaSearchModel();
            RelatedProductSearchModel = new RelatedProductSearchModel();
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
        public Guid UserId { get; set; }
        /// <summary>
        /// gets or sets category
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// gets or sets shipping cost
        /// </summary>
        public Guid? ShippingCostId { get; set; }
        /// <summary>
        /// gets or sets delivery date
        /// </summary>
        public Guid? DeliveryDateId { get; set; }

        public int NumberProductQuestions { get; set; }

        public IList<Guid> ProductTags { get; set; }

        public ProductAttributeMappingSearchModel ProductAttributeMappingSearchModel { get; set; }
        public ProductMediaSearchModel ProductMediaSearchModel { get; set; }
        /// <summary>
        /// gets or sets the related product search
        /// </summary>
        public RelatedProductSearchModel RelatedProductSearchModel { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableDiscountType { get; set; }
        public IList<SelectListItem> AvailableShippingCost { get; set; }
        public IList<SelectListItem> AvailableDeliveryDate { get; set; }
        public IList<SelectListItem> AvailableProductTags { get; set; }
        #endregion

    }
}
