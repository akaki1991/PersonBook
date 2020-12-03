using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.CityAggregate;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class CityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "PersonBook");
        }
    }
}
