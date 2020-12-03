using PersonBook.Domain.PhotoAggregate;
using PersonBook.Domain.PhotoAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.Shared;

namespace PersonBook.Infrastructure.Repositories
{
    public class PhotoRepository : EFBaseRepository<PersonBookDbContext, Photo>, IPhotoRepository
    {
        public PhotoRepository(PersonBookDbContext context) : base(context)
        {
        }
    }
}
