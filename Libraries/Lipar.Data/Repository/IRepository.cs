using Lipar.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Data
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(IEnumerable<T> entities);
        void Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
