using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonBook.Api.Infrastructure;
using PersonBook.Application.Commands.PhotoCommands;
using PersonBook.Application.Infrastructure;
using System.Threading.Tasks;

namespace PersonBook.Api.Controllers.V1
{
    [ApiController]
    [Route("v1/photo")]
    public class PhotoController : BaseAPiController
    {
        private readonly CommandExecutor _commandExecutor;

        public PhotoController(CommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpPost("upload")]
        [ProducesResponseType(typeof(DomainOperationResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] UploadPhotoCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            return CommandResultToHttpResponse(result, EntityStatusCode.Created);
        }
    }
}
