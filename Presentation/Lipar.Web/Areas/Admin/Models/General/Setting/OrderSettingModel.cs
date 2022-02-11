using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class OrderSettingModel :BaseEntityModel<Guid>, ISettingsModel
    {
        public decimal? ShoppingCartRate { get; set; }
    }
}
