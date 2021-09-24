using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class ContactUsService : IContactUsService
    {
        #region Fields
        private readonly IRepository<ContactUs> _repository;
        #endregion

        #region Ctor
        public ContactUsService(IRepository<ContactUs> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(ContactUs model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(ContactUs model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public ContactUs GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _repository.GetById(Id);
        }

        public IPagedList<ContactUs> List(ContactUsListVM listVM)
        {
            var query = _repository.TableNoTracking;
            if(listVM.ContactUsTypeId.HasValue && listVM.ContactUsTypeId.Value != 0)
            {
                query = query.Where(c => c.ContactUsTypeId == listVM.ContactUsTypeId);
            }

            var models = new PagedList<ContactUs>(query, listVM.PageIndex, listVM.PageSize);
            return models;
        }
        #endregion
    }
}
