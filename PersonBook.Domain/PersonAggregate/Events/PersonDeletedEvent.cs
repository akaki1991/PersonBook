using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonAggregate.Events
{
    public class PersonDeletedEvent : DomainEvent
    {
        public PersonDeletedEvent(int id)
        {
            AggregateRootId = id;
        }
    }
}
