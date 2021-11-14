using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class OrderSettingModel :BaseEntityModel, ISettingsModel
    {
        public decimal? ShoppingCartRate { get; set; }
    }
}
