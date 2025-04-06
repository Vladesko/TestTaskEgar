using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.DataBase.Configurations
{
    /// <summary>
    /// Конфигурация полей сущности для БД
    /// </summary>
    internal class SomeEntityConfiguration : IEntityTypeConfiguration<SomeEntity>
    {
        public void Configure(EntityTypeBuilder<SomeEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique(); //Указывает на то, что id будет уникальным

        }
    }
}
