using Domain;
using Infastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.DataBase
{
    internal class SomeEntityDbContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Таблица сущностей в БД
        /// </summary>
        public DbSet<SomeEntity> Entities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SomeEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
