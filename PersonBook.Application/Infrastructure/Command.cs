using PersonBook.Application.Shared;
using PersonBook.Domain.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Infrastructure
{
    public abstract class Command : ApplicationBase
    {
        public abstract Task<CommandExecutionResult> ExecuteAsync();

        public async Task SaveAsync<TAggregate>(TAggregate aggregate, IRepository<TAggregate> repository) where TAggregate : AggregateRoot
        {
            repository.Save(aggregate);
            await _unitOfWork.SaveAsync();
        }

        protected Task<CommandExecutionResult> FailAsync(params string[] errorMessages)
        {
            var result = new CommandExecutionResult
            {
                Success = false
            };

            if (!errorMessages.Any())
            {
                result.Error.Errors = errorMessages;
            }

            return Task.FromResult(result);
        }

        protected Task<CommandExecutionResult> FailAsync(ErrorCode errorCode)
        {
            var result = new CommandExecutionResult
            {
                Success = false,
                ErrorCode = errorCode
            };

            return Task.FromResult(result);
        }

        protected Task<CommandExecutionResult> FailAsync(ErrorCode errorCode, string errorMessage)
        {
            var result = new CommandExecutionResult
            {
                Success = false,
                ErrorCode = errorCode,
                Error = new Error
                {
                    Errors = new List<string>
                    {
                        errorMessage
                    }
                }
            };

            return Task.FromResult(result);
        }

        protected async Task<CommandExecutionResult> OkAsync(DomainOperationResult data)
        {
            var result = new CommandExecutionResult
            {
                Data = data,
                Success = true
            };

            return await Task.FromResult(result);
        }
    }
}
