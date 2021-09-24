using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;

namespace Lipar.Services.General.Implementations
{
    public class ContactUsTypeService : IContactUsTypeService
    {
        #region Ctor
        public ContactUsTypeService(IRepository<ContactUsType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ContactUsType> _repository;
        #endregion

        #region Method
        public IPagedList<ContactUsType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<ContactUsType>(query);

            return models;
        }
        #endregion
    }
}
