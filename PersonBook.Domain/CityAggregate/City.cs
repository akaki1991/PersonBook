using PersonBook.Domain.CityAggregate.Events;
using PersonBook.Domain.Shared;
using System;

namespace PersonBook.Domain.CityAggregate
{
    public class City : AggregateRoot
    {
        public City()
        {
        }

        public City(string name)
        {
            Name = name;
            CreateDate = DateTimeOffset.Now.ToUniversalTime();

            Raise(new CityPlacedEvent(this));
        }

        public string Name { get; private set; }

        public void RaiseDeleteEvent()
        {
            Raise(new CityDeletedEvent(Id));
        }
    }
}
