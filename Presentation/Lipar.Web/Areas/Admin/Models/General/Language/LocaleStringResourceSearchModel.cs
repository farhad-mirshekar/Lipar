﻿using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class LocaleStringResourceSearchModel : BaseSearchModel
    {
        public LocaleStringResourceSearchModel()
        {
            AddResourceString = new LocaleStringResourceModel();
        }
        public string ResourceName { get; set; }
        public int LanguageId { get; set; }

        public LocaleStringResourceModel AddResourceString { get; set; }
    }
}
