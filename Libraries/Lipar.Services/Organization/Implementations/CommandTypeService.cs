using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;

namespace Lipar.Services.Organization.Implementations
{
    public class CommandTypeService : ICommandTypeService
    {
        #region Ctor
        public CommandTypeService(IRepository<CommandType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<CommandType> _repository;
        #endregion

        #region Methods
        public IPagedList<CommandType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<CommandType>(query);

            return models;
        }
        #endregion
    }
}
