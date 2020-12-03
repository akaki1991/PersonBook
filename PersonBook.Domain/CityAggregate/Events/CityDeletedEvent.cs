using PersonBook.Domain.Shared;

namespace PersonBook.Domain.CityAggregate.Events
{
    public class CityDeletedEvent : DomainEvent
    {
        public CityDeletedEvent(int id)
        {
            AggregateRootId = id;
        }
    }
}
