using PersonBook.Domain.CityAggregate;
using PersonBook.Domain.CityAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.Shared;

namespace PersonBook.Infrastructure.Repositories
{
    public class CityRepository : EFBaseRepository<PersonBookDbContext, City>, ICityRepository
    {
        public CityRepository(PersonBookDbContext context) : base(context)
        {
        }
    }
}
