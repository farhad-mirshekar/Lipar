using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Services.Messages
{
   public interface IWorkflowMessageService
    {
        /// <summary>
        /// send password recovery message
        /// </summary>
        /// <param name="user"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        void SendPasswordRecoveryMessage(User user, int languageId);
    }
}
