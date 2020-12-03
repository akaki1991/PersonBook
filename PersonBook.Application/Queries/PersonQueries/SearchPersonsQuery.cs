using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.ReadModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Queries.PersonQueries
{
    public class SearchPersonsQuery : Query<SearchPersonsQueryResult>
    {
        public string SearchTerm { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public async override Task<QueryExecutionResult<SearchPersonsQueryResult>> ExecuteAsync()
        {
            PageSize = PageSize <= 0 ? 20 : PageSize;
            PageIndex = PageIndex <= 0 ? 1 : PageIndex;

            var persons = await _db.Set<PersonReadModel>()
                                   .Where(x => EF.Functions.Like(x.FirstName, $"%{SearchTerm}%")
                                               || EF.Functions.Like(x.LastName, $"%{SearchTerm}%")
                                               || EF.Functions.Like(x.PersonalNumber, $"%{SearchTerm}%"))
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

            return await OkAsync(new SearchPersonsQueryResult
            {
                Persons = result
            });
        }
    }

    public class SearchPersonsQueryResult
    {
        public IEnumerable<PersonDetailsQueryResult> Persons { get; set; }
    }
}
