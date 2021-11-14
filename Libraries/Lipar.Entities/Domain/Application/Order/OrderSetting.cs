using Lipar.Entities.Configuration;

namespace Lipar.Entities.Domain.Application
{
   public class OrderSetting : ISettings
    {
        /// <summary>
        /// shopping cart rate
        /// </summary>
        public decimal? ShoppingCartRate { get; set; }
    }
}
