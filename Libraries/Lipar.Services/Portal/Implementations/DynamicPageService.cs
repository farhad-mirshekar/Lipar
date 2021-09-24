using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class DynamicPageService : IDynamicPageService
    {
        #region Fields
        private readonly IRepository<DynamicPage> _repository;
        private readonly IRepository<DynamicPageDetail> _dynamicPageDetailRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public DynamicPageService(IRepository<DynamicPage> repository
                                , IWorkContext workContext
                                , IRepository<DynamicPageDetail> dynamicPageDetailRepository)
        {
            _repository = repository;
            _workContext = workContext;
            _dynamicPageDetailRepository = dynamicPageDetailRepository;
        }
        #endregion

        #region Methods
        public void Add(DynamicPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(DynamicPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var removeDate = CommonHelper.GetCurrentDateTime();

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = removeDate;

            //find dynamic page detail
            var query = _dynamicPageDetailRepository.Table;
            query = query.Where(dd => dd.RemoverId == null);

            query = query.Where(dd => dd.DynamicPageId == model.Id);
            var dynamicPageDetails = query.ToList();

            if(dynamicPageDetails != null && dynamicPageDetails.Count > 0)
            {
                foreach (var dynamicPageDetail in dynamicPageDetails)
                {
                    dynamicPageDetail.RemoverId = _workContext.CurrentUser.Id;
                    dynamicPageDetail.RemoveDate = removeDate;
                }
            }

            //remove dynamic page
            Edit(model);

            //remove dynamic page details
            _dynamicPageDetailRepository.Update(dynamicPageDetails);
        }

        public void Edit(DynamicPage model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public DynamicPage GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _repository.GetById(Id);
        }

        public IPagedList<DynamicPage> List(DynamicPageListVM listVM)
        {
            var query = _repository.TableNoTracking;
            query = query.Where(d => d.RemoverId == null && d.RemoveDate == null);

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                listVM.Name = listVM.Name.Trim();
                query = query.Where(d => d.Name.Contains(listVM.Name));
            }

            if (listVM.IncludeInTopMenu.HasValue)
            {
                query = query.Where(d => d.IncludeInTopMenu == listVM.IncludeInTopMenu);
            }

            var models = new PagedList<DynamicPage>(query, listVM.PageIndex, listVM.PageSize);
            return models;
        }
        #endregion
    }
}
