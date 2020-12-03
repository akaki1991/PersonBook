using PersonBook.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonBook.Domain.PersonAggregate.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<PhoneNumber> GetPhoneNumberByIdAsync(int id);

        Task<PhoneNumber> GetPhoneNumberAsync(string name);

        Task<IEnumerable<PhoneNumber>> GetPhoneNumbersAsync(ICollection<string> names);
    }
}
