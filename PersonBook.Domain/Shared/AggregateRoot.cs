using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PersonBook.Domain.Shared
{
    public abstract class AggregateRoot : Entity, IHasDomainEvents
    {
        public DateTimeOffset CreateDate { get; protected set; }

        public DateTimeOffset LastChangeDate { get; protected set; }

        private IList<DomainEvent> _events { get; } = new List<DomainEvent>();

        IReadOnlyList<DomainEvent> IHasDomainEvents.UncommittedChanges() => new ReadOnlyCollection<DomainEvent>(_events);

        void IHasDomainEvents.MarkChangesAsCommitted() => _events.Clear();

        void IHasDomainEvents.Raise(DomainEvent evnt) => _events.Add(evnt);

        protected void Raise(DomainEvent evnt)
        {
            evnt.ChangeDetails(Id, DateTime.UtcNow, evnt.GetType().Name);

            (this as IHasDomainEvents).Raise(evnt);
        }

        bool IHasDomainEvents.NewlyCreated() => _events.Any(x => x is ICreateEvent);
    }
}
