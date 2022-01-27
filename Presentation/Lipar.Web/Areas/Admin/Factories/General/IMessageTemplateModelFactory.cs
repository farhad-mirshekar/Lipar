using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public interface IMessageTemplateModelFactory
    {
        /// <summary>
        /// prepare message template list model
        /// </summary>
        /// <param name="searchModel">message template search model</param>
        /// <returns></returns>
        MessageTemplateListModel PrepareMessageTemplateListModel(MessageTemplateSearchModel searchModel);

        /// <summary>
        /// preapre message template model
        /// </summary>
        /// <param name="model">message template model</param>
        /// <param name="messageTemplate">message template</param>
        /// <returns></returns>
        MessageTemplateModel PrepareMessageTemplateModel(MessageTemplateModel model, MessageTemplate messageTemplate);
    }
}
