using Lipar.Data;
using Lipar.Entities;

namespace Lipar.Tests
{
   public interface IFakeRepository<T> : IFakeStoreRepositoryContainer where T : BaseEntity
    {
        IRepository<T> GetRepository();
    }
}
