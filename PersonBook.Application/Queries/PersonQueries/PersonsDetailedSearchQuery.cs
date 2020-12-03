using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Queries.PersonQueries
{
    public class PersonsDetailedSearchQuery : Query<PersonsDetailedSearchQueryResult>
    {
        public int? Id { get; set; }

        public string Firsname { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public string PhoneNumber { get; set; }

        public Gender? Gender { get; set; }

        public DateTimeOffset? BirthDateFrom { get; set; }

        public DateTimeOffset? BirthDateTo { get; set; }

        public int? CityId { get; set; }

        public string CityName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public async override Task<QueryExecutionResult<PersonsDetailedSearchQueryResult>> ExecuteAsync()
        {
            PageSize = PageSize <= 0 ? 20 : PageSize;
            PageIndex = PageIndex <= 0 ? 1 : PageIndex;

            var persons = await _db.Set<PersonReadModel>()
                                   .Where(x =>
                                           (Id == null ? true : x.Id == Id)
                                           && (string.IsNullOrEmpty(Firsname) ? true : x.FirstName == Firsname)
                                           && (string.IsNullOrEmpty(LastName) ? true : x.LastName == LastName)
                                           && (string.IsNullOrEmpty(PersonalNumber) ? true : x.PersonalNumber == PersonalNumber)
                                           && (string.IsNullOrEmpty(PhoneNumber) ? true : (x.HomePhoneNumber == PhoneNumber
                                                                             || x.OfficePhoneNumber == PhoneNumber
                                                                             || x.MobilePhoneNumber == PhoneNumber))
                                           && (Gender == null ? true : x.Gender == Gender)
                                           && (BirthDateFrom == null ? true : x.BirthDate >= BirthDateFrom)
                                           && (BirthDateTo == null ? true : x.BirthDate <= BirthDateTo)
                                           && (CityId == null ? true : x.CityId == CityId)
                                           && (string.IsNullOrEmpty(CityName) ? true : x.CityName == CityName))
                                   .OrderBy(x => x.CreateDate)
                                   .Skip(PageSize * (PageIndex - 1))
                                   .Take(PageSize)
                                   .ToListAsync();

            var result = persons.Select(x => new PersonDetailsQueryResult
            {
                Id = x.Id,
                FirstName = x.FirstName,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                City = new City
                {
                    Id = x.CityId,
                    Name = x.CityName
                },
                LastName = x.LastName,
                PersonalNumber = x.PersonalNumber,
                Photo = x.PhotoId != null
                        ? new Photo { Id = x.PhotoId, Url = x.PhotoUrl, Width = x.PhotoWidth, Height = x.PhotoHeight }
                        : null,
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Home,
                        Number = x.HomePhoneNumber
                    },
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Mobile,
                        Number = x.MobilePhoneNumber
                    },
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Office,
                        Number = x.OfficePhoneNumber
                    }
                }
            });

            return await OkAsync(new PersonsDetailedSearchQueryResult { Persons = result });
        }
    }

    public class PersonsDetailedSearchQueryResult
    {
        public IEnumerable<PersonDetailsQueryResult> Persons { get; set; }
    }
}
