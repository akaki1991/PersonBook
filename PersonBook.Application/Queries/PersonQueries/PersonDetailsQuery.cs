using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.ReadModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonBook.Application.Queries.PersonQueries
{
    public class PersonDetailsQuery : Query<PersonDetailsQueryResult>
    {
        public int Id { get; set; }

        public async override Task<QueryExecutionResult<PersonDetailsQueryResult>> ExecuteAsync()
        {
            var person = await _db.Set<PersonReadModel>().FirstOrDefaultAsync(x => x.Id == Id);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var personPhoto = default(Photo);

            if (person.PhotoId != null)
                personPhoto = new Photo
                {
                    Id = person.PhotoId,
                    Url = person.PhotoUrl,
                    Width = person.PhotoWidth,
                    Height = person.PhotoHeight
                };

            return await OkAsync(new PersonDetailsQueryResult
            {
                Id = person.Id,
                FirstName = person.FirstName,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                City = new City
                {
                    Id = person.CityId,
                    Name = person.CityName
                },
                LastName = person.LastName,
                PersonalNumber = person.PersonalNumber,
                Photo = personPhoto,
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Home,
                        Number = person.HomePhoneNumber
                    },
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Mobile,
                        Number = person.MobilePhoneNumber
                    },
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Office,
                        Number = person.OfficePhoneNumber
                    }
                }
            });
        }
    }

    public class PersonDetailsQueryResult
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public City City { get; set; }

        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }

        public Photo Photo { get; set; }
    }

    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class PhoneNumber
    {
        public string Number { get; set; }

        public PhoneNumberType Type { get; set; }
    }

    public class Photo
    {
        public int? Id { get; set; }

        public string Url { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }
}
