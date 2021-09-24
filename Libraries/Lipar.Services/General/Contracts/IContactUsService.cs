using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
   public interface IContactUsService
    {
        /// <summary>
        /// add contact us method
        /// </summary>
        /// <param name="model"></param>
        void Add(ContactUs model);
        /// <summary>
        /// delete contact us method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ContactUs model);
        /// <summary>
        /// retrieve contact us by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ContactUs GetById(int Id);
        /// <summary>
        /// list contact us method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ContactUs> List(ContactUsListVM listVM);
    }
}
