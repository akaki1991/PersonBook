using PersonBook.Domain.Shared;
using PersonBook.Infrastructure.Db;

namespace PersonBook.Infrastructure.EvnetDispatching
{
    public interface IHandleEvent<in T> where T : DomainEvent
    {
        void Handle(T @event, PersonBookDbContext db);
    }
}
