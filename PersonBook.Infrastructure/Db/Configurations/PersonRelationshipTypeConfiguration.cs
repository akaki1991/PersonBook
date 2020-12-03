using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.PersonRelationAggregate;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class PersonRelationshipTypeConfiguration : IEntityTypeConfiguration<PersonRelationship>
    {
        public void Configure(EntityTypeBuilder<PersonRelationship> builder)
        {
            builder.ToTable("PersonRelationship", "PersonBook");
        }
    }
}
