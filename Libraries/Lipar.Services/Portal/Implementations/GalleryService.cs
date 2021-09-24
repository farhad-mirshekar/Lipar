using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class GalleryService : IGalleryService
    {
        #region Fields
        private readonly IRepository<Gallery> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public GalleryService(IRepository<Gallery> repository
                            , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(Gallery model)
        {
            _repository.Insert(model);
        }

        public void Delete(Gallery model)
        {
            model.RemoveDate = CommonHelper.GetCurrentDateTime();
            model.RemoverId = _workContext.CurrentUser.Id;

            Edit(model);
        }

        public void Edit(Gallery model)
        {
            _repository.Update(model);
        }

        public Gallery GetById(int Id)
        { 
          var gallery = _repository.GetById(Id);
            if (gallery.RemoverId.HasValue && gallery.RemoverId.Value != 0 && gallery.RemoveDate != null)
                return null;

            return gallery;
        }

        public IPagedList<Gallery> List(GalleryListVM listVM)
        {
            var query = _repository.Table;
            query = query.Where(g => g.RemoverId == null && g.RemoveDate == null);

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(g => g.Name.Contains(listVM.Name.Trim()));


            var models = new PagedList<Gallery>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
