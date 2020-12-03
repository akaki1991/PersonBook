using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonRelationAggregate.Events
{
    public class PersonRelationshipDeletedEvent : DomainEvent
    {
        public PersonRelationshipDeletedEvent(int id)
        {
            AggregateRootId = id;
        }
    }
}
