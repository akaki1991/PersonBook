using System;

namespace PersonBook.Domain.PersonAggregate.ReadModels
{
    public class PersonReadModel
    {
        public int Id { get; set; }

        public int AggregateRootId { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string HomePhoneNumber { get; set; }

        public string OfficePhoneNumber { get; set; }

        public int? PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public int? PhotoWidth { get; set; }

        public int? PhotoHeight { get; set; }
    }
}
