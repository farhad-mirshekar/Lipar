using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;

namespace Lipar.Services.General.Implementations
{
    public class QueuedEmailService : IQueuedEmailService
    {
        #region Ctor
        public QueuedEmailService(IRepository<QueuedEmail> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<QueuedEmail> _repository;
        #endregion

        #region Methods
        public void Add(QueuedEmail model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }
        #endregion
    }
}
