using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonRelationAggregate.Events
{
    public class PersonRelationshipPlacedEvent : DomainEvent
    {
        public PersonRelationshipPlacedEvent(PersonRelationship personRelationship)
        {
            PersonRelationship = personRelationship;
        }

        public PersonRelationship PersonRelationship { get; private set; }
    }
}
