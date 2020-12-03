using Microsoft.EntityFrameworkCore;
using PersonBook.Infrastructure.Db.Configurations;

namespace PersonBook.Infrastructure.Db
{
    public class PersonBookDbContext : DbContext
    {
        public PersonBookDbContext(DbContextOptions<PersonBookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("PersonBook");

            builder.ApplyConfiguration(new PersonTypeConfiguration());
            builder.ApplyConfiguration(new PersonReadModelTypeConfiguration());
            builder.ApplyConfiguration(new CityTypeConfiguration());
            builder.ApplyConfiguration(new CityReadModelTypeConfiguration());
            builder.ApplyConfiguration(new PersonRelationshipTypeConfiguration());
            builder.ApplyConfiguration(new PersonRelationShipReadModelTypeConfiguration());
            builder.ApplyConfiguration(new PhotoTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}