using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.PersonAggregate;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasIndex(x => x.PersonalNumber).IsUnique();

            builder.OwnsOne(o => o.Photo);

            builder.ToTable("Person").HasMany(x => x.PhoneNumbers);
        }
    }
}
