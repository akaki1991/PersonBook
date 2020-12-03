using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonBook.Api.Infrastructure;
using PersonBook.Application.Commands.PersonCommands;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Queries.PersonQueries;
using System.Threading.Tasks;

namespace PersonBook.Api.Controllers.V1
{
    [ApiController]
    [Route("v1/person")]
    public class PersonController : BaseAPiController
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly QueryExecutor _queryExecutor;

        public PersonController(CommandExecutor commandExecutor, QueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(DomainOperationResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddPersonCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(DomainOperationResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] ChangePersonCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Updated);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeletePersonCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Deleted);
        }

        [HttpGet("details")]
        [ProducesResponseType(typeof(PersonDetailsQueryResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromQuery] PersonDetailsQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<PersonDetailsQuery, PersonDetailsQueryResult>(query);

            return QueryResultToHttpResponse(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(SearchPersonsQueryResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] SearchPersonsQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<SearchPersonsQuery, SearchPersonsQueryResult>(query);

            return QueryResultToHttpResponse(result);
        }

        [HttpGet("detailedsearch")]
        [ProducesResponseType(typeof(PersonsDetailedSearchQueryResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DetailedSearch([FromQuery] PersonsDetailedSearchQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<PersonsDetailedSearchQuery, PersonsDetailedSearchQueryResult>(query);

            return QueryResultToHttpResponse(result);
        }

        [HttpGet("relatedpersons")]
        [ProducesResponseType(typeof(RelatedPersonsQueryResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RelatedPersons([FromQuery] RelatedPersonsQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<RelatedPersonsQuery, RelatedPersonsQueryResult>(query);

            return QueryResultToHttpResponse(result);
        }
    }
}
