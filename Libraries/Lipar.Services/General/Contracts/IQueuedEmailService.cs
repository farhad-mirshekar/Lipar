using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IQueuedEmailService
    {
        /// <summary>
        /// add queued email method
        /// </summary>
        /// <param name="model">queued email</param>
        void Add(QueuedEmail model);
    }
}
