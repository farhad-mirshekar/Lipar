using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// message template service
    /// </summary>
    public interface IMessageTemplateService
    {
        /// <summary>
        /// add message template method
        /// </summary>
        /// <param name="model">message template</param>
        void Add(MessageTemplate model);

        /// <summary>
        /// edit message template method
        /// </summary>
        /// <param name="model">message template</param>
        void Edit(MessageTemplate model);

        /// <summary>
        /// delete message template method
        /// </summary>
        /// <param name="model">bank</param>
        void Delete(MessageTemplate model);

        /// <summary>
        /// retrieve message template by id method
        /// </summary>
        /// <param name="id">message template id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        MessageTemplate GetById(int id, bool noTracking = false);

        /// <summary>
        /// list message template method
        /// </summary>
        /// <param name="listVM">message template list view model</param>
        /// <returns></returns>
        IPagedList<MessageTemplate> List(MessageTemplateListVM listVM);

        /// <summary>
        /// get message template by name method
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        MessageTemplate GetMessageTemplateByName(string name);
    }
}
