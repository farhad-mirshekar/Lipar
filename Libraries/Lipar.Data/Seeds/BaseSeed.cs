using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Data.Seeds
{
    public abstract class BaseSeed<T> : IBaseSeed where T : class
    {
        public BaseSeed()
        {
            IsAddIfNotExist = false;
        }
        public abstract string ModelName { get; }
        public abstract string Schema { get; }
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }
        public abstract List<T> Items { get; set; }
        public bool IsAddIfNotExist
        {
            get; set;
        }
        public void AddIfNotExist(int index)
        {

        }

        public void Add(int index)
        {
            var contextOptions = new DbContextOptionsBuilder<LiparContext>().UseSqlServer(@"Data Source=fm-pc;Integrated Security=true;Initial Catalog=LiparDb;Persist Security Info=True;;MultipleActiveResultSets=true;").Options;
            using (var db = new LiparContext(contextOptions))
            {
                db.Set<T>().Add(Items[index]);

                db.SaveChanges();
            }
        }

        public void Update(int index)
        {
            var contextOptions = new DbContextOptionsBuilder<LiparContext>().UseSqlServer(@"Data Source=fm-pc;Integrated Security=true;Initial Catalog=LiparDb;Persist Security Info=True;;MultipleActiveResultSets=true;").Options;
            using (var db = new LiparContext(contextOptions))
            {
                db.Set<T>().Update(Items[index]);

                db.SaveChanges();
            }
        }

        public void InsertIfNotExist()
        {
            throw new System.NotImplementedException();
        }
    }
}
