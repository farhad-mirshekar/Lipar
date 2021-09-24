using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Core;
using Lipar.Services.Core.Contracts;

namespace Lipar.Services.Core.Implementations
{
    public class CommentStatusService : ICommentStatusService
    {
        #region Ctor
        public CommentStatusService(IRepository<CommentStatus> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<CommentStatus> _repository;
        #endregion

        #region Methods
        public IPagedList<CommentStatus> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<CommentStatus>(query);

            return models;
        }
        #endregion
    }
}
