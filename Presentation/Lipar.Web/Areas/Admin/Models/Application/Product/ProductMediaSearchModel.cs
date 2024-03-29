﻿using System;
using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductMediaSearchModel : BaseSearchModel
    {
        public ProductMediaSearchModel()
        {
            AddProductMediaModel = new ProductMediaModel();
        }
        public Guid ProductId { get; set; }
        public ProductMediaModel AddProductMediaModel { get; set; }
    }
}
