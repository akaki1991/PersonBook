using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.PersonRelationAggregate.ReaedModels;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class PersonRelationShipReadModelTypeConfiguration : IEntityTypeConfiguration<PersonRelationshipReadModel>
    {
        public void Configure(EntityTypeBuilder<PersonRelationshipReadModel> builder)
        {
            builder.ToTable("PersonRelationShipReadModel", "PersonBookReadModel");
        }
    }
}
