using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonBook.Api.Infrastructure;
using PersonBook.Application.Commands.CityCommands;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Queries.CityQueries;
using System.Threading.Tasks;

namespace PersonBook.Api.Controllers.V1
{
    [ApiController]
    [Route("v1/city")]
    public class CityController : BaseAPiController
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly QueryExecutor _queryExecutor;

        public CityController(CommandExecutor commandExecutor, QueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(DomainOperationResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddCityCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(DomainOperationResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteCityCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Deleted);
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(CitiesQueryResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> List([FromQuery] CitiesQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<CitiesQuery, CitiesQueryResult>(query);

            return QueryResultToHttpResponse(result);
        }
    }
}
