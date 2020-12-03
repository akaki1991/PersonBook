using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonAggregate.Events
{
    public class PersonChangedEvent : DomainEvent
    {
        public PersonChangedEvent(Person person)
        {
            Person = person;            
        }

        public Person Person { get; set; }
    }
}
