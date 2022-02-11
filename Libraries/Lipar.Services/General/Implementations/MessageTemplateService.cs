using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class MessageTemplateService : IMessageTemplateService
    {
        #region Ctor
        public MessageTemplateService(IRepository<MessageTemplate> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<MessageTemplate> _repository;
        #endregion

        #region Methods
        public void Add(MessageTemplate model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(MessageTemplate model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(MessageTemplate model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public MessageTemplate GetById(Guid id, bool noTracking = false)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(m => m.Id == id).FirstOrDefault();

            return model;
        }

        public IPagedList<MessageTemplate> List(MessageTemplateListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(m => m.Name == listVM.Name.Trim());
            }

            var models = new PagedList<MessageTemplate>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public MessageTemplate GetMessageTemplateByName(string name)
        {
            var query = _repository.TableNoTracking.Where(m => m.Name == name && m.IsActive == true);

            return query.FirstOrDefault();
        }
        #endregion
    }
}
