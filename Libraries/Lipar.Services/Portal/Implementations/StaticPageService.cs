using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class StaticPageService : IStaticPageService
    {
        #region Fields
        private readonly IRepository<StaticPage> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public StaticPageService(IRepository<StaticPage> repository
                               , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(StaticPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(StaticPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            Edit(model);
        }

        public void Edit(StaticPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public StaticPage GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _repository.GetById(Id);
        }

        public IPagedList<StaticPage> List(StaticPageListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                listVM.Name = listVM.Name.Trim();
                query = query.Where(s =>s.Name.Contains(listVM.Name));
            }

            //check view status
            if(listVM.ViewStatusId.HasValue && listVM.ViewStatusId.Value != 0)
            {
                query = query.Where(s => s.ViewStatusId == listVM.ViewStatusId);
            }

            //check for menu
            if (listVM.IncludeInTopMenu.HasValue)
            {
                query = query.Where(s => s.IncludeInTopMenu == listVM.IncludeInTopMenu);
            }

            var models = new PagedList<StaticPage>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
