using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public interface IFaqGroupModelFactory
    {
        /// <summary>
        /// prepare faq group model for create or update
        /// </summary>
        /// <param name="model">faq group model</param>
        /// <param name="faqGroup">faq group</param>
        /// <returns></returns>
        FaqGroupModel PrepareFaqGroupModel(FaqGroupModel model, FaqGroup faqGroup);
        /// <summary>
        /// prepare faq group for show cartable
        /// </summary>
        /// <param name="searchModel">search model faq group</param>
        /// <returns></returns>
        FaqGroupListModel PrepareFaqGroupListModel(FaqGroupSearchModel searchModel);
        /// <summary>
        /// prepare faq model for create or update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="faq"></param>
        /// <returns></returns>
        FaqModel PrepareFaqModel(FaqModel model, Faq faq);
        /// <summary>
        /// prepare faq for show cartable
        /// </summary>
        /// <param name="searchModel">search model faq</param>
        /// <returns></returns>
        FaqListModel PrepareFaqListModel(FaqSearchModel searchModel);
    }
}
