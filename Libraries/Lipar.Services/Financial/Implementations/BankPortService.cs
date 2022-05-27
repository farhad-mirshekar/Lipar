using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Financial;
using Lipar.Services.Financial.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.Financial.Implementations
{
    public class BankPortService : IBankPortService
    {
        #region Ctor
        public BankPortService(IRepository<BankPort> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<BankPort> _repository;
        #endregion

        #region Methods
        public void Add(BankPort model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public void Delete(BankPort model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(BankPort model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Update(model);
        }

        public BankPort GetById(Guid id, bool noTracking = false)
        {
            if(id == Guid.Empty)
            {
                return null;
            }

            var query = _repository.Table
                                   .Include(b=>b.Bank);
            if (noTracking)
            {
                query = _repository.TableNoTracking
                                   .Include(b => b.Bank);
            }

            var model = query.Where(bp => bp.Id == id).FirstOrDefault();

            return model;
        }

        public IPagedList<BankPort> List(BankPortListVM listVM)
        {
            var query = _repository.TableNoTracking.Include(x=>x.EnabledType).AsQueryable();

            if(listVM.BankId.HasValue && listVM.BankId.Value != Guid.Empty)
            {
                query = query.Where(bp => bp.BankId == listVM.BankId.Value);
            }

            var models = new PagedList<BankPort>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public IQueryable<BankPort> GetBankPorts()
        {
            var query = _repository.TableNoTracking;

            query = query.Where(bp => bp.IsDefault == true && bp.EnabledTypeId == (int)EnabledTypeEnum.Active);
            return query;
        }

        public BankPort GetActiveBankPort(Guid bankId , bool noTracking = false)
        {
            if(bankId == Guid.Empty)
            {
                throw new Exception("bank id is not valid!");
            }
            
            var query = _repository.Table;

            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var bankPort = query.Where(bp => bp.BankId == bankId).FirstOrDefault();

            return bankPort;
        }
        #endregion
    }
}
