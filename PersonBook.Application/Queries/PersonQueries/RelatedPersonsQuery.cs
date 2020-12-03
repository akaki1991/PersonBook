using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.ReadModels;
using PersonBook.Domain.PersonRelationAggregate;
using PersonBook.Domain.PersonRelationAggregate.ReaedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Queries.PersonQueries
{
    public class RelatedPersonsQuery : Query<RelatedPersonsQueryResult>
    {
        public int Id { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public async override Task<QueryExecutionResult<RelatedPersonsQueryResult>> ExecuteAsync()
        {
            PageSize = PageSize <= 0 ? 20 : PageSize;
            PageIndex = PageIndex <= 0 ? 1 : PageIndex;

            var person = await _db.Set<PersonReadModel>().FirstOrDefaultAsync(x => x.Id == Id);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var personRelationshipReadModel = await _db.Set<PersonRelationshipReadModel>()
                .Where(x => x.FirstPersonId == Id || x.SecondPersonId == Id).ToListAsync();

            var relationsIds = personRelationshipReadModel.Select(x => x.FirstPersonId).Where(x => x != Id).ToList();

            relationsIds.AddRange(personRelationshipReadModel.Select(x => x.SecondPersonId).Where(x => x != Id).ToList());

            var releatedPersons = await _db.Set<PersonReadModel>()
                .Where(x => relationsIds.Contains(x.Id))
                .OrderBy(x => x.CreateDate)
                .Skip(PageSize * (PageIndex - 1))
                .Take(PageSize)
                .ToListAsync();

            var result = releatedPersons.Select(x => new RelatedPersonsQueryResultItem
            {
                Id = x.AggregateRootId,
                FirstName = x.FirstName,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                City = new City
                {
                    Id = x.CityId,
                    Name = x.CityName
                },
                LastName = person.LastName,
                PersonalNumber = person.PersonalNumber,
                Photo = new Photo
                {
                    Url = person.PhotoUrl,
                    Width = person.PhotoWidth,
                    Height = person.PhotoHeight
                },
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
                },
                PersonRealtionshipId = personRelationshipReadModel
                        .FirstOrDefault(r => r.FirstPersonId == x.AggregateRootId
                                        || r.SecondPersonId == x.AggregateRootId).Id,
                PersonRelationshipType = personRelationshipReadModel
                        .FirstOrDefault(r => r.FirstPersonId == x.AggregateRootId
                                        || r.SecondPersonId == x.AggregateRootId).PersonRelationshipType
            }).ToList();

            return await OkAsync(new RelatedPersonsQueryResult
            {
                RelatedPersons = result
            });
        }
    }

    public class RelatedPersonsQueryResult
    {
        public IEnumerable<RelatedPersonsQueryResultItem> RelatedPersons { get; set; }
    }

    public class RelatedPersonsQueryResultItem
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

        public int PersonRealtionshipId { get; set; }

        public PersonRelationshipType PersonRelationshipType { get; set; }
    }

}
