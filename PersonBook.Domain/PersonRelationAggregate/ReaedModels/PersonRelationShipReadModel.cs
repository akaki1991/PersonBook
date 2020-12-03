using System;

namespace PersonBook.Domain.PersonRelationAggregate.ReaedModels
{
    public class PersonRelationshipReadModel
    {
        public int Id { get; set; }

        public int AggregateRootId { get; set; }

        public PersonRelationshipType PersonRelationshipType { get; set; }

        public int FirstPersonId { get; set; }

        public int SecondPersonId { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }
    }
}
