using Microsoft.AspNetCore.Mvc;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;

namespace PersonBook.Api.Infrastructure
{
    public class BaseAPiController : ControllerBase
    {
        protected IActionResult CommandResultToHttpResponse(CommandExecutionResult result, EntityStatusCode entityStatusCode)
        {
            if (result.Success)
            {
                return entityStatusCode switch
                {
                    EntityStatusCode.Created => Created("", result.Data),
                    EntityStatusCode.Updated => Ok(result.Data),
                    EntityStatusCode.Deleted => NoContent(),
                    _ => BadRequest(),
                };
            }

            if (result.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }

            return BadRequest(result.Error);
        }

        protected IActionResult QueryResultToHttpResponse<T>(QueryExecutionResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if (result.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }

            return BadRequest();
        }
    }

    public enum EntityStatusCode
    {
        Created = 1,
        Updated = 2,
        Deleted = 3
    }
}
