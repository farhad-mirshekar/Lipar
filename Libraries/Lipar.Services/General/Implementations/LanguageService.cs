using Lipar.Core;
using Lipar.Core.Infrastructure;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class LanguageService : ILanguageService
    {
        #region Fields
        private readonly IRepository<Language> _repository;
        #endregion

        #region Ctor
        public LanguageService(IRepository<Language> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Language model)
        {
            _repository.Insert(model);
        }

        public void Delete(Language model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Language model)
        {
            _repository.Update(model);
        }

        public Language GetById(int Id, bool noTracking = false)
        {
            var query = _repository.Table
                                   .Include(x => x.LanguageCulture).AsQueryable();
            if (noTracking)
            {
                query = _repository.TableNoTracking
                                   .Include(x => x.LanguageCulture).AsQueryable();

                return query.Where(x => x.Id == Id).FirstOrDefault();
            }

            return query.FirstOrDefault(x => x.Id == Id);
        }

        public IPagedList<Language> List(LanguageListVM listVM)
        {
            var query = _repository.TableNoTracking.Include(x => x.LanguageCulture).AsQueryable();

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(l => l.Name.Contains(listVM.Name.Trim()));

            var languages = new PagedList<Language>(query, listVM.PageIndex, listVM.PageSize, false);

            return languages;
        }

        #endregion
    }
}
