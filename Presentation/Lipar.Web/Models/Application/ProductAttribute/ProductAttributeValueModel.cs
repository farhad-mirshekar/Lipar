﻿namespace Lipar.Web.Models.Application
{
    public class ProductAttributeValueModel : BaseEntityModel
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool IsPreSelected { get; set; }
    }
}
