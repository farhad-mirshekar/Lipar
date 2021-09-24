using Lipar.Core.Caching;

namespace Lipar.Services.General
{
    public static class LiparPublicDefaults
    {
        public static CacheKey Locale_String_Resource_Get_All_Resource_Values => new CacheKey("Locale-String-Resource-Get-All-Resource-Values");
        public static string ImagePath => "images";
        public static string ImageFroalaPath => "images/editor";
        public static CacheKey Activity_Log_Type_Get_All => new CacheKey("Activity-Log-Type-Get-All");
        public static CacheKey Settings_All_As_Dictionary => new CacheKey("Settings-All-As-Dictionary");
    }
}
