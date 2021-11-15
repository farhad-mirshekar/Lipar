using Lipar.Data;
using Lipar.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Tests
{
    public class FakeRepository<T> : Mock<IRepository<T>>, IFakeRepository<T> where T : BaseEntity
    {
        private readonly int[] _initIds = Array.Empty<int>();
        private readonly HashSet<T> _table = new HashSet<T>(new BaseEntityEqualityComparer<T>());
        public IRepository<T> GetRepository()
        {
            return Object;
        }

        public void ResetRepository()
        {
            _table.RemoveWhere(e => !_initIds.Contains(e.Id));
        }

        private void SetNewId(T entity)
        {
            if (entity.Id != 0)
                return;

            if (_table.Count == 0)
            {
                entity.Id = 1;
                return;
            }

            entity.Id = _table.Max(e => e.Id) + 1;
        }

        protected T Insert(T entity)
        {
            SetNewId(entity);

            if (!_table.Add(entity))
                throw new ArgumentException($"Entity with id#{entity.Id} already exist");

            return entity;
        }

    }
}
