using System;

namespace PersonBook.Domain.Shared
{
    public abstract class DomainEvent
    {
        public int AggregateRootId { get; protected set; }

        public DateTimeOffset OccuredOn { get; protected set; }

        public string EventType { get; protected set; }

        public void ChangeDetails(int aggregateRootId, DateTimeOffset occuredOn, string eventType)
        {
            AggregateRootId = aggregateRootId;
            OccuredOn = occuredOn;
            EventType = eventType;
        }
    }
}