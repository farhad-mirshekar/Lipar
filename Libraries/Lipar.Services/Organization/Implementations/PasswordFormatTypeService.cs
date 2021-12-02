using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class PasswordFormatTypeService : IPasswordFormatTypeService
    {
        #region Ctor
        public PasswordFormatTypeService(IRepository<PasswordFormatType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<PasswordFormatType> _repository;
        #endregion
        public IList<PasswordFormatType> List()
        {
            var query = _repository.TableNoTracking;

            return query.Select(p => new PasswordFormatType
            {
                Id = p.Id,
                CreationDate = p.CreationDate,
                Title = p.Title
            }).ToList();
        }
    }
}
