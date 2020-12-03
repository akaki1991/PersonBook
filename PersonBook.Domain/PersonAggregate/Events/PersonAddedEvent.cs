using PersonBook.Domain.Shared;
using System;

namespace PersonBook.Domain.PersonAggregate.Events
{
    public class PersonAddedEvent : DomainEvent
    {
        public PersonAddedEvent(Person person)
        {
            Person = person;
        }

        public Person Person { get; set; }
    }
}
