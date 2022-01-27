using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    /// <summary>
    /// email account model factory
    /// </summary>
    public interface IEmailAccountModelFactory
    {
        /// <summary>
        /// prepare email account list model
        /// </summary>
        /// <param name="searchModel">email account search model</param>
        /// <returns></returns>
        EmailAccountListModel PrepareEmailAccountListModel(EmailAccountSearchModel searchModel);

        /// <summary>
        /// prepare email account model
        /// </summary>
        /// <param name="model">email account model</param>
        /// <param name="emailAccount">email account</param>
        /// <returns></returns>
        EmailAccountModel PrepareEmailAccountModel(EmailAccountModel model, EmailAccount emailAccount);
    }
}
