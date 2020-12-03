using PersonBook.Infrastructure.Db;
using System.Collections.Generic;

namespace PersonBook.Infrastructure.EvnetDispatching
{
    public interface IInternalEventDispatcher<TDomainEvent>
    {
        void Dispatch(IReadOnlyList<TDomainEvent> domainEvents, PersonBookDbContext dbContext);
    }
}
