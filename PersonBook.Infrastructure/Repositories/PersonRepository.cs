using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.Shared;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PersonBook.Infrastructure.Repositories
{
    public class PersonRepository : EFBaseRepository<PersonBookDbContext, Person>, IPersonRepository
    {
        PersonBookDbContext _context;

        public PersonRepository(PersonBookDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PhoneNumber> GetPhoneNumberAsync(string number)
        {
            return await _context.Set<PhoneNumber>().FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<PhoneNumber> GetPhoneNumberByIdAsync(int id)
        {
            return await _context.Set<PhoneNumber>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersAsync(ICollection<string> numbers)
        {
            return await _context.Set<PhoneNumber>().Where(x => numbers.Contains(x.Number)).ToListAsync();
        }
    }
}
