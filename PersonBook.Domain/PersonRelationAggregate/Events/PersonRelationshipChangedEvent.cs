using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonRelationAggregate.Events
{
    public class PersonRelationshipChangedEvent : DomainEvent
    {
        public PersonRelationshipChangedEvent(PersonRelationship personRelationship)
        {
            PersonRelationship = personRelationship;
        }

        public PersonRelationship PersonRelationship { get; private set; }
    }
}
