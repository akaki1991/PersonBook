using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.PhotoAggregate;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class PhotoTypeConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photo", "PersonBook");
        }
    }
}
