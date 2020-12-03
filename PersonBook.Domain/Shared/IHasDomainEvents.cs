using System.Collections.Generic;

namespace PersonBook.Domain.Shared
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<DomainEvent> UncommittedChanges();

        void MarkChangesAsCommitted();

        void Raise(DomainEvent evnt);

        bool NewlyCreated();
    }
}