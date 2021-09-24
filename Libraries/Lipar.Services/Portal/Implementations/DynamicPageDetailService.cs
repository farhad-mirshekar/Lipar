using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class DynamicPageDetailService : IDynamicPageDetailService
    {
        private readonly IRepository<DynamicPageDetail> _repository;
        private readonly IWorkContext _workContext;
        #region Fields

        #endregion

        #region Ctor
        public DynamicPageDetailService(IRepository<DynamicPageDetail> repository
                                      , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(DynamicPageDetail model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(DynamicPageDetail model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            Edit(model);
        }

        public void Edit(DynamicPageDetail model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public DynamicPageDetail GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _repository.GetById(Id);
        }

        public int GetDynamicPageDetailCount(int DynamicPageId, ViewStatusEnum viewStatus)
        {
            if (DynamicPageId == 0)
                return 0;

            var query = _repository.TableNoTracking;
            query = query.Where(dd => dd.DynamicPageId == DynamicPageId);

            query = query.Where(dd => dd.ViewStatusId == (int)viewStatus);


            var count = query.Count();
            return count;
        }

        public IPagedList<DynamicPageDetail> List(DynamicPageDetailListVM listVM)
        {
            var query = _repository.TableNoTracking;
            query = query.Where(dd => dd.RemoverId == null && dd.RemoveDate == null);

            if (listVM.DynamicPageId.HasValue && listVM.DynamicPageId.Value != 0)
            {
                query = query.Where(dd => dd.DynamicPageId == listVM.DynamicPageId);
            }

            var models = new PagedList<DynamicPageDetail>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
