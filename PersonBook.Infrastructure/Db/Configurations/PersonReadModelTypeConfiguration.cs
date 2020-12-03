using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.PersonAggregate.ReadModels;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class PersonReadModelTypeConfiguration : IEntityTypeConfiguration<PersonReadModel>
    {
        public void Configure(EntityTypeBuilder<PersonReadModel> builder)
        {
            builder.ToTable("PersonReadModel", "PersonBookReadModel");
        }
    }
}
