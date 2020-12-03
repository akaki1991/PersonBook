using PersonBook.Domain.PersonRelationAggregate.Events;
using PersonBook.Domain.Shared;
using System;

namespace PersonBook.Domain.PersonRelationAggregate
{
    public class PersonRelationship : AggregateRoot
    {
        public PersonRelationship()
        {
        }

        public PersonRelationship(PersonRelationshipType personRelationshipType,
                                  int firstPersonId,
                                  int secondPersonId)
        {
            PersonRelationshipType = personRelationshipType;
            FirstPersonId = firstPersonId;
            SecondPersonId = secondPersonId;

            CreateDate = DateTimeOffset.Now.ToUniversalTime();

            Raise(new PersonRelationshipPlacedEvent(this));
        }

        public PersonRelationshipType PersonRelationshipType { get; private set; }

        public int FirstPersonId { get; private set; }

        public int SecondPersonId { get; private set; }

        public void ChangePersonRelationshipType(PersonRelationshipType personRelationshipType)
        {
            PersonRelationshipType = personRelationshipType;

            LastChangeDate = DateTimeOffset.Now.ToUniversalTime();

            Raise(new PersonRelationshipChangedEvent(this));
        }

        public void RaiseDeletedEvent()
        {
            Raise(new PersonRelationshipDeletedEvent(Id));
        }
    }
}
