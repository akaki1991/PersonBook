using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Domain.CityAggregate.ReadModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Queries.CityQueries
{
    public class CitiesQuery : Query<CitiesQueryResult>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public async override Task<QueryExecutionResult<CitiesQueryResult>> ExecuteAsync()
        {
            PageSize = PageSize <= 0 ? 20 : PageSize;
            PageIndex = PageIndex <= 0 ? 1 : PageIndex;

            var cities = await _db.Set<CityReadModel>()
                                  .Where(x => (Id == null ? true : x.Id == Id) && 
                                              (Name == null ? true : x.Name.Contains(Name)))
                                  .Skip(PageSize * (PageIndex - 1))
                                  .Take(PageSize)
                                  .ToListAsync();

            var result = cities.Select(x => new CitiesQueryResultItem { Id = x.AggregateRootId, Name = x.Name });

            return await OkAsync(new CitiesQueryResult { Cities = result });
        }
    }

    public class CitiesQueryResult
    {
        public IEnumerable<CitiesQueryResultItem> Cities { get; set; }
    }

    public class CitiesQueryResultItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
