using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
   public interface IContactUsModelFactory
    {
        /// <summary>
        /// prepare contact us model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="contactUs"></param>
        /// <returns></returns>
        ContactUsModel PrepareContactUsModel(ContactUsModel model, ContactUs contactUs);
        /// <summary>
        /// prepare contact us list model
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        ContactUsListModel PrepareContactUsListModel(ContactUsSearchModel searchModel);
        /// <summary>
        /// prepare contact us search model
        /// </summary>
        /// <returns></returns>
        ContactUsSearchModel PrepareContactUsSearchModel();
    }
}
