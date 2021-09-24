using Lipar.Core.Caching;

namespace Lipar.Web.Areas.Admin.Infrastructure.Cache
{
    public static class LiparModelCacheDefaults
    {
        public static CacheKey Category_Portal_List_Key => new CacheKey("Category-Portal-List-Key");
        public static CacheKey Command_List_Key => new CacheKey("Command-List-Key");
        public static CacheKey Department_List_Key => new CacheKey("Department-List-Key");
        public static CacheKey Category_Product_List_Key => new CacheKey("Category-Product-List-Key");
    }
}
