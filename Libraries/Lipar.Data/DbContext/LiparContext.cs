using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Lipar.Data
{
    public class LiparContext : DbContext, IDbContext
    {
        public LiparContext(DbContextOptions<LiparContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
            .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=fm-pc;Integrated Security=true;Initial Catalog=LiparDb;Persist Security Info=True;;MultipleActiveResultSets=true;");
        }
        DbSet<TEntity> IDbContext.Set<TEntity>() 
        {
            return base.Set<TEntity>();
        }
    }
}
