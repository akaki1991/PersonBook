using PersonBook.Domain.PersonRelationAggregate;
using PersonBook.Domain.PersonRelationAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.Shared;

namespace PersonBook.Infrastructure.Repositories
{
    public class PersonRelationshipRepository : EFBaseRepository<PersonBookDbContext, PersonRelationship>, IPersonRelationshipRepository
    {
        public PersonRelationshipRepository(PersonBookDbContext context) : base(context)
        {
        }
    }
}
