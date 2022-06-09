using Lipar.Entities.Domain.Core;
using System.Linq;

namespace Lipar.Services.Core.Contracts
{
    public interface IGenderService
    {
        IQueryable<Gender> GetGenders();
    }
}
