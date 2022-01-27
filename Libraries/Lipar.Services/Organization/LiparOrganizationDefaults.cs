using Lipar.Core.Caching;

namespace Lipar.Services.Organization
{
    public static class LiparOrganizationDefaults
    {
        public static CacheKey Command_All_List => new CacheKey("Command-All-List");
        /// <summary>
        /// Gets a name of generic attribute to store the value of 'PasswordRecoveryToken'
        /// </summary>
        public static string PasswordRecoveryTokenAttribute => "PasswordRecoveryToken";

        /// <summary>
        /// Gets a name of generic attribute to store the value of 'PasswordRecoveryTokenDateGenerated'
        /// </summary>
        public static string PasswordRecoveryTokenDateGeneratedAttribute => "PasswordRecoveryTokenDateGenerated";
    }
}
