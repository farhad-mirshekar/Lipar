using Lipar.Core.Caching;

namespace Lipar.Services.Portal
{
    /// <summary>
    /// lipar portal defaults
    /// </summary>
    public class LiparPortalDefaults
    {
        /// <summary>
        /// load blog settings
        /// </summary>
        public static CacheKey Load_Blog_Settings => new CacheKey("Load-Blog-Settings");
    }
}
