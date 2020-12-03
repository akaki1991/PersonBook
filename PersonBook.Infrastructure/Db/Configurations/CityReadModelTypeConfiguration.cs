using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonBook.Domain.CityAggregate.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonBook.Infrastructure.Db.Configurations
{
    public class CityReadModelTypeConfiguration : IEntityTypeConfiguration<CityReadModel>
    {
        public void Configure(EntityTypeBuilder<CityReadModel> builder)
        {
            builder.ToTable("CityReadModel", "PersonBookReadModel");
        }
    }
}
