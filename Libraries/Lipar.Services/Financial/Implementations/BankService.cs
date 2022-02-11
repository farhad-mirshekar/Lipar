using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Financial;
using Lipar.Services.Financial.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.Financial.Implementations
{
    public class BankService : IBankService
    {
        #region Ctor
        public BankService(IRepository<Bank> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<Bank> _repository;
        #endregion

        #region Methods
        public void Add(Bank model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public void Delete(Bank model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(Bank model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Update(model);
        }

        public Bank GetById(Guid id, bool noTracking = false)
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

            var model = query.Where(b => b.Id == id).FirstOrDefault();

            return model;
        }

        public IPagedList<Bank> List(BankListVM listVM)
        {
            var query = _repository.TableNoTracking.Include(x=>x.EnabledType).AsQueryable();

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(b => b.Name == listVM.Name);
            }

            var models = new PagedList<Bank>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
