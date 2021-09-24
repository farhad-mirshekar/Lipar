using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
   public interface ILanguageCultureService
    {
        /// <summary>
        /// language culture list method
        /// </summary>
        /// <returns></returns>
        IPagedList<LanguageCulture> List();
    }
}
