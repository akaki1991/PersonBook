using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonBook.Api.Infrastructure;
using PersonBook.Application.Commands.PersonConnectionCommands;
using PersonBook.Application.Infrastructure;
using System.Threading.Tasks;

namespace PersonBook.Api.Controllers.V1
{
    [ApiController]
    [Route("v1/personrelationship")]
    public class PersonRelationShipController : BaseAPiController
    {

        private readonly CommandExecutor _commandExecutor;
        private readonly QueryExecutor _queryExecutor;

        public PersonRelationShipController(CommandExecutor commandExecutor, QueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(DomainOperationResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddPersionRelationshipCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(DomainOperationResult), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] ChangePersionRelationshipCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeletePersionRelationshipCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }
    }
}
