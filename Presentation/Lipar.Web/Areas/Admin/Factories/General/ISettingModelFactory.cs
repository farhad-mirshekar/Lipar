using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
   public interface ISettingModelFactory
    {
        BlogSettingModel PrepareBlogSettingModel();

        /// <summary>
        /// prepare order setting model
        /// </summary>
        /// <returns></returns>
        OrderSettingModel PrepareOrderSettingModel();

        /// <summary>
        /// prepare common setting model
        /// </summary>
        /// <returns></returns>
        CommonSettingModel PrepareCommonSettingModel();

        /// <summary>
        /// prepare security setting model
        /// </summary>
        /// <returns></returns>
        SecuritySettingModel PrepareSecuritySettingModel();
    }
}
