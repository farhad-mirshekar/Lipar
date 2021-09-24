using Lipar.Core;
using Lipar.Entities.Domain.Core;

namespace Lipar.Services.Core.Contracts
{
    public interface ICommentStatusService
    {
        /// <summary>
        /// comment status list method
        /// </summary>
        /// <returns></returns>
        IPagedList<CommentStatus> List();
    }
}
