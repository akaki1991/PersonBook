using PersonBook.Domain.Shared;

namespace PersonBook.Domain.CityAggregate.Events
{
    public class CityPlacedEvent : DomainEvent
    {
        public CityPlacedEvent(City city)
        {
            City = city;
        }

        public City City { get; set; }
    }
}
