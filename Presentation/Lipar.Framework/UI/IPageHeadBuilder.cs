namespace Lipar.Web.Framework.UI
{
   public interface IPageHeadBuilder
    {
        /// <summary>
        /// save active menu for show
        /// </summary>
        /// <param name="systemName"></param>
        void SetActiveMenuItemSystemName(string systemName);
        /// <summary>
        /// get active menu
        /// </summary>
        /// <returns></returns>
        string GetActiveMenuItemSystemName();
        /// <summary>
        /// add meta description
        /// </summary>
        /// <param name="metaDescription"></param>
        void AddMetaDescription(string metaDescription);
        /// <summary>
        /// generate meta description header
        /// </summary>
        /// <returns></returns>
        string GenerateMetaDescription();
        /// <summary>
        /// append meta description header
        /// </summary>
        /// <param name="metaDescription"></param>
        void AppendMetaDescription(string metaDescription);
    }
}
