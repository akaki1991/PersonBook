using PersonBook.Domain.PersonAggregate.Events;
using PersonBook.Domain.PersonAggregate.ValueObjects;
using PersonBook.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PersonBook.Domain.PersonAggregate
{
    public class Person : AggregateRoot
    {
        protected Person()
        {
        }

        public Person(string firstName,
                      string lastName,
                      Gender gender,
                      string personalNumber,
                      DateTimeOffset birthDate,
                      string cityName,
                      int cityId,
                      ICollection<PhoneNumber> phoneNumbers,
                      Photo photo,
                      int photoId)
        {
            if (phoneNumbers != null && phoneNumbers.Count() > 3)
                throw new DomainException("Only 3 phonenumber can be added to person.");

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            CityName = cityName;
            CityId = cityId;
            PhoneNumbers = phoneNumbers;
            Photo = photo;
            PhotoId = photoId;
            CreateDate = DateTimeOffset.Now.ToUniversalTime();

            Raise(new PersonAddedEvent(this));
        }

        public void ChangeDetails(string firstName,
                                  string lastName,
                                  Gender gender,
                                  string personalNumber,
                                  DateTimeOffset birthDate,
                                  string cityName,
                                  int cityId,
                                  ICollection<PhoneNumber> phoneNumbers,
                                  Photo photo,
                                  int photoId)
        {
            if (phoneNumbers != null && phoneNumbers.Count() > 3)
                throw new DomainException("Only 3 phonenumber can be added to person.");

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            CityName = cityName;
            CityId = cityId;
            PhoneNumbers = phoneNumbers;
            Photo = photo;
            PhotoId = photoId;

            LastChangeDate = DateTimeOffset.Now.ToUniversalTime();

            Raise(new PersonChangedEvent(this));
        }

        public void RaiseDeletedEvent()
        {
            Raise(new PersonDeletedEvent(Id));
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Gender Gender { get; private set; }

        public string PersonalNumber { get; private set; }

        public DateTimeOffset BirthDate { get; private set; }

        public int CityId { get; private set; }

        public string CityName { get; private set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; private set; }

        public int? PhotoId { get; private set; }

        public virtual Photo Photo { get; private set; }

        public void AddPhoneNumber(PhoneNumber number)
        {
            if (PhoneNumbers != null && !PhoneNumbers.Any())
            {
                PhoneNumbers = new List<PhoneNumber>();
            }

            PhoneNumbers.Add(number);
        }
    }
}
