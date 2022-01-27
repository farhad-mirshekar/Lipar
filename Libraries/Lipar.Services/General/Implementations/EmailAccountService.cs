using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class EmailAccountService : IEmailAccountService
    {
        #region Ctor
        public EmailAccountService(IRepository<EmailAccount> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<EmailAccount> _repository;
        #endregion

        #region Methods
        public void Add(EmailAccount model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Edit(EmailAccount model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public EmailAccount GetById(int id, bool noTracking = false)
        {
            if(id == 0)
            {
                return null;
            }

            var query = _repository.Table;

            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(e => e.Id == id).FirstOrDefault();

            return model;
        }

        public IPagedList<EmailAccount> List(EmailAccountListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(e => e.Name == listVM.Name);
            }

            var models = new PagedList<EmailAccount>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
